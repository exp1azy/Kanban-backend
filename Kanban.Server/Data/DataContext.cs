using Microsoft.EntityFrameworkCore;

namespace Kanban.Server.Data
{
    public class DataContext(IConfiguration config) : DbContext
    {
        private readonly IConfiguration _config = config;

        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config["ConnectionString"]);
        }
    }
}
