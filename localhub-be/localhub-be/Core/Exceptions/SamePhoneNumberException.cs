using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class SamePhoneNumberException : Exception {
    HttpStatusCode StatusCode;

    public SamePhoneNumberException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base("The new phone number cannot be the same as the old one.") {
        StatusCode = statusCode;
    }
}
