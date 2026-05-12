



using ATS.Enums;

namespace ATS.Entities;


public class StageHistory
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }
    public Application Application { get; set; } = null!;

    public ApplicationStageEnum FromStage { get; set; }
    public ApplicationStageEnum ToStage { get; set; }

    public Guid ChangedBy { get; set; }

    public DateTime ChangedAt { get; set; }

    public string? Reason { get; set; }
}