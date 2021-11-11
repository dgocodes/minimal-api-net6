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

        RuleFor(x => x.Initials)
            .NotNull()
            .WithMessage("Initials is required");

        RuleFor(x => x.FoundationDate)
            .NotNull()
            .WithMessage("FoundationDate is required")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage($"Foundation date must be less than or equal to {DateTime.Now.ToShortDateString()}");
    }
}

