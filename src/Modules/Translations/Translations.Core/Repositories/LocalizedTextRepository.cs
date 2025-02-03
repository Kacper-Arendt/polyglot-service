using Microsoft.EntityFrameworkCore;
using Translations.Core.Database;
using Translations.Core.Entities;
using Translations.Core.Exceptions;

namespace Translations.Core.Repositories;

public class LocalizedTextRepository : ILocalizedTextRepository
{
    private readonly TranslationsEfContext _context;

    public LocalizedTextRepository(TranslationsEfContext context)
    {
        _context = context;
    }
    
    public async Task<LocalizedText?> GetByAsync(Guid id)
    {
        return await _context.LocalizedTexts
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<LocalizedText>> GetAllAsync(Guid translationKeyId)
    {
        return await _context.LocalizedTexts
            .AsNoTracking()
            .Where(x => x.TranslationKeyId == translationKeyId)
            .ToListAsync();
    }

    public async Task<Guid> CreateAsync(LocalizedText localizedText)
    {
        await _context.LocalizedTexts.AddAsync(localizedText);
        await _context.SaveChangesAsync();
        return localizedText.Id;
    }

    public async Task UpdateAsync(LocalizedText localizedText)
    {
        _context.LocalizedTexts.Update(localizedText);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var localizedText = await _context.LocalizedTexts.FindAsync(id);
        
        if (localizedText is null)
        {
            throw new LocalizedTextNotFoundException(id);
        }

        _context.LocalizedTexts.Remove(localizedText);
        await _context.SaveChangesAsync();
    }
}