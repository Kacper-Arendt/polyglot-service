using Projects.Core.Dtos;
using Shared.Abstractions.ValueObjects;

namespace Projects.Core.Services;

public interface IProjectService
{
    Task<ProjectToReadDto> GetByAsync(ProjectId id);
    Task<IEnumerable<ProjectToReadDto>> GetAllAsync();
    Task<Guid> CreateAsync(ProjectToCreateDto project);
    Task UpdateAsync(ProjectId id, ProjectToUpdateDto project);
    Task UpdateBaseLanguageAsync(ProjectId id, LanguageId baseLanguage);
    Task DeleteAsync(ProjectId id);
}