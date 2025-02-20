using Microsoft.EntityFrameworkCore;
using Projects.Core.Database;
using Projects.Core.Entities;
using Projects.Core.Exceptions;

namespace Projects.Core.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ProjectsEfContext _context;

    public ProjectRepository(ProjectsEfContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Projects.AnyAsync(p => p.Id == id);
    }

    public async Task<Project?> GetByAsync(Guid ownerId, Guid id)
    {
        return await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id && p.Owner == ownerId);
    }

    public async Task<IEnumerable<Project>> GetAllAsync(Guid ownerId, string? searchName)
    {
        return await _context.Projects
            .AsNoTracking()
            .Where(p => p.Owner == ownerId && (searchName == null || p.Name.Contains(searchName)))
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Guid> CreateAsync(Project project)
    {
        var entityEntry = await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
        return entityEntry.Entity.Id;
    }

    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project is null)
        {
            throw new ProjectNotFoundException(id);
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }
}