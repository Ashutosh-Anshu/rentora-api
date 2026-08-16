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

        public async Task<Result<RegisterResponse>> RegisterAsync(RegisterCommand request,CancellationToken ct)
        {
            var role = await _roleManager.FindByIdAsync(
                request.RoleId.ToString());

            if (role is null)
            {
                return Result<RegisterResponse>.Fail(
                    "Role does not exist.");
            }

            if (!role.IsActive)
            {
                return Result<RegisterResponse>.Fail(
                    "Role is inactive.");
            }

            var usersWithEmail = await _userManager.Users
                .Where(x => x.Email == request.Email)
                .ToListAsync(ct);

            foreach (var existingUser in usersWithEmail)
            {
                var existingRoles = await _userManager.GetRolesAsync(existingUser);

                if (existingRoles.Contains(role.Name!))
                {
                    return Result<RegisterResponse>.Fail(
                        $"Email is already registered as {role.Name}.");
                }
            }

            var usersWithPhone = await _userManager.Users
                .Where(x => x.PhoneNumber == request.PhoneNumber)
                .ToListAsync(ct);

            foreach (var existingUser in usersWithPhone)
            {
                var existingRoles = await _userManager.GetRolesAsync(existingUser);

                if (existingRoles.Contains(role.Name!))
                {
                    return Result<RegisterResponse>.Fail(
                        $"Phone number is already registered as {role.Name}.");
                }
            }

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = $"{request.Email}_{Guid.NewGuid():N}",
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                EmailConfirmed = true,
                TermsAccepted = request.TermsAccepted,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);

            if (!createResult.Succeeded)
            {
                return Result<RegisterResponse>.Fail(
                    createResult.Errors.Select(x =>
                        new Error(x.Description)));
            }

            var roleResult = await _userManager.AddToRoleAsync(
                user,
                role.Name!);

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);

                return Result<RegisterResponse>.Fail(
                    roleResult.Errors.Select(x =>
                        new Error(x.Description)));
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

            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

            if (role is null)
            {
                return Result<LoginResponse>.Fail(
                    "Selected role does not exist.");
            }

            if (!role.IsActive)
            {
                return Result<LoginResponse>.Fail(
                    "Selected role is inactive.");
            }

            var normalizedEmail = _userManager.NormalizeEmail(request.Email);

            var users = await _userManager.Users
                .Where(x => x.NormalizedEmail == normalizedEmail)
                .ToListAsync(ct);

            if (users.Count == 0)
            {
                return Result<LoginResponse>.Fail(
                    "Invalid email or password.");
            }

            ApplicationUser? user = null;

            foreach (var existingUser in users)
            {
                var roles = await _userManager.GetRolesAsync(
                    existingUser);

                if (roles.Contains(
                        role.Name!,
                        StringComparer.OrdinalIgnoreCase))
                {
                    user = existingUser;
                    break;
                }
            }

            if (user is null)
            {
                return Result<LoginResponse>.Fail(
                    "Invalid email, password or role.");
            }

            if (!user.IsActive)
            {
                return Result<LoginResponse>.Fail(
                    "Your account has been deactivated.");
            }

            if (user.IsDeleted)
            {
                return Result<LoginResponse>.Fail(
                    "Account does not exist.");
            }

            var passwordResult =
                await _signInManager.CheckPasswordSignInAsync(
                    user,
                    request.Password,
                    lockoutOnFailure: true);

            if (!passwordResult.Succeeded)
            {
                return Result<LoginResponse>.Fail(
                    "Invalid email or password.");
            }

            var userInfo = new UserInfo
            {
                UserId = user.Id,
                RoleId = role.Id,
                RoleName = role.Name!,
                FullName = user.FullName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!
            };

            var accessToken = await _jwtService.GenerateAccessTokenAsync(userInfo);

            var refreshToken = await _jwtService.GenerateRefreshTokenAsync(user.Id, null);

            var response = new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = DateTime.UtcNow,
                User = userInfo
            };

            return Result<LoginResponse>.Ok(
                response,
                "Login successful.");
        }




    }
}
