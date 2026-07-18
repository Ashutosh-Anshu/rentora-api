using Rentora.Application.Common.Shared.Models;

namespace Rentora.Application.Common.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateAccessTokenAsync(UserInfo user);
        Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId, string? ipAddress);
    }
}
