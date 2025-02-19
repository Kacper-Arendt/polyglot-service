using Projects.Core.Entities;

namespace Projects.Core.Repositories;

public interface IProjectRepository
{
    Task<bool> ExistsAsync(Guid id);
    Task<Project?> GetByAsync(Guid ownerId, Guid id);
    Task<IEnumerable<Project>> GetAllAsync(Guid ownerId, string? searchName);
    Task<Guid> CreateAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(Guid id);
}