using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces;

/// <summary>
/// Defines business logic operations for managing additional services.
/// </summary>
public interface IAdditionalServiceService
{
    /// <summary>
    /// Retrieves all available additional services.
    /// </summary>
    /// <returns>A collection of additional service DTOs.</returns>
    Task<IEnumerable<AdditionalServiceDto>> GetAllAsync();

    /// <summary>
    /// Retrieves a specific additional service by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the service.</param>
    /// <returns>A DTO containing service details, or null if the service was not found.</returns>
    Task<AdditionalServiceDto?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new additional service.
    /// </summary>
    /// <param name="dto">The data transfer object containing new service details.</param>
    /// <returns>A DTO containing the created service details.</returns>
    Task<AdditionalServiceDto> AddAsync(CreateAdditionalServiceDto dto);

    /// <summary>
    /// Updates an existing additional service.
    /// </summary>
    /// <param name="dto">The data transfer object containing updated service details.</param>
    Task UpdateAsync(UpdateAdditionalServiceDto dto);

    /// <summary>
    /// Deletes an additional service by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the service to delete.</param>
    Task DeleteAsync(int id);
}
