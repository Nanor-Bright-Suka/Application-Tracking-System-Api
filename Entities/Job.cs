
using ATS.Enums;


namespace ATS.Entities;



public class Job
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public JobStatusEnum Status { get; set; }
}