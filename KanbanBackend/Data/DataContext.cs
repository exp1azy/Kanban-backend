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

        public DbSet<users> users { get; set; }
        public DbSet<сards> cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config["ConnectionString"]);
        }
    }
}
