using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Entities;

namespace BusinessLogic.Mappings;

/// <summary>
/// This class was created for AutoMapper realizations. It uses Profile class for create BookingProfile.
/// </summary>
public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<CreateBookingDto, Booking>();

        CreateMap<Booking, ResponseBookingDto>();

        CreateMap<UpdateBookingDto, Booking>();
    }
}