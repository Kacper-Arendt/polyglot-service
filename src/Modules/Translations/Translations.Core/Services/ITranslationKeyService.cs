using Translations.Core.Dtos;

namespace Translations.Core.Services;

public interface ITranslationKeyService
{
  Task<TranslationKeyToRead> GetByAsync(Guid id);
  Task<IEnumerable<TranslationKeyToRead>> GetAllAsync(Guid projectId);
  Task<Guid> CreateAsync(TranslationKeyToSet translationKey);
  Task UpdateAsync(Guid id, TranslationKeyToUpdate translationKey);
  Task DeleteAsync(Guid id);
}