using System.Net;

namespace localhub_be.Core.Exceptions;
public class InvalidFileExtensionException : Exception {
    HttpStatusCode StatusCode;

    public InvalidFileExtensionException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base("Only the following file extensions are allowed: .jpg, .jpeg, .png.") {
        StatusCode = statusCode;
    }
}
