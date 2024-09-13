using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class OtherUserExistWithNewProvidedEmailException : Exception {
    HttpStatusCode StatusCode;

    public OtherUserExistWithNewProvidedEmailException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base("Another user has that email.") {
        StatusCode = statusCode;
    }
}
