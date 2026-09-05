using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services;

/// <summary>
/// Provides implementation for additional services business logic.
/// </summary>
/// <param name="repository">The additional service repository instance.</param>
/// <param name="mapper">The AutoMapper instance for DTO mapping.</param>
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

    /// <inheritdoc/>
    public async Task<AdditionalServiceDto?> GetByIdAsync(int id)
    {
        var service = await repository.GetByIdAsync(id);
        return service is not null ? mapper.Map<AdditionalServiceDto>(service) : null;
    }

    /// <inheritdoc/>
    public async Task<AdditionalServiceDto> AddAsync(CreateAdditionalServiceDto dto)
    {
        var service = mapper.Map<AdditionalService>(dto);
        var createdService = await repository.AddAsync(service);
        return mapper.Map<AdditionalServiceDto>(createdService);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(UpdateAdditionalServiceDto dto)
    {
        var service = mapper.Map<AdditionalService>(dto);
        await repository.UpdateAsync(service);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}
