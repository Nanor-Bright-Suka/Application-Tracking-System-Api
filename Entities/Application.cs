using ATS.Enums;
namespace ATS.Entities;


public class Application
{
    public Guid Id { get; set; }

    public Guid JobId { get; set; }
    public Job Job { get; set; } = null!;

    public Guid CandidateId { get; set; }
    public Candidate Candidate { get; set; } = null!;

    public ApplicationStageEnum Stage { get; set; }

}