using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class NoUsersFoundException : Exception {
    public HttpStatusCode StatusCode;

    public NoUsersFoundException(HttpStatusCode statusCode = HttpStatusCode.NotFound) : base("No users are available in the database right now.") { 
        StatusCode = statusCode;
    } 
}
