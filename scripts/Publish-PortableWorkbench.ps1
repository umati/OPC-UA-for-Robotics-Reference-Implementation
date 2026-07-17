[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)][string]$Version,
    [Parameter(Mandatory=$true)][string]$OutputDirectory,
    [string]$SourceRevisionId = ""
)

$ErrorActionPreference = "Stop"
$repo = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
$workbench = Join-Path $repo "src\Robotics.Workbench"
$gateway = Join-Path $repo "src\Robotics.ClientGateway"
$launcher = Join-Path $repo "src\Robotics.Workbench.Launcher"
$stage = Join-Path ([IO.Path]::GetFullPath($OutputDirectory)) "OPC-UA-Robotics-Workbench-$Version-win-x64"
$appStage = Join-Path $stage "app"
$launcherStage = Join-Path ([IO.Path]::GetTempPath()) "Robotics.Workbench.Launcher-$Version"
$zip = "$stage.zip"
$checksum = "$zip.sha256"

if (Test-Path $stage) { Remove-Item -LiteralPath $stage -Recurse -Force }
if (Test-Path $launcherStage) { Remove-Item -LiteralPath $launcherStage -Recurse -Force }
New-Item -ItemType Directory -Path $stage, $appStage | Out-Null

Push-Location $workbench
try {
    if (Test-Path package-lock.json) { npm ci } else { npm install }
    npm run build
} finally { Pop-Location }

dotnet publish (Join-Path $gateway "Robotics.ClientGateway.csproj") -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -p:PublishTrimmed=false -p:Version=$Version -p:VersionPrefix=$Version -p:SourceRevisionId=$SourceRevisionId -o $appStage
New-Item -ItemType Directory -Path (Join-Path $appStage "wwwroot") -Force | Out-Null
Copy-Item (Join-Path $workbench "dist\*") (Join-Path $appStage "wwwroot") -Recurse -Force

dotnet publish (Join-Path $launcher "Robotics.Workbench.Launcher.csproj") -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=false -o $launcherStage
Copy-Item (Join-Path $launcherStage "OPC-UA-Robotics-Workbench.exe") (Join-Path $stage "OPC-UA-Robotics-Workbench.exe") -Force

Copy-Item (Join-Path $repo "packaging\robots.json") (Join-Path $stage "robots.json") -Force
Copy-Item (Join-Path $repo "packaging\README-FIRST.txt") (Join-Path $stage "README-FIRST.txt") -Force
Set-Content -LiteralPath (Join-Path $stage "VERSION.txt") -Value @("OPC UA Robotics Workbench", "Version: $Version", "Revision: $SourceRevisionId", "Runtime: win-x64") -Encoding utf8
New-Item -ItemType Directory -Path (Join-Path $stage "docs"), (Join-Path $stage "licenses"), (Join-Path $stage "logs"), (Join-Path $stage "certificates") -Force | Out-Null
Copy-Item (Join-Path $repo "docs\*") (Join-Path $stage "docs") -Recurse -Force
Copy-Item (Join-Path $repo "LICENSE") (Join-Path $stage "licenses\LICENSE") -Force

$allowedRootFiles = @("OPC-UA-Robotics-Workbench.exe", "robots.json", "README-FIRST.txt", "VERSION.txt")
$allowedRootDirectories = @("app", "logs", "certificates", "docs", "licenses")
$unexpectedFiles = @(Get-ChildItem -LiteralPath $stage -File | Where-Object { $allowedRootFiles -notcontains $_.Name })
$unexpectedDirectories = @(Get-ChildItem -LiteralPath $stage -Directory | Where-Object { $allowedRootDirectories -notcontains $_.Name })
if ($unexpectedFiles.Count -or $unexpectedDirectories.Count) {
    $names = @($unexpectedFiles.Name) + @($unexpectedDirectories.Name)
    throw "Unexpected package-root entries: $($names -join ', ')"
}
if (-not (Test-Path (Join-Path $appStage "Robotics.ClientGateway.exe"))) {
    throw "Gateway executable was not found under app\."
}

if (Test-Path $zip) { Remove-Item -LiteralPath $zip -Force }
Compress-Archive -Path (Join-Path $stage "*") -DestinationPath $zip
Get-FileHash -Algorithm SHA256 -LiteralPath $zip | ForEach-Object { "$($_.Hash)  $([IO.Path]::GetFileName($zip))" } | Set-Content -LiteralPath $checksum -Encoding ascii
Write-Host "Distribution: $stage"
Write-Host "ZIP: $zip"
Write-Host "SHA-256: $checksum"
