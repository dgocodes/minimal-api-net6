using Microsoft.EntityFrameworkCore;
using TeamHistory.WebApi.Entities;

namespace TeamHistory.WebApi.Data
{
    public class TeamHistoryContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("DataSource=TeamHistory.db;Cache=Shared");
    }
}
