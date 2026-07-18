using MediatR;
using Rentora.Application.Common.Shared.Responses;

namespace Rentora.Application.Authentication.Commands.Login
{
    public sealed record LoginCommand : IRequest<Result<LoginResponse>>
    {
        public string Email { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public bool RememberMe { get; init; }
    }
}
