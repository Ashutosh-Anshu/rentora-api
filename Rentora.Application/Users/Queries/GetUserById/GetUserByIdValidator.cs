using FluentValidation;

namespace Rentora.Application.Users.Queries.GetUserById
{
    public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty()
                .WithMessage("UserId is required.")
                .Must(userId => userId != Guid.Empty)
                .WithMessage("UserId cannot be an empty GUID.");
        }
    }
}
