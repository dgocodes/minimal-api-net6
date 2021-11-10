using TeamHistory.WebApi.Entities;

public record TeamCreateDto(string Name, DateTime FoundationDate)
{
    public Team FromDto() => new(Name, FoundationDate);   
}

