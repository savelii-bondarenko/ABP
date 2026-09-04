using BusinessLogic.DTOs;
using DataAccess.Entities;
using Moq;

namespace BusinessLogic.Tests;

public class RoomServiceTests
{
    [Fact]
    public async Task CreateRoomAsync_ValidData_ReturnsRoomId()
    {
        // Arrange
        var mockRoomRepo = new Mock<IRoomRepository>();

        mockRoomRepo.Setup(repo => repo.AddAsync(It.IsAny<Room>()))
            .ReturnsAsync((Room r) =>
            {
                r.Id = 1;
                r.Name = "A";
                r.Capacity = 50;
                r.BasePricePerHour = 2000;
                return r;
            });

        var roomService = new RoomService(mockRoomRepo.Object);
        var createDto = new CreateRoomDto("Зал А", 50, 2000m, new List<int>());

        // Act
        var resultId = await roomService.AddAsync(createDto);

        // Assert
        Assert.Equal(1, resultId);
        mockRoomRepo.Verify(repo => repo.AddAsync(It.IsAny<Room>()), Times.Once);
    }
}
