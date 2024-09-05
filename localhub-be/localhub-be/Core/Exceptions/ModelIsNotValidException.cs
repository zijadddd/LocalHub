using localhub_be.Models.DTOs;
using System.Net;
using System.Text.Json;

namespace localhub_be.Core.Exceptions;
public class ModelIsNotValidException : Exception {
    public HttpStatusCode StatusCode;
    public ModelIsNotValidException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message) {
        StatusCode = statusCode;
    }
}
