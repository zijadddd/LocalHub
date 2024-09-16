using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class CommentNotFoundException : Exception {
    HttpStatusCode StatusCode;

    public CommentNotFoundException(Guid id, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base($"Comment with id {id} not found.") {
        StatusCode = statusCode;
    }
}
