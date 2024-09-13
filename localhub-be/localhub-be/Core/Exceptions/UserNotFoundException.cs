using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class UserNotFoundException : Exception {
    public HttpStatusCode StatusCode;
    public UserNotFoundException(int id, HttpStatusCode statusCode = HttpStatusCode.NotFound)
        : base($"User with id {id} not found.") {
        StatusCode = statusCode;
    }
}
