using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Services;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Moq;

namespace BusinessLogic.Tests;

public class AdditionalServiceServiceTests
{
    private readonly Mock<IAdditionalServiceRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly AdditionalServiceService _service;

    public AdditionalServiceServiceTests()
    {
        _mockRepo = new Mock<IAdditionalServiceRepository>();
        _mockMapper = new Mock<IMapper>();

        _service = new AdditionalServiceService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedDtos()
    {
        // Arrange
        var entities = new List<AdditionalService> { new AdditionalService { Id = 1, Name = "Wi-Fi", Price = 300m } };
        var dtos = new List<AdditionalServiceDto> { new AdditionalServiceDto(1, "Wi-Fi", 300m) };

        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(entities);
        _mockMapper.Setup(m => m.Map<IEnumerable<AdditionalServiceDto>>(entities)).Returns(dtos);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Wi-Fi", result.First().Name);
    }

    [Fact]
    public async Task GetByIdAsync_WhenExists_ReturnsMappedDto()
    {
        // Arrange
        var entity = new AdditionalService { Id = 1, Name = "Wi-Fi", Price = 300m };
        var dto = new AdditionalServiceDto(1, "Wi-Fi", 300m);

        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(entity);
        _mockMapper.Setup(m => m.Map<AdditionalServiceDto>(entity)).Returns(dto);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_WhenDoesNotExist_ReturnsNull()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((AdditionalService?)null);

        // Act
        var result = await _service.GetByIdAsync(99);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_MapsAndSaves_ReturnsDto()
    {
        // Arrange
        var createDto = new CreateAdditionalServiceDto("Projector", 500m);
        var entity = new AdditionalService { Id = 0, Name = "Projector", Price = 500m };
        var savedEntity = new AdditionalService { Id = 1, Name = "Projector", Price = 500m };
        var responseDto = new AdditionalServiceDto(1, "Projector", 500m);

        _mockMapper.Setup(m => m.Map<AdditionalService>(createDto)).Returns(entity);
        _mockRepo.Setup(repo => repo.AddAsync(entity)).ReturnsAsync(savedEntity);
        _mockMapper.Setup(m => m.Map<AdditionalServiceDto>(savedEntity)).Returns(responseDto);

        // Act
        var result = await _service.AddAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

        _mockRepo.Verify(repo => repo.AddAsync(entity), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_MapsAndUpdates()
    {
        // Arrange
        var updateDto = new UpdateAdditionalServiceDto(1, "Projector 4K", 700m);
        var entity = new AdditionalService { Id = 1, Name = "Projector 4K", Price = 700m };

        _mockMapper.Setup(m => m.Map<AdditionalService>(updateDto)).Returns(entity);

        // Act
        await _service.UpdateAsync(updateDto);

        // Assert
        _mockRepo.Verify(repo => repo.UpdateAsync(entity), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_CallsRepository()
    {
        // Arrange
        int serviceId = 1;

        // Act
        await _service.DeleteAsync(serviceId);

        // Assert
        _mockRepo.Verify(repo => repo.DeleteAsync(serviceId), Times.Once);
    }
}
