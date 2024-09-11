﻿using localhub_be.Models.DTOs;
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

    [HttpDelete, Authorize(Roles = "Administrator")]
    public async Task<ActionResult<MessageOut>> DeleteUser(int id) {
        MessageOut response = await _userService.Delete(id);

        return Ok(response);
    }

    [HttpPut("{id}/updatePassword"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<MessageOut>> ChangeUserPassword(int id, ChangeUserPasswordIn request) {
        ValidateModelState();
        MessageOut response = await _userService.ChangePassword(id, request);

        return Ok(response);
    }

    [HttpPut("{id}/updateEmail"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<MessageOut>> UpdateEmail(int id, ChangeUserEmailIn request) {
        ValidateModelState();
        MessageOut response = await _userService.ChangeEmail(id, request);

        return Ok(response);
    }

    [HttpPut("{id}/updatePhoneNumber"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<MessageOut>> UpdatePhoneNumber(int id, ChangeUserPhoneNumberIn request) {
        ValidateModelState();
        MessageOut response = await _userService.ChangePhoneNumber(id, request);

        return Ok(response);
    }

    [HttpPut("{id}/updateAddressAndRegion"), Authorize(Roles = "User, Administrator")]
    public async Task<ActionResult<MessageOut>> UpdateUserAddressAndRegion(int id, ChangeUserAddressAndRegionIn request) {
        ValidateModelState();
        MessageOut response = await _userService.ChangeAddressAndRegion(id, request);

        return Ok(response);
    }
}
