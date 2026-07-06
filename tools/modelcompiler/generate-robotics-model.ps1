Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$script:RepositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot "..\..")).Path
$script:NodeSetsFolder = Join-Path $script:RepositoryRoot "src\Robotics.ReferenceServer\NodeSets"
$script:CompilerInputFolder = Join-Path $script:RepositoryRoot "generated\modelcompiler-input\robotics-core"
$script:OutputFolder = Join-Path $script:RepositoryRoot "generated\opcua-robotics"
$script:RoboticsNamespaceUri = "http://opcfoundation.org/UA/Robotics/"
$script:ModelCompilerCommand = "Opc.Ua.ModelCompiler"

# Robotics is the generation target.
# DI is required as a dependency model.
# The main NodeSets folder may contain optional future models.
# This generation profile intentionally isolates Robotics + DI only.
# IA and Machinery are intentionally excluded from this generation run.
# Do not require Opc.Ua.NodeSet2.xml here. The base OPC UA model is provided by the SDK/compiler infrastructure.
$requiredNodeSets = @(
    "Opc.Ua.Di.NodeSet2.xml",
    "Opc.Ua.Robotics.NodeSet2.xml"
)

Write-Host "OPC UA Robotics model generation"
Write-Host "Repository root: $script:RepositoryRoot"
Write-Host "NodeSets folder:  $script:NodeSetsFolder"
Write-Host "Compiler input:   $script:CompilerInputFolder"
Write-Host "Output folder:    $script:OutputFolder"
Write-Host "Robotics URI:     $script:RoboticsNamespaceUri"
Write-Host ""

if (-not (Test-Path -LiteralPath $script:NodeSetsFolder -PathType Container)) {
    Write-Error "NodeSets folder was not found: $script:NodeSetsFolder"
    exit 1
}

$missingNodeSets = @()
foreach ($nodeSet in $requiredNodeSets) {
    $nodeSetPath = Join-Path $script:NodeSetsFolder $nodeSet
    if (-not (Test-Path -LiteralPath $nodeSetPath -PathType Leaf)) {
        $missingNodeSets += $nodeSetPath
    }
}

if ($missingNodeSets.Count -gt 0) {
    Write-Host "Missing required NodeSet XML files:"
    foreach ($missingNodeSet in $missingNodeSets) {
        Write-Host "  - $missingNodeSet"
    }

    Write-Host ""
    Write-Host "Place the official OPC Foundation NodeSet files in:"
    Write-Host "  $script:NodeSetsFolder"
    exit 1
}

Write-Host "All required NodeSet XML files were found."
Write-Host ""

$modelCompiler = Get-Command $script:ModelCompilerCommand -ErrorAction SilentlyContinue
if ($null -eq $modelCompiler) {
    Write-Error "UA-ModelCompiler command was not found: $script:ModelCompilerCommand"
    exit 1
}

if (Test-Path -LiteralPath $script:CompilerInputFolder) {
    Write-Host "Clearing existing clean compiler input folder..."
    Remove-Item -LiteralPath $script:CompilerInputFolder -Recurse -Force
}

New-Item -ItemType Directory -Path $script:CompilerInputFolder -Force | Out-Null
Write-Host "Created clean compiler input folder."

foreach ($nodeSet in $requiredNodeSets) {
    Copy-Item `
        -LiteralPath (Join-Path $script:NodeSetsFolder $nodeSet) `
        -Destination (Join-Path $script:CompilerInputFolder $nodeSet)
}

Write-Host "Copied isolated Robotics generation inputs:"
foreach ($nodeSet in $requiredNodeSets) {
    Write-Host "  - $nodeSet"
}

Write-Host ""

if (Test-Path -LiteralPath $script:OutputFolder -PathType Container) {
    Write-Host "Clearing existing generated output folder..."
    Remove-Item -LiteralPath $script:OutputFolder -Recurse -Force
}

New-Item -ItemType Directory -Path $script:OutputFolder -Force | Out-Null
Write-Host "Created clean output folder."
Write-Host ""
$roboticsNodeSetPath = Join-Path $script:CompilerInputFolder "Opc.Ua.Robotics.NodeSet2.xml"
$diNodeSetPath = Join-Path $script:CompilerInputFolder "Opc.Ua.Di.NodeSet2.xml"

$arguments = @(
    "compile",
    "-d2",
    "$roboticsNodeSetPath,Opc.Ua.Robotics,Robotics",
    "-d2",
    "$diNodeSetPath,Opc.Ua.DI,DI",
    "-version",
    "v105",
    "-exclude",
    "Draft",
    "-o2",
    $script:OutputFolder
)

$quotedArguments = $arguments | ForEach-Object {
    if ($_ -match "\s") {
        "`"$_`""
    } else {
        $_
    }
}

$displayCommand = "$script:ModelCompilerCommand " + ($quotedArguments -join " ")

Write-Host "Running UA-ModelCompiler:"
Write-Host $displayCommand
Write-Host ""

# Generated files should not be manually edited.
# Regenerate them from official NodeSets instead.
# Exact generated file names may depend on the installed UA-ModelCompiler version.
# This script only generates code. It does not load NodeSets into the running server.
# Target model: OPC UA Robotics.
# Dependency model: OPC UA DI.
& $script:ModelCompilerCommand @arguments

$exitCode = if ($null -eq $LASTEXITCODE) { 0 } else { $LASTEXITCODE }

if ($exitCode -ne 0) {
    Write-Error "UA-ModelCompiler failed with exit code $exitCode."
    exit $exitCode
}

$generatedCSharpFiles = @(Get-ChildItem -LiteralPath $script:OutputFolder -Filter "*.cs" -File -Recurse)
if ($generatedCSharpFiles.Count -lt 1) {
    Write-Error "UA-ModelCompiler completed, but no C# files were produced in: $script:OutputFolder"
    exit 1
}

Write-Host ""
Write-Host "OPC UA Robotics model generation completed successfully."
Write-Host "Generated C# files: $($generatedCSharpFiles.Count)"
Write-Host "Generated output:"
Write-Host "  $script:OutputFolder"
