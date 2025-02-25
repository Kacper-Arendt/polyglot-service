using Microsoft.EntityFrameworkCore;
using Organizations.Core.Database;
using Organizations.Core.Entities;
using Organizations.Core.Exceptions;

namespace Organizations.Core.Repositories;

public class OrganizationMemberRepository : IOrganizationMemberRepository
{
    private readonly OrganizationsEfContext _context;

    public OrganizationMemberRepository(OrganizationsEfContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.OrganizationMembers.AnyAsync(x => x.Id == id);
    }

    public async Task AddAsync(OrganizationMember organizationMember)
    {
        await _context.OrganizationMembers.AddAsync(organizationMember);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrganizationMember>> GetAllAsync(Guid organizationId)
    {
        return await _context.OrganizationMembers
            .Where(x => x.OrganizationId == organizationId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<OrganizationMember?> GetAsync(Guid id)
    {
        return await _context.OrganizationMembers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(OrganizationMember organizationMember)
    {
        _context.OrganizationMembers.Update(organizationMember);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var organizationMember = await _context.OrganizationMembers.FirstOrDefaultAsync(x => x.Id == id);
        
        if (organizationMember == null)
        {
            throw new OrganizationMemberNotFoundException(id);
        }

        _context.OrganizationMembers.Remove(organizationMember);
        await _context.SaveChangesAsync();
    }
}