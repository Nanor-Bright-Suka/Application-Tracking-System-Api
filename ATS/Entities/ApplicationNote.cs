

using ATS.Enums;
namespace ATS.Entities;

public class ApplicationNote
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }
    public Application Application { get; set; } = null!;

    public ApplicationNoteTypeEnum Type { get; set; }

    public string Description { get; set; } = string.Empty;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }
}