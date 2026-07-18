using MediatR;
using Rentora.Application.Common.Interfaces;
using Rentora.Application.Common.Shared.Responses;

namespace Rentora.Application.Authentication.Commands.Login
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly IIdentityService _identityService;

        public LoginCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken ct)
        {
            return await _identityService.LoginAsync(request, ct);
        }
    }
}
