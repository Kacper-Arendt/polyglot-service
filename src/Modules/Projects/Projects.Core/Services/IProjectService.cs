using Projects.Core.Dtos;

namespace Projects.Core.Services;

public interface IProjectService
{
    Task<ProjectToReadDto> GetByAsync(Guid id);
    Task<IEnumerable<ProjectToReadDto>> GetAllAsync(string? searchName);
    Task<Guid> CreateAsync(ProjectToCreateDto project);
    Task UpdateAsync(Guid id, ProjectToUpdateDto project);
    Task UpdateBaseLanguageAsync(Guid id, Guid baseLanguage);
    Task DeleteAsync(Guid id);
}