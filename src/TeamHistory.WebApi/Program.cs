using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TeamHistory.WebApi.Data;
using TeamHistory.WebApi.Dto;
using TeamHistory.WebApi.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureSwaggerGen();
builder.Services.AddDbContext<TeamHistoryContext>();
builder.Services.FluentValidationConfigure();

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

app.MapPost("/v1/teams/", async (TeamCreateDto teamRequest, IValidator<TeamCreateDto> validator, TeamHistoryContext context) =>
{
    var validationResult = validator.Validate(teamRequest);

    if (!validationResult.IsValid)    
        return Results.ValidationProblem(validationResult.ToDictionary());

    var team = new Team(teamRequest.Name, teamRequest.FoundationDate);

    context.Teams.Add(team);
    await context.SaveChangesAsync();
    return Results.Created($"/todoitems/{team.Id}", team);
}).Produces<Team>(StatusCodes.Status201Created).Produces(StatusCodes.Status400BadRequest);

app.MapPut("/v1/teams/{id}", async (Guid id, TeamCreateDto teamRequest, IValidator<TeamCreateDto> validator, TeamHistoryContext context) =>
{
    var validationResult = validator.Validate(teamRequest);

    if (!validationResult.IsValid)
        return Results.ValidationProblem(validationResult.ToDictionary());

    var teamRegistred = await context.Teams.FindAsync(id);

    if (teamRegistred is null)
        return Results.NotFound();

    teamRegistred.SetName(teamRequest.Name);
    teamRegistred.SetFoundationDate(teamRequest.FoundationDate);

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

