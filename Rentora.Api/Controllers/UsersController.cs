using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Common.Shared.Responses;
using Rentora.Application.Users.Queries.GetUserById;

namespace Rentora.Api.Controllers
{
    [Authorize]
    public class UsersController(IMediator _mediator) : BaseApiController
    {

        [HttpGet("getUserById/{userId}")]
        public async Task<Result<GetUserByIdResponse>> GetUserById(Guid userId)
        {
            return await _mediator.Send(new GetUserByIdQuery(userId));
        }
    }
}
