using MediatR;
using MenuSaz.Identity.Application.Feature.User.Command;
using MenuSaz.Identity.Application.Feature.User.Dtos;
using MenuSaz.Identity.Application.Feature.User.Login.Commad;
using MenuSaz.Identity.Application.Feature.User.Login.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MenuSaz.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<UserDto> Register(RegisterCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<LoginDto> Login(LoginCommand command)
    {
        return await _mediator.Send(command);
    }
}
