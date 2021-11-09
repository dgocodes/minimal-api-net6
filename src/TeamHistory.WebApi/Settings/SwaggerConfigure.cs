using Microsoft.OpenApi.Models;

public static class SwaggerConfigure
{
    public static void ConfigureSwaggerGen(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Team History API",
                Description = "An ASP.NET Core Web API for list history the teams of the world",
                Contact = new OpenApiContact
                {
                    Name = "Diego Perillo",
                    Url = new Uri("https://github.com/sisdperillo")
                }
            });
        });
    }

    public static void ConfigureSwaggerUI(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
    }
}

