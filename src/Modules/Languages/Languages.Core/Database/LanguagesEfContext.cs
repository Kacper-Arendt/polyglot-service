using Languages.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Languages.Core.Database;

public class LanguagesEfContext: DbContext
{
    public DbSet<Language> Languages { get; set; }
    
    public LanguagesEfContext(DbContextOptions<LanguagesEfContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Languages");

        modelBuilder.Entity<Language>(x =>
        {
            x.ToTable("Languages");
            x.HasKey(c => c.Id);
            x.Property(c => c.Name).IsRequired();
            x.Property(c => c.Code).IsRequired();
            x.Property(c => c.ProjectId).IsRequired();
            
            x.HasIndex(c => c.ProjectId);
        });
    }
}