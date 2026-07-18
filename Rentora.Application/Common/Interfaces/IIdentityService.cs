using Rentora.Application.Authentication.Commands.Login;
using Rentora.Application.Authentication.Commands.Register;
using Rentora.Application.Common.Shared.Responses;

namespace Rentora.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<RegisterResponse>> RegisterAsync(RegisterCommand request, CancellationToken ct);
        Task<Result<LoginResponse>> LoginAsync(LoginCommand request, CancellationToken ct);
    }
}
