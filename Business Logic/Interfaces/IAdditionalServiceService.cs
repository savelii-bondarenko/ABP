using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces;

public interface IAdditionalServiceService
{
    Task<IEnumerable<AdditionalServiceDto>> GetAllAsync();
}
