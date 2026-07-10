namespace Robotics.ReferenceClient.Reporting;

internal sealed class ConsoleSnapshotPrinter
{
    public void Print(string timing, string target, SnapshotReport report)
    {
        Console.WriteLine();
        Console.WriteLine($"Snapshot {timing} {target}:");

        foreach (SnapshotSectionReport section in report.Sections)
        {
            Console.WriteLine();
            Console.WriteLine($"{section.Name}:");

            if (section.Values.Count == 0)
            {
                Console.WriteLine("- no snapshot values discovered");
                continue;
            }

            foreach (SnapshotValueReport value in section.Values)
            {
                string dataType = value.DataType is null ? string.Empty : $" | DataType={value.DataType}";
                string valueRank = value.ValueRank is null ? string.Empty : $" | ValueRank={value.ValueRank}";
                string sourceTimestamp = value.SourceTimestamp is null ? string.Empty : $" | SourceTimestamp={value.SourceTimestamp:O}";
                string serverTimestamp = value.ServerTimestamp is null ? string.Empty : $" | ServerTimestamp={value.ServerTimestamp:O}";
                string evidence = value.Heuristic ? " | Discovery=heuristic" : string.Empty;

                Console.WriteLine(
                    $"- {value.Label} | NodeId={value.NodeId}{dataType}{valueRank} | Status={value.StatusCode}{sourceTimestamp}{serverTimestamp} | Value={value.Value}{evidence}");
            }
        }
    }
}
