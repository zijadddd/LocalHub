using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class UserIsSuspendedException : Exception {
    HttpStatusCode StatusCode;

    public UserIsSuspendedException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base("You are suspended from this application.") {
        StatusCode = statusCode;
    }
}
