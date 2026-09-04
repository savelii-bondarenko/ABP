using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Entities;

namespace BusinessLogic.Mappings;

/// <summary>
/// This class was created for AutoMapper realizations. It uses Profile class for create AdditionalServiceProfile.
/// </summary>
public class AdditionalServiceProfile : Profile
{
    public AdditionalServiceProfile()
    {
        CreateMap<AdditionalService, AdditionalServiceDto>();
    }
}