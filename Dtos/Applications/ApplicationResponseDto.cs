
using ATS.Enums;


namespace ATS.Dtos.Applications;

public class ApplicationResponseDto
{
public Guid Id { get; set; }

public Guid JobId { get; set; }

public Guid CandidateId { get; set; }

public string CandidateName { get; set; } = string.Empty;

public string CandidateEmail { get; set; } = string.Empty;

public string? CoverLetter { get; set; }

public ApplicationStageEnum Stage { get; set; } = ApplicationStageEnum.Applied;



}
