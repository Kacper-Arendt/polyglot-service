using Translations.Core.Entities;

namespace Translations.Core.Repositories;

public interface ITranslationKeyRepository
{
    Task<TranslationKey?> GetByIdAsync(Guid id);
    Task<IEnumerable<TranslationKey>> GetAllAsync(Guid projectId);
    Task<Guid> CreateAsync(TranslationKey translationKey);
    Task UpdateAsync(TranslationKey translationKey);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}