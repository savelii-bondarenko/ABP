using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

/// <summary>
/// Repository for managing additional services in the database.
/// </summary>
/// <param name="context">The application database context.</param>
public class AdditionalServiceRepository(AppDbContext context) : IAdditionalServiceRepository
{
    /// <inheritdoc/>
    public async Task<IEnumerable<AdditionalService>> GetByIdsAsync(IEnumerable<int> ids)
    {
        return await context.AdditionalServices
            .Where(s => ids.Contains(s.Id))
            .ToListAsync();
    }

    public async Task<IEnumerable<AdditionalService>> GetAllAsync()
    {
        return await context.AdditionalServices.AsNoTracking().ToListAsync();
    }
}
