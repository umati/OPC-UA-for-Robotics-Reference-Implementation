using Robotics.Shared;

namespace Robotics.ReferenceServer.Simulation;

public sealed class RobotProgramExecutor
{
    private const double PositionToleranceDegrees = 0.05;
    private const double VelocityToleranceDegreesPerSecond = 0.1;

    private readonly RobotSimulationService _simulationService;

    private RobotProgram? _currentProgram;
    private int _currentStepIndex = -1;
    private RobotProgramExecutionState _state = RobotProgramExecutionState.Idle;
    private string? _lastErrorMessage;
    private bool _currentStepInitialized;
    private TimeSpan _waitElapsed;

    public RobotProgramExecutor(RobotSimulationService simulationService)
    {
        _simulationService = simulationService ?? throw new ArgumentNullException(nameof(simulationService));
    }

    public string? CurrentProgramName => _currentProgram?.ProgramName;

    public int CurrentStepIndex => _currentStepIndex;

    public RobotProgramExecutionState State => _state;

    public string? LastErrorMessage => _lastErrorMessage;

    public RobotProgramValidationResult LoadProgram(RobotProgram program)
    {
        RobotProgramValidationResult validationResult = RobotProgramValidator.Validate(program);
        if (!validationResult.IsValid)
        {
            _currentProgram = null;
            _currentStepIndex = -1;
            _state = RobotProgramExecutionState.Error;
            _lastErrorMessage = string.Join(" ", validationResult.ErrorMessages);
            ResetCurrentStep();
            return validationResult;
        }

        _currentProgram = program with { Steps = program.Steps.ToArray() };
        _currentStepIndex = 0;
        _state = RobotProgramExecutionState.Loaded;
        _lastErrorMessage = null;
        ResetCurrentStep();

        return validationResult;
    }

    public bool Start()
    {
        if (_currentProgram is null)
        {
            SetError("No robot program is loaded.");
            return false;
        }

        if (_state is RobotProgramExecutionState.Running or RobotProgramExecutionState.Paused)
        {
            return false;
        }

        _currentStepIndex = 0;
        _state = RobotProgramExecutionState.Running;
        _lastErrorMessage = null;
        ResetCurrentStep();
        return true;
    }

    public bool Pause()
    {
        if (_state != RobotProgramExecutionState.Running)
        {
            return false;
        }

        // Program advancement is paused without changing the simulated robot position.
        _state = RobotProgramExecutionState.Paused;
        return true;
    }

    public bool Resume()
    {
        if (_state != RobotProgramExecutionState.Paused)
        {
            return false;
        }

        _state = RobotProgramExecutionState.Running;
        return true;
    }

    public void Stop()
    {
        if (_state == RobotProgramExecutionState.Idle)
        {
            return;
        }

        _simulationService.StopMotion();
        _state = RobotProgramExecutionState.Stopped;
        _lastErrorMessage = null;
        ResetCurrentStep();
    }

    public void Update(TimeSpan elapsed)
    {
        if (_state != RobotProgramExecutionState.Running || _currentProgram is null)
        {
            return;
        }

        if (_currentStepIndex >= _currentProgram.Steps.Count)
        {
            CompleteProgram();
            return;
        }

        RobotProgramStep step = _currentProgram.Steps[_currentStepIndex];
        switch (step.Type)
        {
            case RobotProgramStepType.MoveJoint:
                ExecuteMoveJoint(step);
                break;

            case RobotProgramStepType.Wait:
                ExecuteWait(step, elapsed);
                break;

            default:
                SetError($"Step {_currentStepIndex} has an unsupported Type.");
                break;
        }
    }

    public RobotProgramExecutionSnapshot GetSnapshot()
    {
        return new RobotProgramExecutionSnapshot
        {
            CurrentProgramName = CurrentProgramName,
            CurrentStepIndex = _currentStepIndex,
            State = _state,
            LastErrorMessage = _lastErrorMessage,
            TotalStepCount = _currentProgram?.Steps.Count ?? 0
        };
    }

    private void ExecuteMoveJoint(RobotProgramStep step)
    {
        if (step.JointTarget is null)
        {
            SetError($"Step {_currentStepIndex} MoveJoint requires JointTarget.");
            return;
        }

        if (!_currentStepInitialized)
        {
            // Program execution is preliminary and will later be mapped to OPC UA Robotics Remote Operation concepts.
            // Linear Cartesian moves are intentionally not supported yet; joint moves set axis targets directly.
            _simulationService.SetJointTargets(ToJointTargets(step.JointTarget));
            _currentStepInitialized = true;
        }

        if (HasReachedCurrentSimulationTargets())
        {
            AdvanceStep();
        }
    }

    private void ExecuteWait(RobotProgramStep step, TimeSpan elapsed)
    {
        if (!_currentStepInitialized)
        {
            _waitElapsed = TimeSpan.Zero;
            _currentStepInitialized = true;
        }

        if (elapsed > TimeSpan.Zero)
        {
            _waitElapsed += elapsed;
        }

        if (_waitElapsed.TotalMilliseconds >= step.DurationMs)
        {
            AdvanceStep();
        }
    }

    private bool HasReachedCurrentSimulationTargets()
    {
        RobotTelemetrySnapshot snapshot = _simulationService.GetSnapshot();
        return snapshot.AxisStates.All(axis =>
            Math.Abs(axis.TargetPositionDegrees - axis.PositionDegrees) <= PositionToleranceDegrees
            && Math.Abs(axis.VelocityDegreesPerSecond) <= VelocityToleranceDegreesPerSecond);
    }

    private void AdvanceStep()
    {
        _currentStepIndex++;
        ResetCurrentStep();

        if (_currentProgram is not null && _currentStepIndex >= _currentProgram.Steps.Count)
        {
            CompleteProgram();
        }
    }

    private void CompleteProgram()
    {
        _state = RobotProgramExecutionState.Completed;
        _lastErrorMessage = null;
        ResetCurrentStep();
    }

    private void ResetCurrentStep()
    {
        _currentStepInitialized = false;
        _waitElapsed = TimeSpan.Zero;
    }

    private void SetError(string message)
    {
        _state = RobotProgramExecutionState.Error;
        _lastErrorMessage = message;
        ResetCurrentStep();
    }

    private static IEnumerable<RobotJointTarget> ToJointTargets(JointMoveTarget target)
    {
        yield return new RobotJointTarget { AxisName = RobotAxisName.S, TargetPositionDegrees = target.S };
        yield return new RobotJointTarget { AxisName = RobotAxisName.L, TargetPositionDegrees = target.L };
        yield return new RobotJointTarget { AxisName = RobotAxisName.U, TargetPositionDegrees = target.U };
        yield return new RobotJointTarget { AxisName = RobotAxisName.R, TargetPositionDegrees = target.R };
        yield return new RobotJointTarget { AxisName = RobotAxisName.B, TargetPositionDegrees = target.B };
        yield return new RobotJointTarget { AxisName = RobotAxisName.T, TargetPositionDegrees = target.T };
    }
}

public sealed record RobotProgramExecutionSnapshot
{
    public string? CurrentProgramName { get; init; }

    public required int CurrentStepIndex { get; init; }

    public required RobotProgramExecutionState State { get; init; }

    public string? LastErrorMessage { get; init; }

    public required int TotalStepCount { get; init; }
}
