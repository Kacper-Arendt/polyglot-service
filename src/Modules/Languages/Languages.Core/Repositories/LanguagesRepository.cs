using Languages.Core.Database;
using Languages.Core.Entities;
using Languages.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Languages.Core.Repositories;

public class LanguagesRepository : ILanguagesRepository
{
    private readonly LanguagesEfContext _context;

    public LanguagesRepository(LanguagesEfContext context)
    {
        _context = context;
    }
    
    public async Task<Language?> GetByIdAsync(Guid id)
    {
        return await _context.Languages.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Language>> GetAllAsync(Guid projectId)
    {
        return await _context.Languages
            .AsNoTracking()
            .Where(p => p.ProjectId == projectId)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task AddAsync(Language language)
    {
        await _context.Languages.AddAsync(language);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Language language)
    {
        _context.Languages.Update(language);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var language = await _context.Languages.FindAsync(id);
        
        if (language is null)
        {
            throw new LanguageNotFoundException(id);
        }

        _context.Languages.Remove(language);
        await _context.SaveChangesAsync();
    }
}