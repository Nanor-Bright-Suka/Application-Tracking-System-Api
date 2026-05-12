
using Microsoft.AspNetCore.Mvc;
using ATS.Services;
using ATS.Data;
using ATS.Dtos.Jobs;
using ATS.Helpers;
using ATS.Enums;

namespace ATS.Controllers;



[ApiController]
[Route("api/jobs")]
public class JobController : ControllerBase
{
    private readonly JobService _jobService;
    private readonly AppDbContext _context;

    public JobController(JobService jobService, AppDbContext context)
    {
        _jobService = jobService;
        _context = context;
    }


    [HttpPost]
   public async Task<IActionResult> CreateJob([FromBody] CreateJobRequestDto dto)
 {
    if (!ModelState.IsValid)
    {
        return ValidationProblem(ModelState);
    }

    var actor = await TeamMemberValidationHelper.ValidateAsync(_context, Request);

    if (actor == null)
    {
        return Problem(
            title: "Invalid Team Member",
            detail: "Missing or invalid X-Team-Member-Id header",
            statusCode: StatusCodes.Status400BadRequest);
    }

    var result = await _jobService.CreateJobService(dto);

    return CreatedAtAction(nameof(GetJobById), new { id = result.Id }, result);
 }



  [HttpGet]
  public async Task<IActionResult> GetJobs(
    [FromQuery] JobStatusEnum? status,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 20)
   {
    if (page <= 0 || pageSize <= 0)
    {
        return Problem(
            title: "Invalid pagination",
            detail: "page and pageSize must be greater than 0",
            statusCode: StatusCodes.Status400BadRequest);
    }

    var result = await _jobService.GetJobsService(status, page, pageSize);

    return Ok(result);
   }


      [HttpGet("{id}")]
    public async Task<IActionResult> GetJobById(Guid id)
  {
    var job = await _jobService.GetJobByIdService(id);

    if (job == null)
    {
        return Problem(
            title: "Not Found",
            detail: $"Job with id {id} does not exist",
            statusCode: StatusCodes.Status404NotFound);
    }

    return Ok(job);
  }













}