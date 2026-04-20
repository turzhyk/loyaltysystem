using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.Exceptions;
using LoyaltySystem.Application.Services;
using LoyaltySystem.Domain.Models.User;
using Moq;
using Xunit;

namespace LoyaltySystem.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly Mock<IConfirmationService> _mockConfirmationService;
    private readonly IUserService _service;

    public UserServiceTests()
    {
        _mockConfirmationService = new Mock<IConfirmationService>();
        _mockRepo = new Mock<IUserRepository>();
        _service = new UserService(_mockRepo.Object, _mockConfirmationService.Object);
    }

    [Fact]
    public async Task Get_ShouldReturnUserResponse()
    {
        var guid = Guid.NewGuid();
        var expectedResponse = new User { Id = guid, Email = "test@mail.com", IsConfirmed =  true};

        _mockRepo.Setup(x => 
            x.UserWithIdExists(guid, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _mockRepo.Setup(x => 
            x.GetById(guid, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResponse);

        var result = await _service.Get(guid, CancellationToken.None);
        Assert.Equal(guid, result.Id);
    }
    [Fact]
    public async Task Get_ShouldThrow_WhenUserDoesntExist()
    {
        var guid = Guid.NewGuid();
        var expectedResponse = new User { Id = guid, Email = "test@mail.com", IsConfirmed =  true};

        _mockRepo.Setup(x => 
            x.UserWithIdExists(guid, It.IsAny<CancellationToken>())).ReturnsAsync(false);
        
        await Assert.ThrowsAsync<UserNotFoundException>(() => _service.Get(guid, CancellationToken.None));
    }
}