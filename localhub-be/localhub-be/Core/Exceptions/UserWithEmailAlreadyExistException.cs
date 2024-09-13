using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class UserWithEmailAlreadyExistException : Exception {
    public HttpStatusCode StatusCode;
    public UserWithEmailAlreadyExistException(string email, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base($"User with email {email} already exist.") {
        StatusCode = statusCode;
    }
}
