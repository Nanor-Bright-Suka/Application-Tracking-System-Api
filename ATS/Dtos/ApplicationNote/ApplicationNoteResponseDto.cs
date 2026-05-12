

using ATS.Enums;


namespace ATS.Dtos.ApplicationNote;

public class ApplicationNoteResponseDto
{
    public Guid Id { get; set; }

    public ApplicationNoteTypeEnum Type { get; set; }

    public string Description { get; set; } = string.Empty;

    public Guid CreatedBy { get; set; }

    public string CreatedByName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}