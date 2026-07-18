using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rentora.Application.Authentication.Commands.Login;
using Rentora.Application.Authentication.Commands.Register;
using Rentora.Application.Common.Interfaces;
using Rentora.Application.Common.Shared.Models;
using Rentora.Application.Common.Shared.Responses;
using Rentora.Domain.Entities.Authentication;


namespace Rentora.Infrastructure.Identity
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }
        public async Task<Result<RegisterResponse>> RegisterAsync( RegisterCommand request, CancellationToken ct)
        {
            // Check Email
            var emailExists = await _userManager.Users
                .AnyAsync(x => x.Email == request.Email, ct);

            if (emailExists)
            {
                return Result<RegisterResponse>.Fail(
                [
                    new Error("Email", "Email already exists.")
                ]);
            }

            // Check Phone
            var phoneExists = await _userManager.Users
                .AnyAsync(x => x.PhoneNumber == request.PhoneNumber, ct);

            if (phoneExists)
            {
                return Result<RegisterResponse>.Fail(
                [
                    new Error("PhoneNumber", "Phone number already exists.")
                ]);
            }

            // Check Role
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

            if (role is null)
            {
                return Result<RegisterResponse>.Fail(
                [
                    new Error("Role", "Selected role does not exist.")
                ]);
            }

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                EmailConfirmed = true,
                TermsAccepted = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user,request.Password);

            if (!result.Succeeded)
            {
                return Result<RegisterResponse>.Fail(
                    result.Errors.Select(x =>
                        new Error(x.Code, x.Description)));
            }

            var roleResult = await _userManager.AddToRoleAsync(
                user,
                role.Name!);

            if (!roleResult.Succeeded)
            {
                return Result<RegisterResponse>.Fail(
                    roleResult.Errors.Select(x =>
                        new Error(x.Code, x.Description)));
            }

            var response = new RegisterResponse
            {
                UserId = user.Id,
                RoleId = role.Id,
                FullName = user.FullName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!
            };

            return Result<RegisterResponse>.Ok(
                response,
                "User registered successfully.");
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginCommand request, CancellationToken ct)
        {
            // Find user by email
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return Result<LoginResponse>.Fail(
                [
                    new Error("Email", "Invalid email or password.")
                ]);
            }

            // Check if user is active
            if (!user.IsActive)
            {
                return Result<LoginResponse>.Fail(
                [
                    new Error("Account", "Your account has been deactivated.")
                ]);
            }

            // Check if deleted
            if (user.IsDeleted)
            {
                return Result<LoginResponse>.Fail(
                [
                    new Error("Account", "Account does not exist.")
                ]);
            }

            // Verify password
            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                return Result<LoginResponse>.Fail(
                [
                    new Error("Password", "Invalid email or password.")
                ]);
            }

            // Get user role
            var roles = await _userManager.GetRolesAsync(user);

            var roleName = roles.FirstOrDefault() ?? string.Empty;

            var role = await _roleManager.FindByNameAsync(roleName);

            var userInfo = new UserInfo
            {
                UserId = user.Id,
                RoleId = role?.Id ?? Guid.Empty,
                RoleName = roleName,
                FullName = user.FullName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!
            };

            // TODO: JWT Generation
            var accessToken = await _jwtService.GenerateAccessTokenAsync(userInfo);
            var refreshToken = await _jwtService.GenerateRefreshTokenAsync(user.Id, null);

            var response = new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = DateTime.UtcNow,

                User = new UserInfo
                {
                    UserId = user.Id,
                    RoleId = role?.Id ?? Guid.Empty,
                    RoleName = roleName,
                    FullName = user.FullName,
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber!
                }
            };

            return Result<LoginResponse>.Ok(
                response,
                "Login successful.");
        }





    }
}
