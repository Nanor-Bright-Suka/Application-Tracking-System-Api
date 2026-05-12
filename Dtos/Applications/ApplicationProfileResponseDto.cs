




using ATS.Enums;

namespace ATS.Dtos.Applications;

public class ApplicationProfileResponseDto
{
    public Guid Id { get; set; }
    public Guid JobId { get; set; }

    public string CandidateName { get; set; } = string.Empty;
    public string CandidateEmail { get; set; } = string.Empty;

    public ApplicationStageEnum Stage { get; set; }

    public List<ScoreDto> Scores { get; set; } = [];
    public List<NoteDto> Notes { get; set; } = [];
    public List<StageHistoryDto> StageHistory { get; set; } = [];
}

public class ScoreDto
{
    public ApplicationScoreTypeEnum Type { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string SetBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }
}

public class NoteDto
{
    public ApplicationNoteTypeEnum Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class StageHistoryDto
{
    public ApplicationStageEnum From { get; set; }
    public ApplicationStageEnum To { get; set; }
    public string ChangedBy { get; set; } = string.Empty;
    public string? Reason { get; set; }
    public DateTime ChangedAt { get; set; }
}