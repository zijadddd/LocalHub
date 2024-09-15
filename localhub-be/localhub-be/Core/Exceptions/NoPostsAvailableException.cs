using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class NoPostsAvailableException : Exception {
    HttpStatusCode StatusCode;

    public NoPostsAvailableException(HttpStatusCode statusCode = HttpStatusCode.NotFound) : base("No posts available.") {
        StatusCode = statusCode;
    }
}
