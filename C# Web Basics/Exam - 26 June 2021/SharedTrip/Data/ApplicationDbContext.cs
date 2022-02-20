namespace SharedTrip.Data
{
    using Microsoft.EntityFrameworkCore;
    using SharedTrip.Data.Models;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; init; }
        public DbSet<Trip> Trips { get; init; }
        public DbSet<UserTrip> UserTrips { get; init; }
        public ApplicationDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SharedTrip;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>()
                .HasKey(ut => new { ut.UserId, ut.TripId });
            modelBuilder.Entity<UserTrip>()
                .HasOne(ut => ut.User)
                .WithMany(b => b.UserTrips)
                .HasForeignKey(ut => ut.UserId);
            modelBuilder.Entity<UserTrip>()
                .HasOne(ut => ut.Trip)
                .WithMany(c => c.UserTrips)
                .HasForeignKey(ut => ut.TripId);
        }
    }
}
