namespace TeamHistory.WebApi.Entities
{
    public class Team
    {
        public Team(string name, string initials, DateTime foundationDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Initials = initials;
            FoundationDate = foundationDate;
        }

        public void SetName(string name) => Name = name;
        public void SetFoundationDate(DateTime foundationDate) => FoundationDate = foundationDate;

        public Guid Id { get; private set; }

        public string Name { get; private set; }    

        public string Initials { get; set; }

        public DateTime FoundationDate { get; private set; }
    }
}
