namespace Robotics.ReferenceClient.Client;

internal sealed record DemoStepResult(
    string StepName,
    bool Succeeded,
    string StatusCode);
