using Microsoft.EntityFrameworkCore;
using TeamHistory.WebApi.Data;
using TeamHistory.WebApi.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSwaggerGen();
builder.Services.AddDbContext<TeamHistoryContext>();

var app = builder.Build();

app.ConfigureSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/v1/teams", async (TeamHistoryContext context) =>
{
    var teams = await context.Teams.ToListAsync();
    return teams is null ? Results.NotFound() : Results.Ok(teams);
}).Produces<IList<Team>>(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);

app.MapGet("/v1/teams/{id}", async (Guid id, TeamHistoryContext context) =>
{
    var teams = await context.Teams.FindAsync(id);
    return teams is null ? Results.NotFound() : Results.Ok(teams);
}).Produces<Team>(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);

app.MapPost("/v1/teams/", async (Team model, TeamHistoryContext context) =>
{
    context.Teams.Add(model);
    await context.SaveChangesAsync();
    return Results.Created($"/todoitems/{model.Id}", model);
}).Produces<Team>(StatusCodes.Status201Created).Produces(StatusCodes.Status400BadRequest);

app.MapPut("/v1/teams/{id}", async (Guid id, Team model, TeamHistoryContext context) =>
{
    var teamRegistred = await context.Teams.FindAsync(id);

    if (teamRegistred is null)
        return Results.NotFound();

    teamRegistred.Name = model.Name;
    teamRegistred.FoundationDate = model.FoundationDate;

    await context.SaveChangesAsync();

    return Results.NoContent();
}).Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status400BadRequest);

app.MapDelete("/v1/teams/{id}", async (Guid id, TeamHistoryContext context) =>
{
    var teamRegistred = await context.Teams.FindAsync(id);

    if (teamRegistred is null)
        return Results.NotFound();

    context.Teams.Remove(teamRegistred);
    await context.SaveChangesAsync();

    return Results.NoContent();
}).Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status400BadRequest);

app.Run();

