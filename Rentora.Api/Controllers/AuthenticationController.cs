using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Authentication.Commands.Login;
using Rentora.Application.Authentication.Commands.Register;
using Rentora.Application.Common.Shared.Responses;

namespace Rentora.Api.Controllers
{
    public sealed class AuthenticationController(IMediator _mediator) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<Result<RegisterResponse>> Register(RegisterCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("login")]
        public async Task<Result<LoginResponse>> Login(LoginCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
