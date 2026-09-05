using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services;

/// <summary>
/// Provides implementation for room management business logic.
/// </summary>
/// <param name="repository">The room repository instance.</param>
/// <param name="mapper">The AutoMapper instance for DTO mapping.</param>
public class RoomService(IRoomRepository repository, IMapper mapper) : IRoomService
{
    /// <inheritdoc/>
    public async Task<ResponseRoomDto> AddAsync(CreateRoomDto roomDto)
    {
        ArgumentNullException.ThrowIfNull(roomDto);

        Room room = mapper.Map<Room>(roomDto);

        var result = await repository.AddAsync(room);

        return mapper.Map<ResponseRoomDto>(result);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(UpdateRoomDto roomDto)
    {
        ArgumentNullException.ThrowIfNull(roomDto);

        Room room = mapper.Map<Room>(roomDto);

        await repository.UpdateAsync(room);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ResponseRoomDto>> GetAllAsync()
    {
        var rooms = await repository.GetAllAsync();

        return mapper.Map<IEnumerable<ResponseRoomDto>>(rooms);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ResponseRoomDto>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime, int capacity)
    {
        var rooms = await repository.GetAvailableRoomsAsync(startTime, endTime, capacity);

        return mapper.Map<IEnumerable<ResponseRoomDto>>(rooms);
    }

    /// <inheritdoc/>
    public async Task<ResponseRoomDto?> GetByIdAsync(int id)
    {
        Room? room = await repository.GetByIdAsync(id);

        if (room is null)
        {
            return null;
        }

        return mapper.Map<ResponseRoomDto>(room);
    }
}
