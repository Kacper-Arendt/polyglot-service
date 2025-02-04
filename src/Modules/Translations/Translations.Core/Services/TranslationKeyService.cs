using Translations.Core.Dtos;
using Translations.Core.Entities;
using Translations.Core.Exceptions;
using Translations.Core.Repositories;

namespace Translations.Core.Services;

public class TranslationKeyService : ITranslationKeyService
{
    private readonly ITranslationKeyRepository _translationKeyRepository;

    public TranslationKeyService(ITranslationKeyRepository translationKeyRepository)
    {
        _translationKeyRepository = translationKeyRepository;
    }

    public async Task<TranslationKeyToRead> GetByAsync(Guid id)
    {
        var translationKey = await _translationKeyRepository.GetByIdAsync(id);
        var translationKeyToRead = new TranslationKeyToRead(translationKey.Id, translationKey.Name);
        return translationKeyToRead;
    }

    public async Task<IEnumerable<TranslationKeyToRead>> GetAllAsync(Guid projectId)
    {
        var translationKeys = await _translationKeyRepository.GetAllAsync(projectId);
        var translationKeysToRead = translationKeys.Select(x => new TranslationKeyToRead(x.Id, x.Name));
        return translationKeysToRead;
    }

    public async Task<Guid> CreateAsync(TranslationKeyToSet translationKey)
    {
        var translationKeyToCreate = TranslationKey.Create(translationKey.Name, translationKey.ProjectId);
        return await _translationKeyRepository.CreateAsync(translationKeyToCreate);
    }

    public async Task UpdateAsync(Guid id, TranslationKeyToUpdate translationKeyToUpdate)
    {
        var translationKey = await _translationKeyRepository.GetByIdAsync(id);

        if (translationKey is null)
        {
            throw new TranslationKeyNotFoundException(id);
        }
        
        translationKey.Update(translationKeyToUpdate.Name);
        await _translationKeyRepository.UpdateAsync(translationKey);
    }

    public async Task DeleteAsync(Guid id)
    {
        var translationKey = await _translationKeyRepository.GetByIdAsync(id);

        if (translationKey is null)
        {
            throw new TranslationKeyNotFoundException(id);
        }

        await _translationKeyRepository.DeleteAsync(id);
    }
}