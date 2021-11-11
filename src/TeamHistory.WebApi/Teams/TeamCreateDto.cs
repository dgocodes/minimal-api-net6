using TeamHistory.WebApi.Entities;

public record TeamCreateDto(string Name, string Initials, DateTime FoundationDate)
{
    public Team FromDto() => new(Name, Initials, FoundationDate);   
}

