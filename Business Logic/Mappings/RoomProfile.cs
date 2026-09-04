using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Entities;

namespace BusinessLogic.Mappings;

/// <summary>
/// This class was created for AutoMapper realizations. It uses Profile class for create RoomProfile.
/// </summary>
public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomDto>();

        CreateMap<CreateRoomDto, Room>();

        CreateMap<UpdateRoomDto, Room>();

        CreateMap<Room, ResponseRoomDto>();
    }
}