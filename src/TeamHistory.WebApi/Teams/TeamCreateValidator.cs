using FluentValidation;

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
            .WithMessage("FoundationDate is required")
            .GreaterThanOrEqualTo(x => DateTime.Now)
            .WithMessage($"Foundation date must be greater than or equal to {DateTime.Now.ToShortDateString()}");

    }
}

