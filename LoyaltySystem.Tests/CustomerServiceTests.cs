using LoyaltySystem.Application.Abstractions;
using LoyaltySystem.Application.Exceptions;
using LoyaltySystem.Application.Services;
using LoyaltySystem.Domain.Models.User;
using Moq;
using Xunit;

namespace LoyaltySystem.Tests;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _mockRepo;
    private readonly Mock<IDiscountRepo> _mockDiscountRepo;
    private readonly Mock<IConfirmationService> _mockConfirmationService;
    private readonly Application.Abstractions.ICustomerService _service;

    public CustomerServiceTests()
    {
        _mockConfirmationService = new Mock<IConfirmationService>();
        _mockRepo = new Mock<ICustomerRepository>();
        _mockDiscountRepo = new Mock<IDiscountRepo>();
        _service = new CustomerService(_mockRepo.Object, _mockDiscountRepo.Object, _mockConfirmationService.Object);
    }

    [Fact]
    public async Task Get_ShouldReturnUserResponse()
    {
        var guid = Guid.NewGuid();
        var expectedResponse = new User { Id = guid, Email = "test@mail.com", IsConfirmed = true };

        _mockRepo.Setup(x =>
            x.UserWithIdExistsAsync(guid, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _mockRepo.Setup(x =>
            x.GetByIdAsync(guid, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResponse);

        var result = await _service.Get(guid, CancellationToken.None);
        Assert.Equal(guid, result.Id);
    }

    [Fact]
    public async Task Get_ShouldThrow_WhenUserDoesntExist()
    {
        var guid = Guid.NewGuid();
        var expectedResponse = new User { Id = guid, Email = "test@mail.com", IsConfirmed = true };

        _mockRepo.Setup(x =>
            x.UserWithIdExistsAsync(guid, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        await Assert.ThrowsAsync<UserNotFoundException>(() => _service.Get(guid, CancellationToken.None));
    }
}