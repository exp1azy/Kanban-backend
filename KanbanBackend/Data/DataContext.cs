using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;

        public DataContext(IConfiguration config)
        {
            _config = config;
            Database.EnsureCreated();
        }

        public DbSet<User> User { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<Board> Board { get; set; } 
        public DbSet<BoardColumn> BoardColumn { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config["ConnectionString"]);
        }
    }
}
