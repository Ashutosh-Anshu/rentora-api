using System.Security.Claims;

namespace Rentora.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string? Email { get; }
        string? UserName { get; }
        string? FullName { get; }
        string? Role { get; }
        string? RoleId { get; }
        bool IsAuthenticated { get; }
        ClaimsPrincipal? User { get; }
    }
}
