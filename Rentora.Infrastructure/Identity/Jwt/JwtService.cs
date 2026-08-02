using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rentora.Application.Common.Interfaces;
using Rentora.Application.Common.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Rentora.Infrastructure.Identity.Jwt
{
    public class JwtService: IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateAccessTokenAsync(UserInfo user)
        {
            var key = Encoding.UTF8.GetBytes(
                _configuration["Jwt:SecretKey"]
                ?? throw new Exception("JWT Key missing"));


            var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                
                    new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new(ClaimTypes.Name, user.FullName),
                    new(ClaimTypes.Email, user.Email),
                
                    new(ClaimTypes.Role, user.RoleName),
                
                    new("RoleId", user.RoleId.ToString())
                };


            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256);


            var expirationMinutes = Convert.ToDouble(
                _configuration["Jwt:AccessTokenExpirationMinutes"] ?? "15");


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );


            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        public async Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId, string? ipAddress)
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(
                    Convert.ToDouble(_configuration["Jwt:RefreshTokenExpirationDays"] ?? "7")),
                IsRevoked = false,
                CreatedByIp = ipAddress
            };
        }
    }
}
