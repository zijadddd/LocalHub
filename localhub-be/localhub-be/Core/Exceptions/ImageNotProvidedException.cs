using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class ImageNotProvidedException : Exception {
    HttpStatusCode StatusCode;

    public ImageNotProvidedException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base("Image not provided.") {
        StatusCode = statusCode;
    }
}
