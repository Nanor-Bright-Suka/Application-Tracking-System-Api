




using ATS.Data;
using ATS.Dtos.ApplicationNote;
using ATS.Enums;
using ATS.Entities;
using Microsoft.EntityFrameworkCore;


namespace ATS.Services;
public class ApplicationNoteService
{
    private readonly AppDbContext _context;

    public ApplicationNoteService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<ApplicationNoteResponseDto> CreateNoteService(
        Guid applicationId,
        CreateApplicationNoteRequestDto dto,
        Guid teamMemberId)
    {

        var application = await _context.Applications
            .FirstOrDefaultAsync(a => a.Id == applicationId) ?? throw new Exception("Application not found");

        var teamMember = await _context.TeamMembers
            .FirstOrDefaultAsync(t => t.Id == teamMemberId) ?? throw new Exception("Invalid team member");

        var note = new ApplicationNote
        {
            Id = Guid.NewGuid(),
            ApplicationId = applicationId,
            Type = dto.Type,
            Description = dto.Description,
            CreatedBy = teamMemberId,
            CreatedAt = DateTime.UtcNow
        };

        _context.ApplicationNotes.Add(note);
        await _context.SaveChangesAsync();

        return new ApplicationNoteResponseDto
        {
            Id = note.Id,
            Type = note.Type,
            Description = note.Description,
            CreatedBy = teamMember.Id,
            CreatedByName = teamMember.Name,
            CreatedAt = note.CreatedAt
        };
    }


        public async Task<List<ApplicationNoteResponseDto>> GetNotesByApplicationService(Guid applicationId)
    {
        var notes = await _context.ApplicationNotes
            .Include(n => n.Application)
            .Where(n => n.ApplicationId == applicationId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

        var teamMemberIds = notes.Select(n => n.CreatedBy).Distinct().ToList();

        var teamMembers = await _context.TeamMembers
            .Where(t => teamMemberIds.Contains(t.Id))
            .ToListAsync();

        return [.. notes.Select(n =>
        {
            var member = teamMembers.FirstOrDefault(t => t.Id == n.CreatedBy);

            return new ApplicationNoteResponseDto
            {
                Id = n.Id,
                Type = n.Type,
                Description = n.Description,
                CreatedBy = n.CreatedBy,
                CreatedByName = member?.Name ?? "Unknown",
                CreatedAt = n.CreatedAt
            };
        })];
    }
}

















