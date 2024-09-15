using System.Net;

namespace localhub_be.Core.Exceptions;
public sealed class UserDoesNotHaveProfilePhotoException : Exception {
    HttpStatusCode StatusCode;

    public UserDoesNotHaveProfilePhotoException(Guid id, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base($"User with id {id} does not have profile picture.") {
        StatusCode = statusCode;
    }
}
