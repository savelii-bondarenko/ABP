using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces;

/// <summary>
/// Defines business logic operations for additional services management.
/// </summary>
public interface IAdditionalServiceService
{
    /// <summary>
    /// Retrieves all available additional services in the system.
    /// </summary>
    /// <returns>A collection of additional service DTOs.</returns>
    Task<IEnumerable<AdditionalServiceDto>> GetAllAsync();
}
