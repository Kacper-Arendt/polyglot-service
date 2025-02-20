using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Events;
using Shared.Infrastructure.SqlServer;
using Translations.Core.Database;
using Translations.Core.Events.Handlers;
using Translations.Core.Repositories;
using Translations.Core.Services;

namespace Translations.Core;

public static class Extensions
{
    public static IServiceCollection AddTranslationsCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILocalizedTextRepository, LocalizedTextRepository>();
        services.AddScoped<ITranslationKeyRepository, TranslationKeyRepository>();
        services.AddScoped<ILocalizedTextService, LocalizedTextService>();
        services.AddScoped<ITranslationKeyService, TranslationKeyService>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddSqlServerWithEfCore<TranslationsEfContext>(connectionString);
        
        services.AddScoped<IEventHandler<LanguageCreatedEvent>, LanguageCreatedEventHandler>();

        return services;
    } 
}