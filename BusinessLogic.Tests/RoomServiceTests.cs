using BusinessLogic.DTOs;
using DataAccess.Entities;
using DataAccess.Interfaces;
using AutoMapper;
using Moq;
using BusinessLogic.Services;

namespace BusinessLogic.Tests;

public class RoomServiceTests
{
    private readonly Mock<IRoomRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly RoomService _roomService;

    public RoomServiceTests()
    {
        _mockRepo = new Mock<IRoomRepository>();
        _mockMapper = new Mock<IMapper>();
        _roomService = new RoomService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task AddAsync_ValidData_ReturnsResponseRoomDto()
    {
        // Arrange
        var createDto = new CreateRoomDto("Room A", 50, 2000m);
        var roomEntity = new Room { Id = 0, Name = "Room A", Capacity = 50, BasePricePerHour = 2000m };
        var createdRoom = new Room { Id = 1, Name = "Room A", Capacity = 50, BasePricePerHour = 2000m };
        var expectedDto = new ResponseRoomDto(1, "Room A", 50, 2000m);

        _mockMapper.Setup(m => m.Map<Room>(createDto)).Returns(roomEntity);
        _mockRepo.Setup(r => r.AddAsync(It.IsAny<Room>())).ReturnsAsync(createdRoom);
        _mockMapper.Setup(m => m.Map<ResponseRoomDto>(createdRoom)).Returns(expectedDto);

        // Act
        var result = await _roomService.AddAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<Room>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsResponseRoomDto()
    {
        // Arrange
        int roomId = 1;
        var roomEntity = new Room { Id = roomId, Name = "Room A", Capacity = 50, BasePricePerHour = 2000m };
        var expectedDto = new ResponseRoomDto(roomId, "Room A", 50, 2000m);

        _mockRepo.Setup(r => r.GetByIdAsync(roomId)).ReturnsAsync(roomEntity);
        _mockMapper.Setup(m => m.Map<ResponseRoomDto>(roomEntity)).Returns(expectedDto);

        // Act
        var result = await _roomService.GetByIdAsync(roomId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(roomId, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        int roomId = 999;
        _mockRepo.Setup(r => r.GetByIdAsync(roomId)).ReturnsAsync((Room?)null);

        // Act
        var result = await _roomService.GetByIdAsync(roomId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsCollectionOfResponseRoomDtos()
    {
        // Arrange
        var rooms = new List<Room>
        {
            new Room { Id = 1, Name = "Room A", Capacity = 50, BasePricePerHour = 2000m }
        };
        var expectedDtos = new List<ResponseRoomDto>
        {
            new ResponseRoomDto(1, "Room A", 50, 2000m)
        };

        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(rooms);
        _mockMapper.Setup(m => m.Map<IEnumerable<ResponseRoomDto>>(rooms)).Returns(expectedDtos);

        // Act
        var result = await _roomService.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Room A", result.First().Name);
    }

    [Fact]
    public async Task UpdateAsync_ValidData_CallsRepositoryUpdate()
    {
        // Arrange
        var updateDto = new UpdateRoomDto("Updated Room", 60, 2500m);
        var roomEntity = new Room { Id = 1, Name = "Updated Room", Capacity = 60, BasePricePerHour = 2500m };

        _mockMapper.Setup(m => m.Map<Room>(updateDto)).Returns(roomEntity);

        // Act
        await _roomService.UpdateAsync(updateDto);

        // Assert
        _mockRepo.Verify(r => r.UpdateAsync(roomEntity), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ValidId_CallsRepositoryDelete()
    {
        // Arrange
        int roomId = 1;

        // Act
        await _roomService.DeleteAsync(roomId);

        // Assert
        _mockRepo.Verify(r => r.DeleteAsync(roomId), Times.Once);
    }

    [Fact]
    public async Task GetAvailableRoomsAsync_ValidParameters_ReturnsAvailableRooms()
    {
        // Arrange
        var startTime = new DateTime(2026, 10, 1, 10, 0, 0);
        var endTime = new DateTime(2026, 10, 1, 12, 0, 0);
        int capacity = 20;

        var availableRooms = new List<Room>
        {
            new Room { Id = 1, Name = "Room C", Capacity = 30, BasePricePerHour = 1500m }
        };
        var expectedDtos = new List<ResponseRoomDto>
        {
            new ResponseRoomDto(1, "Room C", 30, 1500m)
        };

        _mockRepo.Setup(r => r.GetAvailableRoomsAsync(startTime, endTime, capacity)).ReturnsAsync(availableRooms);
        _mockMapper.Setup(m => m.Map<IEnumerable<ResponseRoomDto>>(availableRooms)).Returns(expectedDtos);

        // Act
        var result = await _roomService.GetAvailableRoomsAsync(startTime, endTime, capacity);

        // Assert
        Assert.Single(result);
        _mockRepo.Verify(r => r.GetAvailableRoomsAsync(startTime, endTime, capacity), Times.Once);
    }
}
