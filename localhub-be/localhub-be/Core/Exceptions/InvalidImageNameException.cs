using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class InvalidImageNameException : Exception {
    HttpStatusCode StatusCode;

    public InvalidImageNameException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base("Something with image name is not correct.") {
        StatusCode = statusCode;
    }
}
