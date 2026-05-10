
using Microsoft.EntityFrameworkCore;
using ATS.Entities;

namespace ATS.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<ApplicationNote> ApplicationNotes { get; set; }
    public DbSet<ApplicationScore> ApplicationScores { get; set; }
    public DbSet<StageHistory> StageHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    base.OnModelCreating(modelBuilder);
    }


}

