using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class PostNotFoundException : Exception {
    HttpStatusCode StatusCode;

    public PostNotFoundException(Guid id, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base($"Post with id {id} not found.") {
        StatusCode = statusCode;
    }
}
