using Languages.Core.Database;
using Languages.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Polyglot.Tests.e2e.Lanuages;

public class DatabaseFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer;

    public DatabaseFixture()
    {
        _dbContainer = new MsSqlBuilder()
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

    [Fact]
    public async Task Should_Insert_And_Retrieve_Data()
    {
        var options = new DbContextOptionsBuilder<LanguagesEfContext>()
            .UseSqlServer(_dbContainer.GetConnectionString())
            .Options;
        
        await using var context = new LanguagesEfContext(options);
        await context.Database.EnsureCreatedAsync();

        // Arrange
        var entity = Language.Create(Guid.NewGuid(), "123", "123, 123", Guid.NewGuid());
        await context.Languages.AddAsync(entity);
        await context.SaveChangesAsync();
            
        var retrievedEntity = await context.Languages.FindAsync(entity.Id);
        var allLanguages = await context.Languages.ToListAsync();

        // Assert
        Assert.NotNull(retrievedEntity);
        Assert.Equal(entity.Name, retrievedEntity.Name);
    }
    
    
    [Fact]
    public async Task Should_Insert_And_Retrieve_Data2()
    {
        var options = new DbContextOptionsBuilder<LanguagesEfContext>()
            .UseSqlServer(_dbContainer.GetConnectionString())
            .Options;
        
        await using var context = new LanguagesEfContext(options);
        await context.Database.EnsureCreatedAsync();

        // Arrange
        var entity = Language.Create(Guid.NewGuid(), "123", "123, 123", Guid.NewGuid());
        await context.Languages.AddAsync(entity);
        await context.SaveChangesAsync();
            
        var retrievedEntity = await context.Languages.FindAsync(entity.Id);
        var allLanguages = await context.Languages.ToListAsync();

        // Assert
        Assert.NotNull(retrievedEntity);
        Assert.Equal(entity.Name, retrievedEntity.Name);
    }
}
