using Microsoft.EntityFrameworkCore;
using ATS.Data;
using ATS.Dtos.Jobs;
using ATS.Entities;
using ATS.Enums;


namespace ATS.Services;
public class JobService
{
    private readonly AppDbContext _context;

    public JobService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<JobResponseDto> CreateJobService(CreateJobRequestDto dto)
  {
    var job = new Job
    {
        Id = Guid.NewGuid(),
        Title = dto.Title,
        Description = dto.Description,
        Location = dto.Location,
        Status = dto.Status
    };

    _context.Jobs.Add(job);
    await _context.SaveChangesAsync();

    return new JobResponseDto
    {
        Id = job.Id,
        Title = job.Title,
        Description = job.Description,
        Location = job.Location,
        Status = job.Status
    };
  }


    public async Task<JobResponseDto?> GetJobByIdService(Guid id)
   {
    var job = await _context.Jobs.FindAsync(id);

    if (job == null)
        return null;

    return new JobResponseDto
    {
        Id = job.Id,
        Title = job.Title,
        Description = job.Description,
        Location = job.Location,
        Status = job.Status
    };
   }

    public async Task<PagedResultDto<JobResponseDto>> GetJobsService(
        JobStatusEnum? status,
        int page,
        int pageSize)
    {
        var query = _context.Jobs.AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(j => j.Status == status.Value);
        }

        var totalCount = await query.CountAsync();

        var jobs = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(j => new JobResponseDto
            {
                Id = j.Id,
                Title = j.Title,
                Description = j.Description,
                Location = j.Location,
                Status = j.Status
            })
            .ToListAsync();

        return new PagedResultDto<JobResponseDto>
        {
            Items = jobs,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }



















}