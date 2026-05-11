
using System.ComponentModel.DataAnnotations;

namespace ATS.Dtos.Applications;

public class CreateApplicationRequestDto
{
 [Required]
public string Name { get; set; } = string.Empty;

[Required]
[EmailAddress]
public string Email { get; set; } = string.Empty;

public string? CoverLetter { get; set; }


}
