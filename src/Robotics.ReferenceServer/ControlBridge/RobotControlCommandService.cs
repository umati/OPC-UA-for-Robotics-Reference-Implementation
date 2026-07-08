using Opc.Ua;
using Robotics.ReferenceServer.Simulation;
using Robotics.Shared;
using System.Text.Json;

namespace Robotics.ReferenceServer.ControlBridge;

internal sealed class RobotControlCommandService
{
    private static readonly IReadOnlyDictionary<string, string> SampleProgramFiles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["pick-and-place-demo"] = "pick-and-place-demo.json",
        ["axis-range-demo"] = "axis-range-demo.json"
    };

    private static readonly JsonSerializerOptions ProgramJsonOptions = new(JsonSerializerDefaults.Web);

    private readonly RobotSimulationService _simulationService;
    private readonly RobotProgramExecutor _programExecutor;
    private readonly object _syncRoot;
    private readonly Action<string, bool, string> _updateRemoteControlStatus;
    private readonly Action<string, bool, string> _updateRemoteProgramCommandStatus;

    public RobotControlCommandService(
        RobotSimulationService simulationService,
        RobotProgramExecutor programExecutor,
        object syncRoot,
        Action<string, bool, string> updateRemoteControlStatus,
        Action<string, bool, string> updateRemoteProgramCommandStatus)
    {
        _simulationService = simulationService;
        _programExecutor = programExecutor;
        _syncRoot = syncRoot;
        _updateRemoteControlStatus = updateRemoteControlStatus;
        _updateRemoteProgramCommandStatus = updateRemoteProgramCommandStatus;
    }

    public RobotControlCommandResult MoveJoints(
        double s,
        double l,
        double u,
        double r,
        double b,
        double t)
    {
        lock (_syncRoot)
        {
            _simulationService.SetJointTargets(
            [
                new RobotJointTarget { AxisName = RobotAxisName.S, TargetPositionDegrees = s },
                new RobotJointTarget { AxisName = RobotAxisName.L, TargetPositionDegrees = l },
                new RobotJointTarget { AxisName = RobotAxisName.U, TargetPositionDegrees = u },
                new RobotJointTarget { AxisName = RobotAxisName.R, TargetPositionDegrees = r },
                new RobotJointTarget { AxisName = RobotAxisName.B, TargetPositionDegrees = b },
                new RobotJointTarget { AxisName = RobotAxisName.T, TargetPositionDegrees = t }
            ]);

            return UpdateRemoteControlStatus("MoveJoints", true, "Joint targets accepted.");
        }
    }

    public RobotControlCommandResult RejectMoveJoints(string message)
    {
        lock (_syncRoot)
        {
            return UpdateRemoteControlStatus("MoveJoints", false, message, RobotControlCommandFailureKind.InvalidArgument);
        }
    }

    public RobotControlCommandResult ResetToHome()
    {
        lock (_syncRoot)
        {
            _simulationService.SetJointTargets(
            [
                new RobotJointTarget { AxisName = RobotAxisName.S, TargetPositionDegrees = 0 },
                new RobotJointTarget { AxisName = RobotAxisName.L, TargetPositionDegrees = 0 },
                new RobotJointTarget { AxisName = RobotAxisName.U, TargetPositionDegrees = 0 },
                new RobotJointTarget { AxisName = RobotAxisName.R, TargetPositionDegrees = 0 },
                new RobotJointTarget { AxisName = RobotAxisName.B, TargetPositionDegrees = 0 },
                new RobotJointTarget { AxisName = RobotAxisName.T, TargetPositionDegrees = 0 }
            ]);

            return UpdateRemoteControlStatus("ResetToHome", true, "Home joint targets accepted.");
        }
    }

    public RobotControlCommandResult StartDemoMotion()
    {
        lock (_syncRoot)
        {
            _simulationService.SetJointTargets(
            [
                new RobotJointTarget { AxisName = RobotAxisName.S, TargetPositionDegrees = 45 },
                new RobotJointTarget { AxisName = RobotAxisName.L, TargetPositionDegrees = -30 },
                new RobotJointTarget { AxisName = RobotAxisName.U, TargetPositionDegrees = 60 },
                new RobotJointTarget { AxisName = RobotAxisName.R, TargetPositionDegrees = 20 },
                new RobotJointTarget { AxisName = RobotAxisName.B, TargetPositionDegrees = 15 },
                new RobotJointTarget { AxisName = RobotAxisName.T, TargetPositionDegrees = 90 }
            ]);

            return UpdateRemoteControlStatus("StartDemoMotion", true, "Demo joint targets accepted.");
        }
    }

    public RobotControlCommandResult StopMotion()
    {
        lock (_syncRoot)
        {
            _simulationService.StopMotion();
            return UpdateRemoteControlStatus("StopMotion", true, "Stop motion accepted.");
        }
    }

    public RobotControlCommandResult LoadProgramFromJson(string? programJson)
    {
        if (string.IsNullOrWhiteSpace(programJson))
        {
            return UpdateRemoteProgramCommandStatus(
                "LoadProgramFromJson",
                false,
                "LoadProgramFromJson requires a non-empty ProgramJson string.",
                RobotControlCommandFailureKind.InvalidArgument);
        }

        RobotProgram? program;
        try
        {
            program = JsonSerializer.Deserialize<RobotProgram>(programJson, ProgramJsonOptions);
        }
        catch (JsonException exception)
        {
            return UpdateRemoteProgramCommandStatus(
                "LoadProgramFromJson",
                false,
                $"ProgramJson is not valid JSON: {exception.Message}",
                RobotControlCommandFailureKind.InvalidArgument);
        }

        return LoadProgram("LoadProgramFromJson", program, "ProgramJson accepted.");
    }

    public RobotControlCommandResult LoadSampleProgram(string? sampleProgramName)
    {
        return LoadSampleProgram("LoadSampleProgram", sampleProgramName, "SampleProgramName");
    }

    public RobotControlCommandResult LoadProgramByName(string? programName)
    {
        return LoadSampleProgram("LoadByName", programName, "Name");
    }

    public RobotControlCommandResult LoadProgramByNodeId(ExpandedNodeId? programNodeId)
    {
        return UpdateRemoteProgramCommandStatus(
            "LoadByNodeId",
            false,
            "LoadByNodeId is not supported because the reference program store does not expose OPC UA FileType program nodes.",
            RobotControlCommandFailureKind.NotSupported);
    }

    public RobotControlCommandResult RejectProgramCommand(
        string commandName,
        string message,
        RobotControlCommandFailureKind failureKind)
    {
        return UpdateRemoteProgramCommandStatus(commandName, false, message, failureKind);
    }

    private RobotControlCommandResult LoadSampleProgram(string commandName, string? sampleProgramName, string argumentName)
    {
        if (string.IsNullOrWhiteSpace(sampleProgramName))
        {
            return UpdateRemoteProgramCommandStatus(
                commandName,
                false,
                $"{commandName} requires a non-empty {argumentName} string.",
                RobotControlCommandFailureKind.InvalidArgument);
        }

        string normalizedName = NormalizeSampleProgramName(sampleProgramName);
        if (!SampleProgramFiles.TryGetValue(normalizedName, out string? fileName))
        {
            return UpdateRemoteProgramCommandStatus(
                commandName,
                false,
                $"Sample program '{sampleProgramName}' is not supported.",
                RobotControlCommandFailureKind.InvalidArgument);
        }

        SampleProgramPathResolution sampleProgramPath = FindSampleProgramPath(fileName);
        if (sampleProgramPath.Path is null)
        {
            Console.WriteLine(
                $"Sample program file '{fileName}' was not found. Searched paths: {string.Join("; ", sampleProgramPath.SearchedPaths)}");
            return UpdateRemoteProgramCommandStatus(
                commandName,
                false,
                $"Sample program file '{fileName}' was not found. Searched paths: {string.Join("; ", sampleProgramPath.SearchedPaths)}",
                RobotControlCommandFailureKind.NotFound);
        }

        RobotProgram? program;
        try
        {
            string programJson = File.ReadAllText(sampleProgramPath.Path);
            program = JsonSerializer.Deserialize<RobotProgram>(programJson, ProgramJsonOptions);
        }
        catch (IOException exception)
        {
            return UpdateRemoteProgramCommandStatus(
                commandName,
                false,
                $"Sample program file could not be read: {exception.Message}",
                RobotControlCommandFailureKind.Unexpected);
        }
        catch (JsonException exception)
        {
            return UpdateRemoteProgramCommandStatus(
                commandName,
                false,
                $"Sample program file is not valid JSON: {exception.Message}",
                RobotControlCommandFailureKind.InvalidArgument);
        }

        RobotControlCommandResult result = LoadProgram(commandName, program, $"Sample program '{normalizedName}' accepted.");
        if (result.Accepted)
        {
            Console.WriteLine($"Loaded program by name: {normalizedName}");
            Console.WriteLine($"Program source: {sampleProgramPath.Path}");
            Console.WriteLine($"Step count: {program?.Steps.Count ?? 0}");
        }

        return result;
    }

    public RobotControlCommandResult StartProgram()
    {
        lock (_syncRoot)
        {
            bool accepted = _programExecutor.Start();
            string message = accepted
                ? "Program start accepted."
                : GetProgramCommandRejectionMessage("Program start was rejected.");

            return UpdateRemoteProgramCommandStatus(
                "StartProgram",
                accepted,
                message,
                RobotControlCommandFailureKind.InvalidState);
        }
    }

    public RobotControlCommandResult PauseProgram()
    {
        lock (_syncRoot)
        {
            RobotProgramExecutionState currentState = _programExecutor.State;
            bool accepted = _programExecutor.Pause();
            string message = accepted
                ? "Program pause accepted."
                : $"Program pause was rejected because the program is {currentState}, not Running.";

            return UpdateRemoteProgramCommandStatus(
                "PauseProgram",
                accepted,
                message,
                RobotControlCommandFailureKind.InvalidState);
        }
    }

    public RobotControlCommandResult ResumeProgram()
    {
        lock (_syncRoot)
        {
            bool accepted = _programExecutor.Resume();
            return UpdateRemoteProgramCommandStatus(
                "ResumeProgram",
                accepted,
                accepted ? "Program resume accepted." : "Program resume was rejected because the program is not paused.",
                RobotControlCommandFailureKind.InvalidState);
        }
    }

    public RobotControlCommandResult StopProgram()
    {
        lock (_syncRoot)
        {
            _programExecutor.Stop();
            return UpdateRemoteProgramCommandStatus("StopProgram", true, "Program stop accepted.");
        }
    }

    public RobotControlCommandResult UnloadProgram()
    {
        lock (_syncRoot)
        {
            bool accepted = _programExecutor.UnloadProgram();
            return UpdateRemoteProgramCommandStatus(
                "UnloadProgram",
                accepted,
                accepted ? "Program unload accepted." : "Program unload was rejected because no program is loaded.",
                RobotControlCommandFailureKind.InvalidState);
        }
    }

    public RobotControlCommandResult UnloadProgramByName(string? programName)
    {
        if (string.IsNullOrWhiteSpace(programName))
        {
            return UpdateRemoteProgramCommandStatus(
                "UnloadByName",
                false,
                "UnloadByName requires a non-empty Name string.",
                RobotControlCommandFailureKind.InvalidArgument);
        }

        lock (_syncRoot)
        {
            bool accepted = _programExecutor.UnloadProgramByName(programName.Trim());
            return UpdateRemoteProgramCommandStatus(
                "UnloadByName",
                accepted,
                accepted
                    ? $"Program '{programName.Trim()}' unload accepted."
                    : $"Program '{programName}' is not the currently loaded program.",
                RobotControlCommandFailureKind.InvalidState);
        }
    }

    public RobotControlCommandResult UnloadProgramByNodeId(ExpandedNodeId? programNodeId)
    {
        return UpdateRemoteProgramCommandStatus(
            "UnloadByNodeId",
            false,
            "UnloadByNodeId is not supported because the reference program store does not expose OPC UA FileType program nodes.",
            RobotControlCommandFailureKind.NotSupported);
    }

    public RobotControlCommandResult ResetProgramToStart()
    {
        lock (_syncRoot)
        {
            bool accepted = _programExecutor.ResetToProgramStart();
            return UpdateRemoteProgramCommandStatus(
                "ResetToProgramStart",
                accepted,
                accepted
                    ? "Program reset to first step accepted."
                    : "Program reset was rejected because no program is loaded or the program is running.",
                RobotControlCommandFailureKind.InvalidState);
        }
    }

    private RobotControlCommandResult LoadProgram(string commandName, RobotProgram? program, string acceptedMessage)
    {
        RobotProgramValidationResult validationResult = RobotProgramValidator.Validate(program);
        if (!validationResult.IsValid)
        {
            return UpdateRemoteProgramCommandStatus(
                commandName,
                false,
                string.Join(" ", validationResult.ErrorMessages),
                RobotControlCommandFailureKind.InvalidArgument);
        }

        lock (_syncRoot)
        {
            RobotProgramValidationResult loadResult = _programExecutor.LoadProgram(program!);
            bool accepted = loadResult.IsValid;
            string message = accepted ? acceptedMessage : string.Join(" ", loadResult.ErrorMessages);
            return UpdateRemoteProgramCommandStatus(
                commandName,
                accepted,
                message,
                RobotControlCommandFailureKind.InvalidArgument);
        }
    }

    private RobotControlCommandResult UpdateRemoteControlStatus(
        string commandName,
        bool accepted,
        string message,
        RobotControlCommandFailureKind failureKind = RobotControlCommandFailureKind.None)
    {
        _updateRemoteControlStatus(commandName, accepted, message);
        return accepted
            ? RobotControlCommandResult.Accept(message)
            : RobotControlCommandResult.Reject(message, failureKind);
    }

    private RobotControlCommandResult UpdateRemoteProgramCommandStatus(
        string commandName,
        bool accepted,
        string message,
        RobotControlCommandFailureKind failureKind = RobotControlCommandFailureKind.None)
    {
        lock (_syncRoot)
        {
            _updateRemoteProgramCommandStatus(commandName, accepted, message);
        }

        return accepted
            ? RobotControlCommandResult.Accept(message)
            : RobotControlCommandResult.Reject(message, failureKind);
    }

    private static string NormalizeSampleProgramName(string sampleProgramName)
    {
        string trimmedName = sampleProgramName.Trim();
        return trimmedName.EndsWith(".json", StringComparison.OrdinalIgnoreCase)
            ? trimmedName[..^".json".Length]
            : trimmedName;
    }

    private static SampleProgramPathResolution FindSampleProgramPath(string fileName)
    {
        string relativePath = Path.Combine("RobotPrograms", "SamplePrograms", fileName);
        string repositoryRelativePath = Path.Combine("src", "Robotics.ReferenceServer", relativePath);
        var candidatePaths = new List<string>();

        foreach (string startDirectory in GetExistingSearchStartDirectories())
        {
            foreach (string directory in EnumerateSelfAndParents(startDirectory))
            {
                AddCandidatePath(candidatePaths, Path.Combine(directory, repositoryRelativePath));
            }
        }

        foreach (string startDirectory in GetExistingSearchStartDirectories())
        {
            foreach (string directory in EnumerateSelfAndParents(startDirectory))
            {
                AddCandidatePath(candidatePaths, Path.Combine(directory, relativePath));
            }
        }

        string? foundPath = candidatePaths.FirstOrDefault(File.Exists);
        return new SampleProgramPathResolution(foundPath, candidatePaths);
    }

    private string GetProgramCommandRejectionMessage(string fallbackMessage)
    {
        string? lastErrorMessage = _programExecutor.LastErrorMessage;
        return string.IsNullOrWhiteSpace(lastErrorMessage) ? fallbackMessage : lastErrorMessage;
    }

    private static IEnumerable<string> GetExistingSearchStartDirectories()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        if (Directory.Exists(currentDirectory))
        {
            yield return currentDirectory;
        }

        string baseDirectory = AppContext.BaseDirectory;
        if (Directory.Exists(baseDirectory)
            && !string.Equals(Path.GetFullPath(baseDirectory), Path.GetFullPath(currentDirectory), StringComparison.OrdinalIgnoreCase))
        {
            yield return baseDirectory;
        }
    }

    private static IEnumerable<string> EnumerateSelfAndParents(string startDirectory)
    {
        DirectoryInfo? directory = new DirectoryInfo(Path.GetFullPath(startDirectory));
        while (directory is not null)
        {
            yield return directory.FullName;
            directory = directory.Parent;
        }
    }

    private static void AddCandidatePath(List<string> candidatePaths, string candidatePath)
    {
        string fullPath = Path.GetFullPath(candidatePath);
        if (!candidatePaths.Contains(fullPath, StringComparer.OrdinalIgnoreCase))
        {
            candidatePaths.Add(fullPath);
        }
    }

    private sealed record SampleProgramPathResolution(string? Path, IReadOnlyList<string> SearchedPaths);
}
