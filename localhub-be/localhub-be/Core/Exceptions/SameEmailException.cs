using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class SameEmailException : Exception {
    HttpStatusCode StatusCode;

    public SameEmailException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base("The new email cannot be the same as the old one.") {
        StatusCode = statusCode;
    }
}
