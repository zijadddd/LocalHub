using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class PasswordReuseException : Exception {
    public HttpStatusCode StatusCode;

    public PasswordReuseException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base("The new password cannot be the same as the old password.") {
        StatusCode = statusCode;
    }
}
