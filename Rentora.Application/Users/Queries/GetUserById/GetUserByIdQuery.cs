using MediatR;
using Rentora.Application.Common.Shared.Responses;

namespace Rentora.Application.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid userId) : IRequest<Result<GetUserByIdResponse>>;
}
