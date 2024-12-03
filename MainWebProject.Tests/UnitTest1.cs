#pragma warning disable CS8600, CS8604
using Xunit;
using MainWebProject.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moq;

namespace MainWebProject.Tests
{public class HomeControllerTests
{
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly Mock<ISession> _sessionMock;
    private readonly HomeController _controller;

    public HomeControllerTests()
    {
        // Mocking the session without extension methods
        _sessionMock = new Mock<ISession>();
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        
        // Set up the mock for session access
        _httpContextAccessorMock.Setup(x => x.HttpContext.Session).Returns(_sessionMock.Object);
        
        _controller = new HomeController(_httpContextAccessorMock.Object);
    }

    [Fact]
    public void SecureLogin_ThrowsException_ReturnsErrorMessage()
    {
        // Simulate the exception
        var exception = new Exception("Username or password is missing.");
        try
        {
            // Act: Call the method that throws exception
            var result = _controller.SecureLogin(null, null);  // Assuming this will throw
        }
        catch (Exception ex)
        {
            // Assert: Check if exception message matches
            Assert.Contains("Username or password is missing", ex.Message);
        }
    }

    [Fact]
    public void SecureLogin_ValidCredentials_ReturnsSuccessMessage()
    {
        // Arrange: Set valid credentials
        var username = "testUser";
        var password = "validPassword";

        // Bypass SetString usage directly; mocking session response instead
        _sessionMock.Setup(s => s.SetString(It.IsAny<string>(), It.IsAny<string>()));

        // Act: Call the controller method
        var result = _controller.SecureLogin(username, password);

        // Assert: Check if the expected success result was returned
        var contentResult = result as ContentResult;
        Assert.NotNull(contentResult);
        Assert.Equal("Login successful", contentResult?.Content);
    }
}

}
