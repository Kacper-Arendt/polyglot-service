using Translations.Core.Dtos;

namespace Translations.Core.Services;

public interface ILocalizedTextService
{
    Task<LocalizedTextToRead> GetByAsync(Guid id);
    Task<IEnumerable<LocalizedTextToRead>> GetAllAsync(Guid translationKeyId);
    Task<Guid> CreateAsync(LocalizedTextToSet localizedText);
    Task UpdateAsync(Guid id, LocalizedTextToUpdate localizedText);
    Task DeleteAsync(Guid id);
}