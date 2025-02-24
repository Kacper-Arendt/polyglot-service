using Languages.Core.Repositories;
using Languages.Shared;
using Languages.Shared.Dto;

namespace Languages.Core.Services;

public class LanguagesModuleApi: ILanguagesModuleApi
{
    private readonly ILanguagesRepository _languagesRepository;

    public LanguagesModuleApi(ILanguagesRepository languagesRepository)
    {
        _languagesRepository = languagesRepository;
    }
    
    public async Task<LanguageDto?> GetAsync(Guid id)
    {
        var language = await _languagesRepository.GetByIdAsync(id);
        
        var languageDto = language is null
            ? null
            : new LanguageDto(
                language.Id,
                language.Name,
                language.Code,
                language.ProjectId
            );
        
        return languageDto;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _languagesRepository.ExistsAsync(id);
    }
}