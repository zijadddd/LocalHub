using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class IncorrectPasswordException : Exception {
    public HttpStatusCode StatusCode;
    public IncorrectPasswordException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base("Incorrect password.") {
        StatusCode = statusCode;
    }
}
