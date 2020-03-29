
using Microsoft.EntityFrameworkCore;

namespace ScrumBlazor.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }

        public DbSet<Member> Members { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=sqlitedemo.db");
    }
}