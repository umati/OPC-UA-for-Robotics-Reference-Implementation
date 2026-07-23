param(
    [string]$GatewayBaseUrl = "http://localhost:5080",
    [string]$RobotId = "my-robot",
    [string]$ProgramName = "axis-range-demo",
    [int]$StopMode = 0
)

$ErrorActionPreference = "Stop"

function Pause-BeforeNext {
    param([string]$NextStep)

    Write-Host ""
    Write-Host "Press Enter to continue to: $NextStep" -ForegroundColor Cyan
    [void](Read-Host)
}

function Invoke-VerificationStep {
    param(
        [string]$Name,
        [scriptblock]$Action
    )

    Write-Host ""
    Write-Host "============================================================" -ForegroundColor DarkGray
    Write-Host $Name -ForegroundColor Yellow
    Write-Host "============================================================" -ForegroundColor DarkGray

    try {
        $result = & $Action

        if ($null -ne $result) {
            $result | ConvertTo-Json -Depth 20
        }

        Write-Host ""
        Write-Host "Step completed." -ForegroundColor Green
    }
    catch {
        Write-Host ""
        Write-Host "Step failed:" -ForegroundColor Red
        Write-Host $_.Exception.Message -ForegroundColor Red

        if ($_.ErrorDetails.Message) {
            Write-Host $_.ErrorDetails.Message -ForegroundColor DarkRed
        }
    }
}

function Assert-RotationalAxisSnapshot {
    param([object]$Snapshot)

    $expectedAxes = @("SAxis", "LAxis", "UAxis", "RAxis", "BAxis", "TAxis")
    $actualPositions = @($Snapshot.Sections | ForEach-Object { $_.Values } | Where-Object { $_.BrowseName -like "*ActualPosition" })

    if ($actualPositions.Count -ne $expectedAxes.Count) {
        throw "Expected six ActualPosition values, found $($actualPositions.Count)."
    }

    $units = @($actualPositions | ForEach-Object { $_.EngineeringUnits } | Where-Object { -not [string]::IsNullOrWhiteSpace($_) } | Select-Object -Unique)
    if ($units.Count -ne 1 -or $units[0] -like "ExtensionObject(*") {
        throw "Expected one meaningful non-generic EngineeringUnits value shared by the six rotational axes."
    }

    $nodeIds = @($actualPositions | ForEach-Object { $_.NodeId } | Select-Object -Unique)
    if ($nodeIds.Count -ne $expectedAxes.Count) {
        throw "ActualPosition NodeIds are not unique per axis; possible cross-axis data mixing."
    }

    $inventory = $Snapshot.MotionInventory
    if ($null -eq $inventory -or @($inventory.MotionDeviceSystems).Count -eq 0) {
        throw "Snapshot MotionInventory is missing or contains no MotionDeviceSystem."
    }

    $inventoryDevices = @($inventory.MotionDeviceSystems | ForEach-Object { $_.MotionDevices })
    $inventoryAxes = @($inventoryDevices | ForEach-Object { $_.Axes })
    if ($inventoryDevices.Count -lt 1 -or $inventoryAxes.Count -lt $expectedAxes.Count) {
        throw "MotionInventory does not retain the expected MotionDevice ownership and axes."
    }

    $stableKeys = @($inventoryAxes | ForEach-Object { $_.StableKey } | Where-Object { -not [string]::IsNullOrWhiteSpace($_) })
    if ($stableKeys.Count -ne $inventoryAxes.Count -or @($stableKeys | Select-Object -Unique).Count -ne $stableKeys.Count) {
        throw "MotionInventory stable Axis identifiers are missing or not unique."
    }

    foreach ($device in $inventoryDevices) {
        if ([string]::IsNullOrWhiteSpace($device.MotionDevice.StableKey)) {
            throw "MotionDevice ownership entry has no stable identifier."
        }
        foreach ($axis in @($device.Axes)) {
            if ($axis.MotionDeviceKey -ne $device.MotionDevice.StableKey) {
                throw "Axis '$($axis.Axis.DisplayName)' does not retain its MotionDevice ownership key."
            }
            if ($null -ne $axis.ActualPosition -and [string]::IsNullOrWhiteSpace($axis.ActualPosition.StatusCode)) {
                throw "Axis '$($axis.Axis.DisplayName)' ActualPosition is missing its exact StatusCode."
            }
        }
    }

    foreach ($axisName in $expectedAxes) {
        $axisPosition = $actualPositions | Where-Object { $_.Label -like "*$axisName*" } | Select-Object -First 1
        if ($null -eq $axisPosition) {
            throw "ActualPosition for $axisName was not found."
        }

        if ([string]::IsNullOrWhiteSpace($axisPosition.EngineeringUnits)) {
            throw "ActualPosition EngineeringUnits for $axisName is null or empty."
        }

        if ($null -eq $axisPosition.EngineeringUnit) {
            throw "ActualPosition for $axisName has no structured EUInformation metadata."
        }

        if ([string]::IsNullOrWhiteSpace($axisPosition.EngineeringUnit.NamespaceUri) -or
            $null -eq $axisPosition.EngineeringUnit.UnitId -or
            [string]::IsNullOrWhiteSpace($axisPosition.EngineeringUnit.DisplayName) -or
            [string]::IsNullOrWhiteSpace($axisPosition.EngineeringUnit.Description)) {
            throw "ActualPosition for $axisName has incomplete structured EUInformation metadata."
        }

        if ($axisPosition.EngineeringUnits -like "ExtensionObject(*") {
            throw "ActualPosition for $axisName still exposes generic ExtensionObject text as its primary unit."
        }

        if ($axisPosition.StatusCode -ne "Good") {
            throw "ActualPosition for $axisName has StatusCode '$($axisPosition.StatusCode)'."
        }

        if ($axisPosition.ValueText -notmatch '^[-+]?\d+(\.\d+)?([eE][-+]?\d+)?$') {
            throw "ActualPosition for $axisName has no numeric value text."
        }
    }

    return $Snapshot
}

