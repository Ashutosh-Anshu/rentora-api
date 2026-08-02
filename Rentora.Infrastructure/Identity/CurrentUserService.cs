using Microsoft.AspNetCore.Http;
using Rentora.Application.Common.Interfaces;
using System.Security.Claims;

namespace Rentora.Infrastructure.Identity
{
    public sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? Principal => _httpContextAccessor.HttpContext?.User;

        public ClaimsPrincipal? User => Principal;

        public bool IsAuthenticated =>
            Principal?.Identity?.IsAuthenticated ?? false;

        public Guid UserId
        {
            get
            {
                var value = Principal?.FindFirstValue(ClaimTypes.NameIdentifier);

                return Guid.TryParse(value, out var id)
                    ? id
                    : Guid.Empty;
            }
        }

        public string? Email =>
            Principal?.FindFirstValue(ClaimTypes.Email);

        public string? UserName =>
            Principal?.FindFirstValue(ClaimTypes.Name);

        public string? FullName =>
            Principal?.FindFirst("FullName")?.Value;

        public string? Role =>
            Principal?.FindFirstValue(ClaimTypes.Role);

        public string? RoleId =>
            Principal?.FindFirstValue("RoleId");
    }
}
