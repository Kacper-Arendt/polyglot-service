using Microsoft.EntityFrameworkCore;
using Projects.Core.Entities;

namespace Projects.Core.Database;

public class ProjectsEfContext : DbContext
{
    public DbSet<Project> Projects { get; set; }

    public ProjectsEfContext(DbContextOptions<ProjectsEfContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Projects");

        modelBuilder.Entity<Project>(x =>
        {
            x.ToTable("Projects");
            x.HasKey(c => c.Id);
            x.Property(c => c.Name).IsRequired();
            x.Property(c => c.Description);
            x.Property(c => c.BaseLanguage);
            x.Property(c => c.Owner).IsRequired();
            
            x.HasIndex(c => c.Owner);
        });
    }
}