$robotBaseUrl = "$GatewayBaseUrl/api/robots/$RobotId"
$legacyBaseUrl = "$GatewayBaseUrl/api/robotics"

Write-Host ""
Write-Host "OPC UA Robotics Manual Verification" -ForegroundColor Green
Write-Host "Gateway: $GatewayBaseUrl"
Write-Host "Robot ID: $RobotId"
Write-Host "Program: $ProgramName"
Write-Host "Stop mode: $StopMode"

$steps = @(
    @{
        Name = "1. List configured robots"
        Action = {
            Invoke-RestMethod -Method Get -Uri "$GatewayBaseUrl/api/robots"
        }
    },
    @{
        Name = "2. Robot-scoped status"
        Action = {
            Invoke-RestMethod -Method Get -Uri "$robotBaseUrl/status"
        }
    },
    @{
        Name = "3. Robot-scoped discovery"
        Action = {
            Invoke-RestMethod -Method Get -Uri "$robotBaseUrl/discovery"
        }
    },
    @{
        Name = "4. Robot-scoped snapshot"
        Action = {
            $snapshot = Invoke-RestMethod -Method Get -Uri "$robotBaseUrl/snapshot?selection=all"
            Assert-RotationalAxisSnapshot -Snapshot $snapshot
        }
    },
    @{
        Name = "5. SystemOperation - GetReady"
        Action = {
            Invoke-RestMethod -Method Post -Uri "$robotBaseUrl/system/get-ready" -ContentType "application/json" -Body "{}"
        }
    },
    @{
        Name = "6. SystemOperation - Start"
        Action = {
            Invoke-RestMethod -Method Post -Uri "$robotBaseUrl/system/start" -ContentType "application/json" -Body "{}"
        }
    },
    @{
        Name = "7. SystemOperation - Stop"
        Action = {
            $body = @{ stopMode = $StopMode } | ConvertTo-Json
            Invoke-RestMethod -Method Post -Uri "$robotBaseUrl/system/stop" -ContentType "application/json" -Body $body
        }
    },
    @{
        Name = "8. SystemOperation - StandDown"
        Action = {
            Invoke-RestMethod -Method Post -Uri "$robotBaseUrl/system/stand-down" -ContentType "application/json" -Body "{}"
        }
    },
    @{
        Name = "9. TaskControlOperation - LoadByName"
        Action = {
            $body = @{ programName = $ProgramName } | ConvertTo-Json
            Invoke-RestMethod -Method Post -Uri "$robotBaseUrl/task/load-by-name" -ContentType "application/json" -Body $body
        }
    },
    @{
        Name = "10. TaskControlOperation - Start"
        Action = {
            Invoke-RestMethod -Method Post -Uri "$robotBaseUrl/task/start" -ContentType "application/json" -Body "{}"
        }
    },
    @{
        Name = "11. TaskControlOperation - Stop"
        Action = {
            $body = @{ stopMode = $StopMode } | ConvertTo-Json
            Invoke-RestMethod -Method Post -Uri "$robotBaseUrl/task/stop" -ContentType "application/json" -Body $body
        }
    },
    @{
        Name = "12. TaskControlOperation - ResetToProgramStart"
        Action = {
            Invoke-RestMethod -Method Post -Uri "$robotBaseUrl/task/reset-to-program-start" -ContentType "application/json" -Body "{}"
        }
    },
    @{
        Name = "13. TaskControlOperation - UnloadProgram"
        Action = {
            Invoke-RestMethod -Method Post -Uri "$robotBaseUrl/task/unload-program" -ContentType "application/json" -Body "{}"
        }
    },
    @{
        Name = "14. Negative test - Unknown robot ID"
        Action = {
            Invoke-RestMethod -Method Post -Uri "$GatewayBaseUrl/api/robots/unknown/system/get-ready" -ContentType "application/json" -Body "{}"
        }
    },
    @{
        Name = "15. Backward compatibility - Legacy status"
        Action = {
            Invoke-RestMethod -Method Get -Uri "$GatewayBaseUrl/api/opcua/status"
        }
    },
    @{
        Name = "16. Backward compatibility - Legacy discovery"
        Action = {
            Invoke-RestMethod -Method Get -Uri "$legacyBaseUrl/discovery"
        }
    },
    @{
        Name = "17. Backward compatibility - Legacy snapshot"
        Action = {
            Invoke-RestMethod -Method Get -Uri "$legacyBaseUrl/snapshot?selection=all"
        }
    },
    @{
        Name = "18. Backward compatibility - Legacy GetReady"
        Action = {
            Invoke-RestMethod -Method Post -Uri "$legacyBaseUrl/system/get-ready" -ContentType "application/json" -Body "{}"
        }
    }
)

for ($index = 0; $index -lt $steps.Count; $index++) {
    $step = $steps[$index]
    Invoke-VerificationStep -Name $step.Name -Action $step.Action

    if ($index -lt ($steps.Count - 1)) {
        Pause-BeforeNext -NextStep $steps[$index + 1].Name
    }
}

Write-Host ""
Write-Host "Manual verification complete" -ForegroundColor Green
