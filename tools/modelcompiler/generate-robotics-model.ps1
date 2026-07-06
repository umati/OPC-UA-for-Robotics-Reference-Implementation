Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$script:RepositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot "..\..")).Path
$script:NodeSetsFolder = Join-Path $script:RepositoryRoot "src\Robotics.ReferenceServer\NodeSets"
$script:DiCompilerInputFolder = Join-Path $script:RepositoryRoot "generated\modelcompiler-input\di-core"
$script:RoboticsCompilerInputFolder = Join-Path $script:RepositoryRoot "generated\modelcompiler-input\robotics-core"
$script:DiOutputFolder = Join-Path $script:RepositoryRoot "generated\opcua-di"
$script:RoboticsOutputFolder = Join-Path $script:RepositoryRoot "generated\opcua-robotics"
$script:DiNamespaceUri = "http://opcfoundation.org/UA/DI/"
$script:RoboticsNamespaceUri = "http://opcfoundation.org/UA/Robotics/"
$script:ModelCompilerCommand = "Opc.Ua.ModelCompiler"

# The main NodeSets folder may contain optional future models.
# The current generation profiles intentionally isolate DI and Robotics + DI only.
# IA and Machinery are intentionally excluded from this generation run.
# Do not require Opc.Ua.NodeSet2.xml here. The base OPC UA model is provided by the SDK/compiler infrastructure.
$requiredNodeSets = @(
    "Opc.Ua.Di.NodeSet2.xml",
    "Opc.Ua.Robotics.NodeSet2.xml"
)

Write-Host "OPC UA Robotics model generation"
Write-Host "Repository root: $script:RepositoryRoot"
Write-Host "NodeSets folder:     $script:NodeSetsFolder"
Write-Host "DI compiler input:   $script:DiCompilerInputFolder"
Write-Host "DI output folder:    $script:DiOutputFolder"
Write-Host "DI URI:              $script:DiNamespaceUri"
Write-Host "Robotics input:      $script:RoboticsCompilerInputFolder"
Write-Host "Robotics output:     $script:RoboticsOutputFolder"
Write-Host "Robotics URI:        $script:RoboticsNamespaceUri"
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

function Reset-Folder {
    param(
        [Parameter(Mandatory = $true)]
        [string] $Path,

        [Parameter(Mandatory = $true)]
        [string] $Description
    )

    if (Test-Path -LiteralPath $Path) {
        Write-Host "Clearing existing $Description..."
        Remove-Item -LiteralPath $Path -Recurse -Force
    }

    New-Item -ItemType Directory -Path $Path -Force | Out-Null
    Write-Host "Created clean $Description."
}

function Copy-NodeSets {
    param(
        [Parameter(Mandatory = $true)]
        [string] $InputFolder,

        [Parameter(Mandatory = $true)]
        [string[]] $NodeSets
    )

    foreach ($nodeSet in $NodeSets) {
        Copy-Item `
            -LiteralPath (Join-Path $script:NodeSetsFolder $nodeSet) `
            -Destination (Join-Path $InputFolder $nodeSet)
    }
}

function Invoke-ModelCompiler {
    param(
        [Parameter(Mandatory = $true)]
        [string] $DisplayName,

        [Parameter(Mandatory = $true)]
        [string[]] $Arguments,

        [Parameter(Mandatory = $true)]
        [string] $OutputFolder
    )

    $quotedArguments = $Arguments | ForEach-Object {
        if ($_ -match "\s") {
            "`"$_`""
        } else {
            $_
        }
    }

    $displayCommand = "$script:ModelCompilerCommand " + ($quotedArguments -join " ")

    Write-Host "Running UA-ModelCompiler for ${DisplayName}:"
    Write-Host $displayCommand
    Write-Host ""

    & $script:ModelCompilerCommand @Arguments

    $exitCode = if ($null -eq $LASTEXITCODE) { 0 } else { $LASTEXITCODE }

    if ($exitCode -ne 0) {
        Write-Error "UA-ModelCompiler failed for $DisplayName with exit code $exitCode."
        exit $exitCode
    }

    $generatedCSharpFiles = @(Get-ChildItem -LiteralPath $OutputFolder -Filter "*.cs" -File -Recurse)
    if ($generatedCSharpFiles.Count -lt 1) {
        Write-Error "UA-ModelCompiler completed for $DisplayName, but no C# files were produced in: $OutputFolder"
        exit 1
    }

    Write-Host "$DisplayName generation completed. Generated C# files: $($generatedCSharpFiles.Count)"
    Write-Host ""
}

Reset-Folder -Path $script:DiCompilerInputFolder -Description "DI compiler input folder"
Copy-NodeSets -InputFolder $script:DiCompilerInputFolder -NodeSets @("Opc.Ua.Di.NodeSet2.xml")

Write-Host "Copied isolated DI generation inputs:"
Write-Host "  - Opc.Ua.Di.NodeSet2.xml"
Write-Host ""

Reset-Folder -Path $script:RoboticsCompilerInputFolder -Description "Robotics compiler input folder"
Copy-NodeSets -InputFolder $script:RoboticsCompilerInputFolder -NodeSets @(
    "Opc.Ua.Di.NodeSet2.xml",
    "Opc.Ua.Robotics.NodeSet2.xml"
)

Write-Host "Copied isolated Robotics generation inputs:"
Write-Host "  - Opc.Ua.Di.NodeSet2.xml"
Write-Host "  - Opc.Ua.Robotics.NodeSet2.xml"
Write-Host ""

Reset-Folder -Path $script:DiOutputFolder -Description "DI generated output folder"
Reset-Folder -Path $script:RoboticsOutputFolder -Description "Robotics generated output folder"
Write-Host ""

$diNodeSetPath = Join-Path $script:DiCompilerInputFolder "Opc.Ua.Di.NodeSet2.xml"
$diArguments = @(
    "compile",
    "-d2",
    "$diNodeSetPath,Opc.Ua.DI,DI",
    "-version",
    "v105",
    "-exclude",
    "Draft",
    "-o2",
    $script:DiOutputFolder
)

$roboticsNodeSetPath = Join-Path $script:RoboticsCompilerInputFolder "Opc.Ua.Robotics.NodeSet2.xml"
$roboticsDiNodeSetPath = Join-Path $script:RoboticsCompilerInputFolder "Opc.Ua.Di.NodeSet2.xml"
$roboticsArguments = @(
    "compile",
    "-d2",
    "$roboticsNodeSetPath,Opc.Ua.Robotics,Robotics",
    "-d2",
    "$roboticsDiNodeSetPath,Opc.Ua.DI,DI",
    "-version",
    "v105",
    "-exclude",
    "Draft",
    "-o2",
    $script:RoboticsOutputFolder
)

# Generated files should not be manually edited.
# Regenerate them from official NodeSets instead.
# Exact generated file names may depend on the installed UA-ModelCompiler version.
# This script only generates code. It does not load NodeSets into the running server.
# DI is generated first because Robotics generated classes reference DI generated types such as ComponentTypeState.
# Robotics is generated second with DI available as a dependency model.
Invoke-ModelCompiler -DisplayName "OPC UA DI" -Arguments $diArguments -OutputFolder $script:DiOutputFolder
Invoke-ModelCompiler -DisplayName "OPC UA Robotics" -Arguments $roboticsArguments -OutputFolder $script:RoboticsOutputFolder

Write-Host ""
Write-Host "OPC UA DI and Robotics model generation completed successfully."
Write-Host "Generated DI output:"
Write-Host "  $script:DiOutputFolder"
Write-Host "Generated Robotics output:"
Write-Host "  $script:RoboticsOutputFolder"
