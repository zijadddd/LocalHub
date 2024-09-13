using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class ModelIsNotValidException : Exception {
    public HttpStatusCode StatusCode;
    public ModelIsNotValidException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message) {
        StatusCode = statusCode;
    }
}
