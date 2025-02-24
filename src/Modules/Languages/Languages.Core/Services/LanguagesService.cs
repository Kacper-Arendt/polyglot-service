using Languages.Core.Dtos;
using Languages.Core.Entities;
using Languages.Core.Exceptions;
using Languages.Core.Repositories;
using Projects.Shared;
using Shared.Abstractions.Events;
using Shared.Infrastructure.Helpers;

namespace Languages.Core.Services;

public class LanguagesService : ILanguagesService
{
    private readonly ILanguagesRepository _languagesRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IProjectsModuleApi _projectsModuleApi;

    public LanguagesService(ILanguagesRepository languagesRepository, IEventPublisher eventPublisher, IProjectsModuleApi projectsModuleApi)
    {
        _languagesRepository = languagesRepository;
        _eventPublisher = eventPublisher;
        _projectsModuleApi = projectsModuleApi;
    }

    public async Task<LanguageToReadDto> GetByAsync(Guid id)
    {
        var language = await _languagesRepository.GetByIdAsync(id);

        if (language is null)
        {
            throw new LanguageNotFoundException(id);
        }

        var languageToReadDto = new LanguageToReadDto(language.Id, language.Name, language.Code);
        return languageToReadDto;
    }

    public async Task<IEnumerable<LanguageToReadDto>> GetAllAsync(Guid projectId)
    {
        var languages = await _languagesRepository.GetAllAsync(projectId);
        return languages.Select(l => new LanguageToReadDto(l.Id, l.Name, l.Code));
    }

    public async Task<Guid> CreateAsync(LanguageToSetDto language)
    {
        var projectExists = await _projectsModuleApi.ExistsAsync(language.ProjectId);
        
        if (!projectExists)
        {
            throw new ProjectNotFoundException(language.ProjectId);
        }
        
        var languageToSet = Language.Create(
            Guid.NewGuid(),
            language.Name,
            language.Code,
            language.ProjectId
        );
        await _languagesRepository.AddAsync(languageToSet);
        
        var projectExistsEvent = new LanguageCreatedEvent(languageToSet.Id, language.ProjectId, language.Name, language.Code);
        await _eventPublisher.PublishAsync(projectExistsEvent);
        
        return languageToSet.Id;
    }

    public async Task UpdateAsync(Guid id, LanguageToSetDto language)
    {
        var languageToUpdate = await _languagesRepository.GetByIdAsync(id);

        if (languageToUpdate is null)
        {
            throw new LanguageNotFoundException(id);
        }

        languageToUpdate.Update(language.Name, language.Code);
        await _languagesRepository.UpdateAsync(languageToUpdate);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _languagesRepository.DeleteAsync(id);
    }
}