using localhub_be.Models.DTOs;
using localhub_be.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace localhub_be.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class AuthController : BaseController {

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) {
        _authService = authService;
    }

    [HttpPost("authentication"), AllowAnonymous]
    public async Task<ActionResult<AuthOut>> Authentication(AuthIn request) {
        ValidateModelState();
        AuthOut response = await _authService.AuthenticateUser(request);

        return Ok(response);
    }

    [HttpPost("{userId}/suspendUser"), Authorize(Roles="Administrator")]
    public async Task<ActionResult<MessageOut>> SuspendUser(Guid userId, SuspendIn request) {
        ValidateModelState();
        MessageOut response = await _authService.SuspendUser(userId, request);

        return Ok(response);
    }
}
