using Microsoft.EntityFrameworkCore;
using Translations.Core.Entities;

namespace Translations.Core.Database;

public class TranslationsEfContext: DbContext
{
    public DbSet<TranslationKey> TranslationsKeys { get; set; }
    public DbSet<LocalizedText?> LocalizedTexts { get; set; }
    
    public TranslationsEfContext(DbContextOptions<TranslationsEfContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Translations");

        modelBuilder.Entity<LocalizedText>(x =>
        {
            x.ToTable("LocalizedTexts");
            x.HasKey(c => c.Id);
            x.Property(c => c.Value);
            x.Property(c => c.TranslationKeyId).IsRequired();
            x.Property(c => c.LanguageId).IsRequired();

            x.HasIndex(c => new { c.LanguageId, c.TranslationKeyId });
        });

        modelBuilder.Entity<TranslationKey>(x =>
        {
            x.ToTable("TranslationKeys");
            x.HasKey(c => c.Id);
            x.Property(c => c.Name).IsRequired().HasMaxLength(255);
            x.Property(c => c.ProjectId).IsRequired();
            
            x.HasIndex(c => c.ProjectId);
        });
    }
}