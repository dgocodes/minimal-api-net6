using FluentValidation;

public static class ValidationConfigure
{
    public interface IAssemblyMarker { }


    public static void ConfigureFluentValidation(this IServiceCollection services)
    {
        //services.AddValidatorsFromAssemblyContaining<TeamCreateDto>(lifetime: ServiceLifetime.Scoped);
        services.AddValidatorsFromAssemblyContaining(typeof(IAssemblyMarker));
    }
}

