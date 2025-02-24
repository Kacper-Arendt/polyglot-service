using Languages.Core.Entities;

namespace Languages.Core.Repositories;

public interface ILanguagesRepository
{
    Task<Language?> GetByIdAsync(Guid id);
    Task<IEnumerable<Language>> GetAllAsync(Guid projectId);
    Task AddAsync(Language language);
    Task UpdateAsync(Language language);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}