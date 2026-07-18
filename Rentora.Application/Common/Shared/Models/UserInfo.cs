namespace Rentora.Application.Common.Shared.Models
{
    public sealed class UserInfo
    {
        public Guid UserId { get; init; }

        public Guid RoleId { get; init; }

        public string RoleName { get; init; } = string.Empty;

        public string FullName { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public string PhoneNumber { get; init; } = string.Empty;
    }
}
