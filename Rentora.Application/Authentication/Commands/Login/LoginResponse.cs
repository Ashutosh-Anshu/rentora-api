using Rentora.Application.Common.Shared.Models;

namespace Rentora.Application.Authentication.Commands.Login
{
    public sealed class LoginResponse
    {
        public string AccessToken { get; init; } = string.Empty;

        public string RefreshToken { get; init; } = string.Empty;

        public DateTime ExpiresAt { get; init; }

        public UserInfo User { get; init; } = null!;
    }
}
