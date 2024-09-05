using localhub_be.Models.DTOs;
using localhub_be.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace localhub_be.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class UserController : BaseController {

    private readonly IUserService _userService;

    public UserController(IUserService userService) {
        _userService = userService;
    }

    [HttpGet, Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<List<UserOut>>> GetAllUsers() {
        List<UserOut> response = await _userService.GetAll();

        return Ok(response);
    }

    [HttpGet("{id}"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<UserOut>> GetUser(int id) {
        UserOut response = await _userService.Get(id);

        return Ok(response);
    }

    [HttpPost, Authorize(Roles = "Administrator")]
    public async Task<ActionResult<UserOut>> CreateUser(UserIn request) {
        ValidateModelState();
        UserOut response = await _userService.Create(request);

        return Ok(response);
    }

    [HttpPut, Authorize(Roles = "Administrator")]
    public async Task<ActionResult<UserOut>> UpdateUser(UserIn request) {
        ValidateModelState();
        UserOut response = await _userService.Update(request);

        return Ok(response);
    }

    [HttpDelete, Authorize(Roles = "Administrator")]
    public async Task<ActionResult<string>> DeleteUser(int id) {
        string response = await _userService.Delete(id);

        return Ok(response);
    }

    [HttpPost("{id}"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<string>> ChangeUserPassword(int id, ChangeUserPasswordIn request) {
        ValidateModelState();
        string response = await _userService.ChangePassword(id, request);

        return Ok(response);
    }
}
