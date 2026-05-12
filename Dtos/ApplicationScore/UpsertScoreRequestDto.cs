




using System.ComponentModel.DataAnnotations;

namespace ATS.Dtos.ApplicationScore;

public class UpsertScoreRequestDto
{
    [Range(1, 5)]
    public int Score { get; set; }

    public string Comment { get; set; } = string.Empty;
}