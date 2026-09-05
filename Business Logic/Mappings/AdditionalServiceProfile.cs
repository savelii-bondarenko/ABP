using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Entities;

namespace BusinessLogic.Mappings;

/// <summary>
/// Configures AutoMapper mappings for additional services. 
/// Extends the AutoMapper Profile class.
/// </summary>
public class AdditionalServiceProfile : Profile
{
    public AdditionalServiceProfile()
    {
        CreateMap<AdditionalService, AdditionalServiceDto>();

        CreateMap<CreateAdditionalServiceDto, AdditionalService>();

        CreateMap<UpdateAdditionalServiceDto, AdditionalService>();
    }
}
