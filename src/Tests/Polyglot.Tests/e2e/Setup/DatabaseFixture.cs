using Languages.Core.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Projects.Core.Database;
using Shared.Infrastructure.SqlServer;
using Testcontainers.MsSql;
using Translations.Core.Database;
using Users.Core.Database;

namespace Polyglot.Tests.e2e.Setup;

public class DatabaseFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer;
    private string _testConnectionString;

    public DatabaseFixture()
    {
        _dbContainer = new MsSqlBuilder().Build();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _testConnectionString = _dbContainer.GetConnectionString();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var testConfig = new Dictionary<string, string?>
            {
                { "ConnectionStrings:DefaultConnection", _testConnectionString }
            };

            config.AddInMemoryCollection(testConfig);
        });

        builder.ConfigureServices(services =>
        {
            services.AddSqlServerWithEfCore<ProjectsEfContext>(_testConnectionString);
            services.AddSqlServerWithEfCore<LanguagesEfContext>(_testConnectionString);
            services.AddSqlServerWithEfCore<TranslationsEfContext>(_testConnectionString);
            services.AddSqlServerWithEfCore<UsersEfContext>(_testConnectionString);
        });

        return base.CreateHost(builder);
    }
}
