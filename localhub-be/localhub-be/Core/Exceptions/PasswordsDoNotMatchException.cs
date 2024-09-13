using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class PasswordsDoNotMatchException : Exception {
    public HttpStatusCode StatusCode;
    public PasswordsDoNotMatchException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base("The old password you entered is not correct.") {
        StatusCode = statusCode;
    }
}
