using Microsoft.EntityFrameworkCore;

namespace AuthWebApp.Models
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext()
        {}

        public UsersDbContext(DbContextOptions options) : base(options){}

        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Data Source=Home-PC\\SQLEXPRESS;Initial Catalog=UsersDb;Persist Security Info=True;User ID=sa;Password=Admin1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
