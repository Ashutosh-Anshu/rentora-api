using MediatR;
using Rentora.Application.Common.Interfaces;
using Rentora.Application.Common.Shared.Responses;

namespace Rentora.Application.Authentication.Commands.Register
{
    public sealed class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
    {
        private readonly IIdentityService _identityService;

        public RegisterCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<RegisterResponse>> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            return await _identityService.RegisterAsync(request, cancellationToken);
        }
    }
}
