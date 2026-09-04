using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BusinessLogic.Services;

/// <summary>
/// Provides business logic operations for managing additional services.
/// </summary>
/// <param name="repository">The additional service repository for data access.</param>
/// <param name="mapper">The AutoMapper instance for object mapping.</param>
public class AdditionalServiceService(
    IAdditionalServiceRepository repository,
    IMapper mapper) : IAdditionalServiceService
{
    /// <inheritdoc/>
    public async Task<IEnumerable<AdditionalServiceDto>> GetAllAsync()
    {
        var services = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<AdditionalServiceDto>>(services);
    }
}
