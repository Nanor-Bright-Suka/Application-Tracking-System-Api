


using System.ComponentModel.DataAnnotations;
using ATS.Enums;

namespace ATS.Dtos.Applications;

public class UpdateApplicationStageRequestDto
{
    [Required]
    public ApplicationStageEnum Stage { get; set; }

    public string? Reason { get; set; }
}   