using ATS.Enums;

namespace ATS.Entities;


public class ApplicationScore
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }
    public Application Application { get; set; } = null!;

    public ApplicationScoreTypeEnum Type { get; set; }

    public int Score { get; set; }

    public string Comment { get; set; } = string.Empty;

    public Guid TeamMemberId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }
}