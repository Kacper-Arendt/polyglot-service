using Languages.Shared;
using Translations.Core.Dtos;
using Translations.Core.Entities;
using Translations.Core.Exceptions;
using Translations.Core.Repositories;

namespace Translations.Core.Services;

public class LocalizedTextService : ILocalizedTextService
{
    private readonly ILocalizedTextRepository _localizedTextRepository;
    private readonly ILanguagesModuleApi _languagesModuleApi;

    public LocalizedTextService(ILocalizedTextRepository localizedTextRepository, ILanguagesModuleApi languagesModuleApi)
    {
        _localizedTextRepository = localizedTextRepository;
        _languagesModuleApi = languagesModuleApi;
    }

    public async Task<LocalizedTextToRead> GetByAsync(Guid id)
    {
        var localizedText = await _localizedTextRepository.GetByAsync(id);

        if (localizedText is null)
        {
            throw new LocalizedTextNotFoundException(id);
        }

        var localizedTextToRead = new LocalizedTextToRead(
            localizedText.Id,
            localizedText.Value,
            localizedText.TranslationKeyId,
            localizedText.LanguageId
        );
        return localizedTextToRead;
    }

    public async Task<IEnumerable<LocalizedTextToRead>> GetAllAsync(Guid translationKeyId)
    {
        var localizedTexts = await _localizedTextRepository.GetAllAsync(translationKeyId);
        var localizedTextsToRead = localizedTexts.Select(x => new LocalizedTextToRead(
            x.Id,
            x.Value,
            x.TranslationKeyId,
            x.LanguageId
        ));
        return localizedTextsToRead;
    }

    public async Task<Guid> CreateAsync(LocalizedTextToSet localizedText)
    {
        var languageExists = await _languagesModuleApi.ExistsAsync(localizedText.LanguageId);
        
        if (!languageExists)
        {
            throw new LanguageNotFoundException(localizedText.LanguageId);
        }
        
        var localizedTextToCreate = LocalizedText.Create(
            localizedText.Value,
            localizedText.TranslationKeyId,
            localizedText.LanguageId);

        return await _localizedTextRepository.CreateAsync(localizedTextToCreate);
    }

    public async Task UpdateAsync(Guid id, LocalizedTextToUpdate localizedText)
    {
        var localizedTextToUpdate = await _localizedTextRepository.GetByAsync(id);

        if (localizedTextToUpdate is null)
        {
            throw new LocalizedTextNotFoundException(id);
        }

        localizedTextToUpdate.Update(localizedText.Value);
        await _localizedTextRepository.UpdateAsync(localizedTextToUpdate);
    }

    public async Task DeleteAsync(Guid id)
    {
        var localizedText = await _localizedTextRepository.GetByAsync(id);

        if (localizedText is null)
        {
            throw new LocalizedTextNotFoundException(id);
        }

        await _localizedTextRepository.DeleteAsync(id);
    }
}