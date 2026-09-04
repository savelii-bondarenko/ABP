using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Moq;

namespace BusinessLogic.Tests;

public class BookingServiceTests
{
    private readonly Mock<IBookingRepository> _mockBookingRepo;
    private readonly Mock<IRoomRepository> _mockRoomRepo;
    private readonly Mock<IAdditionalServiceRepository> _mockAddServiceRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IPriceCalculatorService> _mockCalculator;
    private readonly BookingService _bookingService;

    public BookingServiceTests()
    {
        _mockBookingRepo = new Mock<IBookingRepository>();
        _mockRoomRepo = new Mock<IRoomRepository>();
        _mockAddServiceRepo = new Mock<IAdditionalServiceRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockCalculator = new Mock<IPriceCalculatorService>();

        _bookingService = new BookingService(
            _mockBookingRepo.Object,
            _mockRoomRepo.Object,
            _mockAddServiceRepo.Object,
            _mockMapper.Object,
            _mockCalculator.Object
        );
    }

    [Fact]
    public async Task AddAsync_WhenRoomAlreadyBooked_ThrowsInvalidOperationException()
    {
        // Arrange
        var dto = new CreateBookingDto(1, DateTime.Now, DateTime.Now.AddHours(2), new List<int>());

        _mockBookingRepo.Setup(repo => repo.GetOverlappingBookingsAsync(dto.RoomId, dto.StartTime, dto.EndTime))
                        .ReturnsAsync(new List<Booking>
                        {
                        new Booking
                        {
                            RoomId = dto.RoomId,
                            StartTime = dto.StartTime,
                            EndTime = dto.EndTime,
                            TotalPrice = 0m
                        }
                        });

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _bookingService.AddAsync(dto));
        Assert.Equal("The room is already booked for the selected time period.", exception.Message);
    }

    [Fact]
    public async Task AddAsync_ValidData_ReturnsResponseBookingDto()
    {
        // Arrange
        var startTime = new DateTime(2026, 10, 1, 10, 0, 0);
        var endTime = new DateTime(2026, 10, 1, 12, 0, 0);

        var createDto = new CreateBookingDto(1, startTime, endTime, new List<int>());
        var expectedRoom = new Room { Id = 1, Capacity = 50, BasePricePerHour = 2000m, Name = "Conference Room" };
        var bookingEntity = new Booking { Id = 0, RoomId = 1, StartTime = startTime, EndTime = endTime, TotalPrice = 4000m };
        var createdBooking = new Booking { Id = 1, RoomId = 1, StartTime = startTime, EndTime = endTime, TotalPrice = 4000m };
        var expectedDto = new ResponseBookingDto(1, 1, startTime, endTime, 4000m);

        _mockBookingRepo.Setup(r => r.GetOverlappingBookingsAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                        .ReturnsAsync(new List<Booking>());

        _mockRoomRepo.Setup(r => r.GetByIdAsync(createDto.RoomId))
                     .ReturnsAsync(expectedRoom);

        _mockCalculator.Setup(c => c.Calculate(expectedRoom.BasePricePerHour, startTime, endTime))
                       .Returns(4000m);

        _mockMapper.Setup(m => m.Map<Booking>(createDto)).Returns(bookingEntity);
        _mockBookingRepo.Setup(r => r.AddAsync(It.IsAny<Booking>())).ReturnsAsync(createdBooking);
        _mockMapper.Setup(m => m.Map<ResponseBookingDto>(createdBooking)).Returns(expectedDto);

        // Act
        var result = await _bookingService.AddAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        _mockBookingRepo.Verify(r => r.AddAsync(It.IsAny<Booking>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ValidId_CallsRepositoryDelete()
    {
        // Arrange
        int bookingId = 1;

        // Act
        await _bookingService.DeleteAsync(bookingId);

        // Assert
        _mockBookingRepo.Verify(r => r.DeleteAsync(bookingId), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsResponseBookingDto()
    {
        // Arrange
        int bookingId = 1;
        var startTime = new DateTime(2026, 10, 1, 10, 0, 0);
        var endTime = new DateTime(2026, 10, 1, 12, 0, 0);

        var bookingEntity = new Booking { Id = bookingId, RoomId = 1, StartTime = startTime, EndTime = endTime, TotalPrice = 4000m };
        var expectedDto = new ResponseBookingDto(bookingId, 1, startTime, endTime, 4000m);

        _mockBookingRepo.Setup(r => r.GetByIdAsync(bookingId)).ReturnsAsync(bookingEntity);
        _mockMapper.Setup(m => m.Map<ResponseBookingDto>(bookingEntity)).Returns(expectedDto);

        // Act
        var result = await _bookingService.GetByIdAsync(bookingId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bookingId, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        int bookingId = 999;
        _mockBookingRepo.Setup(r => r.GetByIdAsync(bookingId)).ReturnsAsync((Booking?)null);

        // Act
        var result = await _bookingService.GetByIdAsync(bookingId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsCollectionOfResponseBookingDtos()
    {
        // Arrange
        var startTime = new DateTime(2026, 10, 1, 10, 0, 0);
        var endTime = new DateTime(2026, 10, 1, 12, 0, 0);

        var bookings = new List<Booking>
        {
            new Booking { Id = 1, RoomId = 1, StartTime = startTime, EndTime = endTime, TotalPrice = 4000m }
        };
        var expectedDtos = new List<ResponseBookingDto>
        {
            new ResponseBookingDto(1, 1, startTime, endTime, 4000m)
        };

        _mockBookingRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(bookings);
        _mockMapper.Setup(m => m.Map<IEnumerable<ResponseBookingDto>>(bookings)).Returns(expectedDtos);

        // Act
        var result = await _bookingService.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal(1, result.First().RoomId);
    }

    [Fact]
    public async Task UpdateAsync_ValidData_CallsRepositoryUpdate()
    {
        // Arrange
        var startTime = new DateTime(2026, 10, 1, 10, 0, 0);
        var endTime = new DateTime(2026, 10, 1, 12, 0, 0);

        var updateDto = new UpdateBookingDto(1, 1, startTime, endTime, 5000m);
        var bookingEntity = new Booking { Id = 1, RoomId = 1, StartTime = startTime, EndTime = endTime, TotalPrice = 5000m };

        _mockMapper.Setup(m => m.Map<Booking>(updateDto)).Returns(bookingEntity);

        // Act
        await _bookingService.UpdateAsync(updateDto);

        // Assert
        _mockBookingRepo.Verify(r => r.UpdateAsync(bookingEntity), Times.Once);
    }

    [Fact]
    public async Task GetOverlappingBookingsAsync_ValidParameters_ReturnsOverlappingBookings()
    {
        // Arrange
        int roomId = 1;
        var queryStart = new DateTime(2026, 10, 1, 11, 0, 0);
        var queryEnd = new DateTime(2026, 10, 1, 13, 0, 0);

        var overlappingBookings = new List<Booking>
        {
            new Booking { Id = 1, RoomId = roomId, StartTime = new DateTime(2026, 10, 1, 10, 0, 0), EndTime = new DateTime(2026, 10, 1, 12, 0, 0), TotalPrice = 4000m }
        };

        var expectedDtos = new List<ResponseBookingDto>
        {
            new ResponseBookingDto(1, roomId, new DateTime(2026, 10, 1, 10, 0, 0), new DateTime(2026, 10, 1, 12, 0, 0), 4000m)
        };

        _mockBookingRepo.Setup(r => r.GetOverlappingBookingsAsync(roomId, queryStart, queryEnd)).ReturnsAsync(overlappingBookings);
        _mockMapper.Setup(m => m.Map<IEnumerable<ResponseBookingDto>>(overlappingBookings)).Returns(expectedDtos);

        // Act
        var result = await _bookingService.GetOverlappingBookingsAsync(roomId, queryStart, queryEnd);

        // Assert
        Assert.Single(result);
        _mockBookingRepo.Verify(r => r.GetOverlappingBookingsAsync(roomId, queryStart, queryEnd), Times.Once);
    }
}
