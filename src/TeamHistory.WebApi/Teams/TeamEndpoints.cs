using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TeamHistory.WebApi.Data;
using TeamHistory.WebApi.Entities;

/// <summary>
/// Teams endpoint
/// </summary>
public static class TeamEndpoints
{
    public static void MapTeamEndpoints(this WebApplication app)
    {
        app.MapGet("/v1/teams/", GetTeamsAsync)
           .Produces<IList<Team>>(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status404NotFound);

        app.MapGet("/v1/teams/{id}", GetTeamByIdAsync)
           .Produces<Team>(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status404NotFound);

        app.MapPut("/v1/teams/{id}", UpdateTeamAsync)
           .WithValidator<TeamCreateDto>()
           .Produces(StatusCodes.Status204NoContent)
           .Produces(StatusCodes.Status400BadRequest);

        app.MapDelete("/v1/teams/{id}", DeleteTeamAsync)
           .Produces(StatusCodes.Status204NoContent)
           .Produces(StatusCodes.Status404NotFound);

        app.MapPost("/v1/teams", CreateTeamAsync)
           .WithValidator<TeamCreateDto>()
           .Produces<Team>(StatusCodes.Status201Created)
           .Produces(StatusCodes.Status400BadRequest);
    }

    internal static async Task<IResult> GetTeamsAsync(TeamHistoryContext context)
    {
        var allTeams = await context.Teams
                                    .AsNoTracking()
                                    .ToListAsync();

        return allTeams is not null ? Results.Ok(allTeams) : Results.NotFound();
    }

    internal static async Task<IResult> GetTeamByIdAsync(TeamHistoryContext context, Guid id)
    {
        var teamRegistred = await context.Teams
                                         .FindAsync(id);

        return teamRegistred is not null ? Results.Ok(teamRegistred) : Results.NotFound();
    }

    internal static async Task<IResult> CreateTeamWithValidationAsync(TeamHistoryContext context, Team teamRequest, IValidator<Team> validator)
    {
        var validate = await validator.ValidateAsync(teamRequest);

        if (!validate.IsValid)
            return Results.BadRequest(validate.Errors);        

        await context.Teams.AddAsync(teamRequest);
        await context.SaveChangesAsync();

        return Results.Created($"v1/teams/{teamRequest.Id}", teamRequest);
    }

    internal static async Task<IResult> CreateTeamAsync(TeamHistoryContext context, TeamCreateDto teamRequest)
    {
        var team = teamRequest.FromDto();

        await context.Teams.AddAsync(team);
        await context.SaveChangesAsync();

        return Results.Created($"v1/teams/{team.Id}", team);
    }

    internal static async Task<IResult> UpdateTeamAsync(TeamHistoryContext context, Guid id, TeamCreateDto updateTeam)
    {
        var registredTeam = await context.Teams.FindAsync(id);

        if (registredTeam is null)        
            return Results.NotFound();        

        registredTeam.SetName(updateTeam.Name);

        await context.SaveChangesAsync();
        return Results.NoContent();
    }

    internal static async Task<IResult> DeleteTeamAsync(TeamHistoryContext context, Guid id)
    {
        var teamRegistred = await context.Teams.FindAsync(id);

        if (teamRegistred is null)
            return Results.NotFound();

        context.Teams.Remove(teamRegistred);
        await context.SaveChangesAsync();

        return Results.NoContent();
    }
}

