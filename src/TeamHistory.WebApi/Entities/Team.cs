namespace TeamHistory.WebApi.Entities
{
    public class Team
    {
        public Team(string name, DateTime foundationDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            FoundationDate = foundationDate;
        }

        public Guid Id { get; private set; }

        public string Name { get; set; }

        public DateTime FoundationDate { get; set; }
    }
}
