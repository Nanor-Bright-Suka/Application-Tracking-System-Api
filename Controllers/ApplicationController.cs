


using ATS.Helpers;
using ATS.Data;
using ATS.Enums;
using ATS.Services;
using ATS.Dtos.Applications;
using Microsoft.AspNetCore.Mvc;

namespace ATS.Controllers;

[ApiController]
[Route("api/jobs/{jobId}/applications")]
public class ApplicationController : ControllerBase
{
    private readonly ApplicationService _applicationService;
    private readonly AppDbContext _context;

    public ApplicationController(
        ApplicationService applicationService,
        AppDbContext context)
    {
        _applicationService = applicationService;
        _context = context;
    }

    // POST /api/jobs/{jobId}/applications
    // Public-ish endpoint
    // DOES NOT require X-Team-Member-Id
    [HttpPost]
    public async Task<IActionResult> ApplyToJob(
        Guid jobId,
        [FromBody] CreateApplicationRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            var result = await _applicationService.ApplyToJobService(jobId, dto);

            return CreatedAtAction(
                nameof(GetApplicationsByJob),
                new { jobId = result.JobId },
                result);
        }
        catch (Exception ex)
        {
            return Problem(
                title: "Application Failed",
                detail: ex.Message,
                statusCode: StatusCodes.Status400BadRequest);
        }
    }

    // GET /api/jobs/{jobId}/applications?stage=Screening
    [HttpGet]
    public async Task<IActionResult> GetApplicationsByJob(
        Guid jobId,
        [FromQuery] ApplicationStageEnum? stage)
    {
        var result = await _applicationService
            .GetApplicationsByJobService(jobId, stage);

        return Ok(result);
    }



   [HttpPatch("/api/applications/{id}/stage")]
    public async Task<IActionResult> UpdateApplicationStage(
    Guid id,
    [FromBody] UpdateApplicationStageRequestDto dto)
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
            var result = await _applicationService
                .UpdateApplicationStageService(id, dto, actor.Id);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(
                title: "Stage Transition Failed",
                detail: ex.Message,
                statusCode: StatusCodes.Status400BadRequest);
        }
    }











}