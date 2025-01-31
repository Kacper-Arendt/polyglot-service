using Microsoft.EntityFrameworkCore;
using Projects.Core.Database;
using Projects.Core.Entities;
using Projects.Core.Exceptions;
using Shared.Abstractions.ValueObjects;

namespace Projects.Core.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ProjectsEfContext _context;

    public ProjectRepository(ProjectsEfContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetByAsync(OwnerId ownerId, ProjectId id)
    {
        return await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id && p.Owner == ownerId);
    }

    public async Task<IEnumerable<Project>> GetAllAsync(OwnerId ownerId)
    {
        return await _context.Projects
            .Where(p => p.Owner == ownerId)
            .ToListAsync();
    }

    public async Task<ProjectId> CreateAsync(Project project)
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

    public async Task DeleteAsync(ProjectId id)
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