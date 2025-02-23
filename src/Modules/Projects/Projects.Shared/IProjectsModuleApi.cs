using Projects.Shared.Dto;

namespace Projects.Shared;

public interface IProjectsModuleApi
{
    Task<ProjectDto?> GetAsync(Guid id, Guid ownerId);
    Task<bool> ExistsAsync(Guid id);
}