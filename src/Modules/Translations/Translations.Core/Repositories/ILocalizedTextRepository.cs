using Translations.Core.Entities;

namespace Translations.Core.Repositories;

public interface ILocalizedTextRepository
{
    Task<LocalizedText?> GetByAsync(Guid id);
    Task<IEnumerable<LocalizedText>> GetAllAsync(Guid translationKeyId);
    Task<Guid> CreateAsync(LocalizedText localizedText);
    Task UpdateAsync(LocalizedText localizedText);
    Task DeleteAsync(Guid id);
}