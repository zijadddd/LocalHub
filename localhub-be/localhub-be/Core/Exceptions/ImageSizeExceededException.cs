using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class ImageSizeExceededException : Exception {
    HttpStatusCode StatusCode;

    public ImageSizeExceededException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base("Picture exceeds the maximum allowed size.") {
        StatusCode = statusCode;
    }
}
