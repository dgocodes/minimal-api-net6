using FluentValidation;
using TeamHistory.WebApi.Dto;

public static class ValidationConfigure
{
    public static void FluentValidationConfigure(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<TeamCreateDto>(lifetime: ServiceLifetime.Scoped);
    }
}

