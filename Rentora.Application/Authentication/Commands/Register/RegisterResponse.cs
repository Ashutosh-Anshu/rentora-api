namespace Rentora.Application.Authentication.Commands.Register
{
    public sealed class RegisterResponse
    {
        public Guid UserId { get; init; }

        public Guid RoleId { get; init; }

        public string FullName { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public string PhoneNumber { get; init; } = string.Empty;
    }
}
