using DataAccess.Entities;

namespace DataAccess.Interfaces;

/// <summary>
/// Defines database operations for additional services.
/// </summary>
public interface IAdditionalServiceRepository
{
    /// <summary>
    /// Retrieves a collection of additional services based on their unique identifiers.
    /// </summary>
    /// <param name="ids">A collection of unique identifiers.</param>
    /// <returns>A collection of matching additional services.</returns>
    Task<IEnumerable<AdditionalService>> GetByIdsAsync(IEnumerable<int> ids);

    /// <summary>
    /// Retrieves all available additional services from the database.
    /// </summary>
    /// <returns>A collection of all additional services.</returns>
    Task<IEnumerable<AdditionalService>> GetAllAsync();
}
