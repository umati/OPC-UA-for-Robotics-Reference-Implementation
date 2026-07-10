using Opc.Ua;

namespace Robotics.ReferenceClient.Reporting;

internal sealed record SnapshotReport(IReadOnlyList<SnapshotSectionReport> Sections);

internal sealed record SnapshotSectionReport(
    string Name,
    IReadOnlyList<SnapshotValueReport> Values);

internal sealed record SnapshotValueReport(
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

internal sealed record SnapshotNode(
    string SectionName,
    string Label,
    string BrowseName,
    NodeId NodeId,
    bool Heuristic);
