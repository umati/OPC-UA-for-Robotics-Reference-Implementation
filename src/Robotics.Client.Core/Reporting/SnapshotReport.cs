using Opc.Ua;

namespace Robotics.Client.Core.Reporting;

public sealed record SnapshotReport(IReadOnlyList<SnapshotSectionReport> Sections);

public sealed record SnapshotSectionReport(
    string Name,
    IReadOnlyList<SnapshotValueReport> Values);

public sealed record SnapshotValueReport(
    string Label,
    string BrowseName,
    string NodeId,
    string? DataType,
    int? ValueRank,
    string StatusCode,
    DateTime? SourceTimestamp,
    DateTime? ServerTimestamp,
    string Value,
    bool Heuristic);

public sealed record SnapshotNode(
    string SectionName,
    string Label,
    string BrowseName,
    NodeId NodeId,
    bool Heuristic);
