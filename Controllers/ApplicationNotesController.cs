

using ATS.Data;
using ATS.Dtos.ApplicationNote;
using ATS.Helpers;
using ATS.Services;
using Microsoft.AspNetCore.Mvc;


namespace ATS.Controllers;

[ApiController]
[Route("api/applications/{applicationId}/notes")]
public class ApplicationNoteController : ControllerBase
{
    private readonly ApplicationNoteService _noteService;
    private readonly AppDbContext _context;

    public ApplicationNoteController(
        ApplicationNoteService noteService,
        AppDbContext context)
    {
        _noteService = noteService;
        _context = context;
    }


        [HttpPost]
    public async Task<IActionResult> CreateNote(
        Guid applicationId,
        [FromBody] CreateApplicationNoteRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var actor = await TeamMemberValidationHelper
            .ValidateAsync(_context, Request);

        if (actor == null)
        {
            return Problem(
                title: "Invalid Team Member",
                detail: "Missing or invalid X-Team-Member-Id header",
                statusCode: StatusCodes.Status400BadRequest);
        }

        try
        {
            var result = await _noteService.CreateNoteService(
                applicationId,
                dto,
                actor.Id);

            return CreatedAtAction(
                nameof(GetNotes),
                new { applicationId },
                result);
        }
        catch (Exception ex)
        {
            return Problem(
                title: "Failed to create note",
                detail: ex.Message,
                statusCode: StatusCodes.Status400BadRequest);
        }
    }



        [HttpGet]
    public async Task<IActionResult> GetNotes(Guid applicationId)
    {
        var result = await _noteService
            .GetNotesByApplicationService(applicationId);

        return Ok(result);
    }
}







