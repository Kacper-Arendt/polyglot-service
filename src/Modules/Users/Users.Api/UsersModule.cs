using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Users.Core;
using Users.Core.Database;
using Users.Core.Entities;

namespace Users.Api;

public static class UsersModule
{
    public static WebApplicationBuilder RegisterUsersModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddUsersCore(builder.Configuration); 
        
        builder.Services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<UsersEfContext>()
            .AddApiEndpoints();
        
        builder.Services.AddIdentityApiEndpoints<User>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(2);
            })
            .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<UsersEfContext>();
        
        return builder;
    }

    public static WebApplication UseUsersModule(this WebApplication app)
    {
        app
            .MapGroup("api/auth")
            .MapIdentityApi<User>();

        return app;
    }
}