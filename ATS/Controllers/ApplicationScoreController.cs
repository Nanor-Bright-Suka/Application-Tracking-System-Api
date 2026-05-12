
using ATS.Data;
using ATS.Dtos.ApplicationScore;
using ATS.Enums;
using ATS.Helpers;
using ATS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Controllers;

[ApiController]
[Route("api/applications/{applicationId}/scores")]
public class ApplicationScoreController : ControllerBase
{
    private readonly ApplicationScoreService _scoreService;
    private readonly AppDbContext _context;

    public ApplicationScoreController(
        ApplicationScoreService scoreService,
        AppDbContext context)
    {
        _scoreService = scoreService;
        _context = context;
    }


        [HttpPut("culture-fit")]
    public async Task<IActionResult> UpsertCultureFitScore(
        Guid applicationId,
        [FromBody] UpsertScoreRequestDto dto)
    {
        return await UpsertScore(
            applicationId,
            ApplicationScoreTypeEnum.CultureFit,
            dto
            );
    }



        [HttpPut("interview")]
    public async Task<IActionResult> UpsertInterviewScore(
        Guid applicationId,
        [FromBody] UpsertScoreRequestDto dto)
    {
        return await UpsertScore(
            applicationId,
            ApplicationScoreTypeEnum.Interview,
            dto);
    }


        [HttpPut("assessment")]
    public async Task<IActionResult> UpsertAssessmentScore(
        Guid applicationId,
        [FromBody] UpsertScoreRequestDto dto)
    {
        return await UpsertScore(
            applicationId,
            ApplicationScoreTypeEnum.Assessment,
            dto);
    }


    
        private async Task<IActionResult> UpsertScore(
        Guid applicationId,
        ApplicationScoreTypeEnum type,
        UpsertScoreRequestDto dto)
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
            var result = await _scoreService.UpsertScoreService(
                applicationId,
                type,
                dto,
                actor.Id);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(
                title: "Failed to upsert score",
                detail: ex.Message,
                statusCode: StatusCodes.Status400BadRequest);
        }



    }
}























