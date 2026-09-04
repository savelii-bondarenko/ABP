using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BusinessLogic.Services;

public class AdditionalServiceService(
    IAdditionalServiceRepository repository,
    IMapper mapper) : IAdditionalServiceService
{
    public async Task<IEnumerable<AdditionalServiceDto>> GetAllAsync()
    {
        var services = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<AdditionalServiceDto>>(services);
    }
}
