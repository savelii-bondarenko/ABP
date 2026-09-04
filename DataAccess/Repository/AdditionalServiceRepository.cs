using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

/// <summary>
/// Repository class for the AdditionalService entity. Handles all direct database operations.
/// </summary>
/// <param name="context">The application database context.</param>
public class AdditionalServiceRepository(AppDbContext context) : IAdditionalServiceRepository
{
    /// <inheritdoc/>
    public async Task<IEnumerable<AdditionalService>> GetAllAsync()
    {
        return await context.AdditionalServices.AsNoTracking().ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<AdditionalService>> GetByIdsAsync(IEnumerable<int> ids)
    {
        return await context.AdditionalServices.Where(s => ids.Contains(s.Id)).ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<AdditionalService?> GetByIdAsync(int id)
    {
        return await context.AdditionalServices.FindAsync(id);
    }

    /// <inheritdoc/>
    public async Task<AdditionalService> AddAsync(AdditionalService service)
    {
        await context.AdditionalServices.AddAsync(service);
        await context.SaveChangesAsync();
        return service;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(AdditionalService service)
    {
        context.AdditionalServices.Update(service);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id)
    {
        await context.AdditionalServices.Where(s => s.Id == id).ExecuteDeleteAsync();
    }
}
