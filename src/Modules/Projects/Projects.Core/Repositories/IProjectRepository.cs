using Projects.Core.Entities;
using Shared.Abstractions.ValueObjects;

namespace Projects.Core.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByAsync(OwnerId ownerId, ProjectId id);
    Task<IEnumerable<Project>> GetAllAsync(OwnerId ownerId);
    Task<ProjectId> CreateAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(ProjectId id);
}