using localhub_be.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace localhub_be.Controllers;
public abstract class BaseController : ControllerBase {
    protected void ValidateModelState() {
        if (!ModelState.IsValid) {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            throw new ModelIsNotValidException(string.Join(";", errors));
        }
    }
}

