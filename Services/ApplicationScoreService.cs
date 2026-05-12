



using ATS.Data;
using ATS.Dtos.ApplicationScore;
using ATS.Entities;
using ATS.Enums;
using Microsoft.EntityFrameworkCore;

namespace ATS.Services;

public class ApplicationScoreService
{
    private readonly AppDbContext _context;

    public ApplicationScoreService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UpsertScoreResponseDto> UpsertScoreService(
        Guid applicationId,
        ApplicationScoreTypeEnum type,
        UpsertScoreRequestDto dto,
        Guid teamMemberId)
    {
        var application = await _context.Applications
    .FirstOrDefaultAsync(a => a.Id == applicationId) ?? throw new Exception("Application not found");

        var teamMember = await _context.TeamMembers
    .FirstOrDefaultAsync(t => t.Id == teamMemberId) ?? throw new Exception("Invalid team member");


        var existingScore = await _context.ApplicationScores
        .FirstOrDefaultAsync(s =>
            s.ApplicationId == applicationId &&
            s.Type == type);

        if (existingScore != null)
        {
            existingScore.Score = dto.Score;
            existingScore.Comment = dto.Comment;
            existingScore.TeamMemberId = teamMemberId;
             existingScore.UpdatedAt = DateTime.UtcNow;
             existingScore.UpdatedBy = teamMemberId;

        }
        else
        {
            existingScore = new ApplicationScore
            {
                Id = Guid.NewGuid(),
                ApplicationId = applicationId,
                Type = type,
                Score = dto.Score,
                Comment = dto.Comment,
                TeamMemberId = teamMemberId,
                CreatedAt = DateTime.UtcNow
            };

            _context.ApplicationScores.Add(existingScore);
        }
          await _context.SaveChangesAsync();
        return new UpsertScoreResponseDto
        {
            Id = existingScore.Id,
            ApplicationId = existingScore.ApplicationId,
            Type = existingScore.Type,
            Score = existingScore.Score,
            Comment = existingScore.Comment,
            TeamMemberId = existingScore.TeamMemberId,
            CreatedAt = existingScore.CreatedAt
        };
    }
}











