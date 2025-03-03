using System.Net.Http.Headers;
using Polyglot.Tests.e2e.Helpers;
using Polyglot.Tests.e2e.Setup;

namespace Polyglot.Tests.e2e.Modules;

public class OrganizationApiTests : IClassFixture<DatabaseFixture>
{
    private readonly HttpClient _client;

    public OrganizationApiTests(DatabaseFixture fixture)
    {
        _client = fixture.CreateClient();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AuthHelper.AuthenticateAsync(_client).Result.AccessToken);
    }
    
}