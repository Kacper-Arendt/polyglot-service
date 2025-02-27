using Microsoft.EntityFrameworkCore;
using Organizations.Core.Entities;

namespace Organizations.Core.Database;

public class OrganizationsEfContext : DbContext
{
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationMember> OrganizationMembers { get; set; }

    public OrganizationsEfContext(DbContextOptions<OrganizationsEfContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Organizations");

        modelBuilder.Entity<Organization>(x =>
        {
            x.ToTable("Organizations");
            x.HasKey(c => c.Id);
            x.Property(c => c.Name).IsRequired();
        });
        
        modelBuilder.Entity<OrganizationMember>(x =>
        {
            x.ToTable("OrganizationMembers");
            x.HasKey(c => c.Id);
            x.Property(c => c.OrganizationId).IsRequired();
            x.Property(c => c.UserId).IsRequired();
            x.Property(c => c.Role).IsRequired();
            x.Property(c => c.Email).IsRequired();
            
            x.HasIndex(c => c.OrganizationId);
            x.HasIndex(c => c.UserId);
        });
    }
}