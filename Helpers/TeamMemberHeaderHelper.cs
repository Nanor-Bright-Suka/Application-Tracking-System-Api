


namespace ATS.Helpers;

public static class TeamMemberHeaderHelper
{
    public static bool TryGetTeamMemberId(
        HttpRequest request,
        out Guid teamMemberId)
    {
        teamMemberId = Guid.Empty;

        if (!request.Headers.TryGetValue("X-Team-Member-Id", out var headerValue))
        {
            return false;
        }

        return Guid.TryParse(headerValue, out teamMemberId);
    }
}