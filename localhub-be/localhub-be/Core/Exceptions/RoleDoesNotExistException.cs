using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class RoleDoesNotExistException : Exception {
    HttpStatusCode StatusCode;

    public RoleDoesNotExistException(string roleName, HttpStatusCode statusCode = HttpStatusCode.NotFound) : base($"Role {roleName} does not exist.") {
        StatusCode = statusCode;
    } 
}

