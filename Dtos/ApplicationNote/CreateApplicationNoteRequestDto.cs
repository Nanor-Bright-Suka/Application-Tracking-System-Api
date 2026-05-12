




using System.ComponentModel.DataAnnotations;
using ATS.Enums;

namespace ATS.Dtos.ApplicationNote;

public class CreateApplicationNoteRequestDto
{
    [Required]
    public ApplicationNoteTypeEnum Type { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;
}