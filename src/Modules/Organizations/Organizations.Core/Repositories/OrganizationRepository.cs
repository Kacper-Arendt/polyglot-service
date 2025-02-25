using Microsoft.EntityFrameworkCore;
using Organizations.Core.Database;
using Organizations.Core.Entities;
using Organizations.Core.Exceptions;

namespace Organizations.Core.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly OrganizationsEfContext _context;

    public OrganizationRepository(OrganizationsEfContext context)
    {
        _context = context;
    }
    
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Organizations.AnyAsync(x => x.Id == id);
    }

    public async Task AddAsync(Organization? organization)
    {
         await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        return await _context
            .Organizations            
            .ToListAsync();
    }

    public async Task<IEnumerable<Organization>> GetAllAsync(Guid userId)
    {
        var organizations = await _context.Organizations
            .Join(_context.OrganizationMembers, o => o.Id, m => m.OrganizationId, (o, m) => new { o, m })
            .Where(x => x.m.UserId == userId)
            .AsNoTracking()
            .Select(x => x.o)
            .ToListAsync();
        
        return organizations;
    }

    public async Task<Organization?> GetAsync(Guid id)
    {
        return await _context.Organizations.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Organization organization)
    {   
        _context.Organizations.Update(organization);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var organization = await _context.Organizations.FirstOrDefaultAsync(x => x.Id == id);
        if (organization == null)
        {
            throw new OrganizationNotFoundException(id);
        }

        _context.Organizations.Remove(organization);
        await _context.SaveChangesAsync();
    }
}