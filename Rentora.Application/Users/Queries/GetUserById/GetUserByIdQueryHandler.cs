using MediatR;
using Microsoft.AspNetCore.Identity;
using Rentora.Application.Common.Shared.Responses;
using Rentora.Domain.Entities.Authentication;

namespace Rentora.Application.Users.Queries.GetUserById
{
    public sealed class GetUserByIdQueryHandler(
        UserManager<ApplicationUser> _userManager
        ) : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdResponse>>
    {
        public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.userId.ToString());
            if (user is null)
            {
                return Result<GetUserByIdResponse>.Fail(
                    new List<Error>
                    {
                        new Error("User", "User not found.")
                    });
            }
            var userInfo = new GetUserByIdResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                IsActive = user.IsActive,
            };
            return Result<GetUserByIdResponse>.Ok(userInfo);
        }
    }
}
