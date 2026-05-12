



using ATS.Enums;

namespace ATS.Dtos.ApplicationScore;

public class UpsertScoreResponseDto
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }

    public ApplicationScoreTypeEnum Type { get; set; }

    public int Score { get; set; }

    public string Comment { get; set; } = string.Empty;

    public Guid TeamMemberId { get; set; }

    public DateTime CreatedAt { get; set; }
}