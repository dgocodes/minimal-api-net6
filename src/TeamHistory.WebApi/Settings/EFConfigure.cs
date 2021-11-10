using TeamHistory.WebApi.Data;

public static class EFConfigure
{
    public static void ConfigureEFContext(this IServiceCollection services)
    {
        services.AddDbContext<TeamHistoryContext>();
    }
}
