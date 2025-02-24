using Languages.Shared.Dto;

namespace Languages.Shared;

public interface ILanguagesModuleApi
{
    Task<LanguageDto?> GetAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

