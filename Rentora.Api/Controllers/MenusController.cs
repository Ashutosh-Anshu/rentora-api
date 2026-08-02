using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Common.Shared.Responses;
using Rentora.Application.Menus.Queries.GetMenuById;
using Rentora.Application.Menus.Queries.GetMenus;

namespace Rentora.Api.Controllers
{
    [Authorize]
    public sealed class MenusController(IMediator _mediator) : BaseApiController
    {

        [HttpGet("getMenus/{userId}")]
        public async Task<Result<List<GetMenuByIdResponse>>> GetMenus(Guid userId)
        {
            return await _mediator.Send(new GetMenuByIdQuery(userId));
        }
    }
}
