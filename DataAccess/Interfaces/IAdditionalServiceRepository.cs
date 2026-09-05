using DataAccess.Entities;

namespace DataAccess.Interfaces;

/// <summary>
/// Defines database operations for managing additional services.
/// </summary>
public interface IAdditionalServiceRepository
{
    /// <summary>
    /// Retrieves all additional services from the database without tracking.
    /// </summary>
    /// <returns>A collection of all additional service entities.</returns>
    Task<IEnumerable<AdditionalService>> GetAllAsync();

    /// <summary>
    /// Retrieves a collection of additional services based on their unique identifiers.
    /// </summary>
    /// <param name="ids">A collection of unique identifiers.</param>
    /// <returns>A collection of matching additional services.</returns>
    Task<IEnumerable<AdditionalService>> GetByIdsAsync(IEnumerable<int> ids);

    /// <summary>
    /// Retrieves a specific additional service by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the service.</param>
    /// <returns>The additional service entity if found; otherwise, null.</returns>
    Task<AdditionalService?> GetByIdAsync(int id);

    /// <summary>
    /// Adds a new additional service to the database and saves changes.
    /// </summary>
    /// <param name="service">The service entity to add.</param>
    /// <returns>The added service entity with generated database properties.</returns>
    Task<AdditionalService> AddAsync(AdditionalService service);

    /// <summary>
    /// Updates an existing additional service in the database.
    /// </summary>
    /// <param name="service">The service entity with updated values.</param>
    Task UpdateAsync(AdditionalService service);

    /// <summary>
    /// Deletes an additional service from the database efficiently.
    /// </summary>
    /// <param name="id">The unique identifier of the service to delete.</param>
    Task DeleteAsync(int id);
}
