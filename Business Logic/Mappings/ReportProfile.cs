using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Entities;

namespace BusinessLogic.Mappings;

/// <summary>
/// AutoMapper profile for report-related mappings.
/// </summary>
public class ReportProfile : Profile
{
    public ReportProfile()
    {
        CreateMap<Room, RoomRevenueDto>()
            .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Name))
            // Ці поля розраховуються динамічно, тому ігноруємо їх при базовому маппінгу
            .ForMember(dest => dest.TotalBookings, opt => opt.Ignore())
            .ForMember(dest => dest.TotalRevenue, opt => opt.Ignore());
    }
}
