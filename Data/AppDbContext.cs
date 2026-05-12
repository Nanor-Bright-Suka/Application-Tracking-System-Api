using ATS.Enums;
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

        modelBuilder.Entity<TeamMember>().HasData(
            new TeamMember
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Alice Recruiter",
                Email = "alice@company.com",
                Role = RoleEnum.Recruiter
            },
            new TeamMember
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Bob Hiring Manager",
                Email = "bob@company.com",
                Role = RoleEnum.HiringManager
            },
            new TeamMember
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Charlie Recruiter",
                Email = "charlie@company.com",
                Role = RoleEnum.Recruiter
            }
        );

        modelBuilder.Entity<Candidate>()
             .HasIndex(c => c.Email)
             .IsUnique();


        modelBuilder.Entity<Application>()
            .HasOne(a => a.Job)
            .WithMany()
            .HasForeignKey(a => a.JobId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Application>()
            .HasOne(a => a.Candidate)
            .WithMany()
            .HasForeignKey(a => a.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Application>()
            .HasIndex(a => new { a.JobId, a.CandidateId })
            .IsUnique();

        modelBuilder.Entity<Candidate>()
        .HasIndex(c => c.Email)
        .IsUnique();

        modelBuilder.Entity<Application>()
        .HasIndex(a => new { a.JobId, a.CandidateId })
        .IsUnique();


        modelBuilder.Entity<ApplicationScore>()
       .HasIndex(s => new { s.ApplicationId, s.Type })
       .IsUnique();

    }



}

