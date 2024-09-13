using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class InvalidDateException : Exception {
    public HttpStatusCode StatusCode;
    public InvalidDateException(string dateType, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base($"Invalid {dateType} provided.") {
        StatusCode = statusCode;
    }
}
