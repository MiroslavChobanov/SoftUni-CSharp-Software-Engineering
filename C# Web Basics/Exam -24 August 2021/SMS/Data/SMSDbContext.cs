namespace SMS.Data
{
    using Microsoft.EntityFrameworkCore;
    using SMS.Data.Models;

    public class SMSDbContext : DbContext
    {
        public DbSet<User> Users { get; init; }
        public DbSet<Product> Products { get; init; }
        public DbSet<Cart> Carts { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SMS;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    }
}