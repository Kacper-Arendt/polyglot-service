using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Shared.Infrastructure.Helpers;

public class HttpContextHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        var ownerId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

        if (Guid.TryParse(ownerId?.Value, out var guid))
        {
            return guid;
        }
        
        throw new UnauthorizedAccessException("User is not authenticated");
    }
}