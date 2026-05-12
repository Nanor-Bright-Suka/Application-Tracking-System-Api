

using Microsoft.EntityFrameworkCore;
using ATS.Data;
using ATS.Entities;
namespace ATS.Helpers;

public static class TeamMemberValidationHelper
{
    public static async Task<TeamMember?> ValidateAsync(
        AppDbContext context,
        HttpRequest request)
    {
        if (!TeamMemberHeaderHelper.TryGetTeamMemberId(
            request,
            out var teamMemberId))
        {
            return null;
        }

        return await context.TeamMembers
            .FirstOrDefaultAsync(t => t.Id == teamMemberId);
    }
}