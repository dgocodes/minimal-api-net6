using FluentValidation;

namespace TeamHistory.WebApi.Dto
{
    public record TeamCreateDto(string Name, DateTime FoundationDate)
    {
        public class Validator : AbstractValidator<TeamCreateDto>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotNull()
                    .WithMessage("Name is required")
                    .MinimumLength(3)
                    .WithMessage("Name has min length of 3 characters");


                RuleFor(x => x.FoundationDate)
                    .NotNull()
                    .WithMessage("FoundationDate is required");
            }
        }
    }
}
