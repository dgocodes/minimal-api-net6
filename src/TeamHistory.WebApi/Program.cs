var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSwaggerGen();
builder.Services.ConfigureFluentValidation();
builder.Services.ConfigureEFContext();


var app = builder.Build();
app.ConfigureSwaggerUI();
app.UseHttpsRedirection();
app.MapTeamEndpoints();
app.Run();

