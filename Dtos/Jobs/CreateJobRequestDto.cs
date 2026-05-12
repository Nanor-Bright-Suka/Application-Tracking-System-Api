

using System.ComponentModel.DataAnnotations;
using ATS.Enums;

namespace ATS.Dtos.Jobs;




public class CreateJobRequestDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;

    public JobStatusEnum Status { get; set; } = JobStatusEnum.Open;
}