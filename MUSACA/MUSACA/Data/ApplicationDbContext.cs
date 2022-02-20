namespace MUSACA.Data
{
    using Microsoft.EntityFrameworkCore;
    using MUSACA.Data.Models;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; init; }
        public DbSet<Order> Orders { get; init; }
        public DbSet<Product> Products { get; init; }
        public DbSet<Receipt> Receipts { get; init; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MUSACA;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
