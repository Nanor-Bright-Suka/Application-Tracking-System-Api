
using ATS.Helpers;
using ATS.Data;
using ATS.Dtos.Applications;
using ATS.Entities;
using ATS.Enums;
using Microsoft.EntityFrameworkCore;

namespace ATS.Services;
public class ApplicationService
{
    private readonly AppDbContext _context;

    public ApplicationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationResponseDto> ApplyToJobService(Guid jobId, CreateApplicationRequestDto dto)
    {
       
        var job = await _context.Jobs
            .FirstOrDefaultAsync(j => j.Id == jobId);

        if (job == null)
            throw new Exception("Job not found");

      
        var candidate = await _context.Candidates
            .FirstOrDefaultAsync(c => c.Email == dto.Email);

        if (candidate == null)
        {
            candidate = new Candidate
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email
            };

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
        }

     
        var exists = await _context.Applications
            .AnyAsync(a => a.JobId == jobId && a.CandidateId == candidate.Id);

        if (exists)
            throw new Exception("Candidate already applied for this job");

     
        var application = new Application
        {
            Id = Guid.NewGuid(),
            JobId = jobId,
            CandidateId = candidate.Id,
            CoverLetter = dto.CoverLetter,
            Stage = ApplicationStageEnum.Applied
          
        };

        _context.Applications.Add(application);
        await _context.SaveChangesAsync();

     
        return new ApplicationResponseDto
        {
            Id = application.Id,
            JobId = job.Id,
            CandidateId = candidate.Id,
            CandidateName = candidate.Name,
            CandidateEmail = candidate.Email,
            CoverLetter = application.CoverLetter,
            Stage = application.Stage,
           
        };
    }

    public async Task<List<ApplicationResponseDto>> GetApplicationsByJobService(
    Guid jobId,
    ApplicationStageEnum? stage)
    {
        var query = _context.Applications
        .Include(a => a.Candidate)
        .Where(a => a.JobId == jobId);

        if (stage.HasValue)
        {
            query = query.Where(a => a.Stage == stage.Value);
        }

        var applications = await query.ToListAsync();

        return [.. applications.Select(a => new ApplicationResponseDto
        {
            Id = a.Id,
            JobId = a.JobId,
            CandidateId = a.CandidateId,
            CandidateName = a.Candidate.Name,
            CandidateEmail = a.Candidate.Email,
            CoverLetter = a.CoverLetter,
            Stage = a.Stage,
           
        })];


    }

    public async Task<ApplicationResponseDto> UpdateApplicationStageService(
    Guid applicationId,
    UpdateApplicationStageRequestDto dto,
    Guid teamMemberId)
{

    var application = await _context.Applications
        .Include(a => a.Candidate)
        .FirstOrDefaultAsync(a => a.Id == applicationId);

    if (application == null)
    {
        throw new Exception("Application not found");
    }

  
    var isValidTransition = ApplicationStageValidator.IsValidTransition(
        application.Stage,
        dto.Stage);

    if (!isValidTransition)
    {
        throw new Exception(
            $"Cannot move application from {application.Stage} to {dto.Stage}");
    }

    var previousStage = application.Stage;

    application.Stage = dto.Stage;

    var stageHistory = new StageHistory
    {
        Id = Guid.NewGuid(),
        ApplicationId = application.Id,
        FromStage = previousStage,
        ToStage = dto.Stage,
        ChangedBy = teamMemberId,
        ChangedAt = DateTime.UtcNow,
        Reason = dto.Reason
    };

    _context.StageHistories.Add(stageHistory);


    await _context.SaveChangesAsync();

  
    return new ApplicationResponseDto
    {
        Id = application.Id,
        JobId = application.JobId,
        CandidateId = application.CandidateId,
        CandidateName = application.Candidate.Name,
        CandidateEmail = application.Candidate.Email,
        CoverLetter = application.CoverLetter,
        Stage = application.Stage,
    };
}



    public async Task<ApplicationProfileResponseDto?> GetApplicationProfileService(Guid id)
    {
        var result = await _context.Applications
            .Where(a => a.Id == id)
            .Select(a => new ApplicationProfileResponseDto
            {
                Id = a.Id,
                JobId = a.JobId,
                CandidateName = a.Candidate.Name,
                CandidateEmail = a.Candidate.Email,
                Stage = a.Stage,

                // SCORES
                Scores = _context.ApplicationScores
                    .Where(s => s.ApplicationId == a.Id)
                    .Select(s => new ScoreDto
                    {
                        Type = s.Type,
                        Score = s.Score,
                        Comment = s.Comment,
                        SetBy = _context.TeamMembers
                            .Where(t => t.Id == s.TeamMemberId)
                            .Select(t => t.Name)
                            .FirstOrDefault() ?? "Unknown",
                        CreatedAt = s.CreatedAt
                    })
                    .ToList(),

                // NOTES
                Notes = _context.ApplicationNotes
                    .Where(n => n.ApplicationId == a.Id)
                    .Select(n => new NoteDto
                    {
                        Type = n.Type,
                        Description = n.Description,
                        AuthorName = _context.TeamMembers
                            .Where(t => t.Id == n.CreatedBy)
                            .Select(t => t.Name)
                            .FirstOrDefault() ?? "Unknown",
                        CreatedAt = n.CreatedAt
                    })
                    .ToList(),

                // STAGE HISTORY
                StageHistory = _context.StageHistories
                    .Where(h => h.ApplicationId == a.Id)
                    .Select(h => new StageHistoryDto
                    {
                        From = h.FromStage,
                        To = h.ToStage,
                        ChangedBy = _context.TeamMembers
                            .Where(t => t.Id == h.ChangedBy)
                            .Select(t => t.Name)
                            .FirstOrDefault() ?? "Unknown",
                        Reason = h.Reason,
                        ChangedAt = h.ChangedAt
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        return result;
    }

}