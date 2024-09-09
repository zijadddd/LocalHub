using System.Net;

namespace localhub_be.Core.Exceptions;
public class OtherUserExistWithNewProvidedPhoneNumberException : Exception {
    HttpStatusCode StatusCode;

    public OtherUserExistWithNewProvidedPhoneNumberException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base("Another user has that phone number.") {
        StatusCode = statusCode;
    }
}
