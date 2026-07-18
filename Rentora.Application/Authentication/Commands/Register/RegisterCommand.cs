using MediatR;
using Rentora.Application.Common.Shared.Responses;

namespace Rentora.Application.Authentication.Commands.Register
{
    public sealed record RegisterCommand : IRequest<Result<RegisterResponse>>
    {
        public Guid RoleId { get; init; }

        public string FullName { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public string PhoneNumber { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public bool TermsAccepted { get; init; }
    }
}
