using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class UserAuthInfoNotFoundException : Exception {
    public HttpStatusCode StatusCode;
    public UserAuthInfoNotFoundException(string email, HttpStatusCode statusCode = HttpStatusCode.NotFound) 
        : base($"User with email {email} not found.") {
        StatusCode = statusCode;
    }

    public UserAuthInfoNotFoundException(Guid id, HttpStatusCode statusCode = HttpStatusCode.NotFound)
    : base($"User auth info with user id {id} not found.") {
        StatusCode = statusCode;
    }
}
