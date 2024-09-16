using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class NoCommentsAvailableForPostException : Exception {
    HttpStatusCode StatusCode;

    public NoCommentsAvailableForPostException(Guid id, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base($"No comments available for post with id {id}.") {
        StatusCode = statusCode;
    }
}
