using Languages.Core.Dtos;

namespace Languages.Core.Services;

public interface ILanguagesService
{
    Task<LanguageToReadDto> GetByAsync(Guid id);
    Task<IEnumerable<LanguageToReadDto>> GetAllAsync(Guid projectId);
    Task<Guid> CreateAsync(LanguageToSetDto language);
    Task UpdateAsync(Guid id, LanguageToSetDto language);
    Task DeleteAsync(Guid id);
}