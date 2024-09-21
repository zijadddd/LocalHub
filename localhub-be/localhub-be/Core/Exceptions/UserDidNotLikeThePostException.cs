using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class UserDidNotLikeThePostException : Exception {
    HttpStatusCode StatusCode;

    public UserDidNotLikeThePostException(HttpStatusCode statusCode = HttpStatusCode.NotFound) : base("User did not like the post.") {
        StatusCode = statusCode;
    }
}
