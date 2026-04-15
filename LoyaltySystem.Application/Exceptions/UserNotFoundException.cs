using System.Net;

namespace LoyaltySystem.Application.Exceptions;

public class UserNotFoundException:Exception
{
    public HttpStatusCode StatusCode { get; }

    public UserNotFoundException(string message = "User not found", HttpStatusCode statusCode = HttpStatusCode.NotFound):base(message)
    {
        StatusCode = statusCode;
    }
}