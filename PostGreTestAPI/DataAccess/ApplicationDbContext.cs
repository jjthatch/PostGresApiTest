using Microsoft.EntityFrameworkCore;

namespace PostGreTestAPI.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Potato> potatoes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Configuring our database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=mydatabase;Username=username;Password=password;Port=5432");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
