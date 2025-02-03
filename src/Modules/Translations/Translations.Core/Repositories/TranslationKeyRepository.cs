using Microsoft.EntityFrameworkCore;
using Translations.Core.Database;
using Translations.Core.Entities;
using Translations.Core.Exceptions;

namespace Translations.Core.Repositories;

public class TranslationKeyRepository: ITranslationKeyRepository
{
    private readonly TranslationsEfContext _context;

    public TranslationKeyRepository(TranslationsEfContext context)
    {
        _context = context;
    }
    
    public async Task<TranslationKey?> GetByIdAsync(Guid id)
    {
        return await _context.TranslationsKeys.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<TranslationKey>> GetAllAsync(Guid projectId)
    {
        return await _context.TranslationsKeys
            .AsNoTracking()
            .Where(x => x.ProjectId == projectId)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Guid> CreateAsync(TranslationKey translationKey)
    {
        await _context.TranslationsKeys.AddAsync(translationKey);
        await _context.SaveChangesAsync();
        return translationKey.Id;
    }

    public async Task UpdateAsync(TranslationKey translationKey)
    {
        _context.TranslationsKeys.Update(translationKey);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var translationKey = await _context.TranslationsKeys.FindAsync(id);
        
        if (translationKey is null)
        {
            throw new TranslationKeyNotFoundException(id);
        }

        _context.TranslationsKeys.Remove(translationKey);
        await _context.SaveChangesAsync();
    }
}