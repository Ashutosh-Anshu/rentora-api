using MediatR;
using Rentora.Application.Common.Shared.Responses;
using Rentora.Application.Menus.Queries.GetMenus;

namespace Rentora.Application.Menus.Queries.GetMenuById
{
    public sealed record GetMenuByIdQuery(Guid userId)
    : IRequest<Result<List<GetMenuByIdResponse>>>;
}
