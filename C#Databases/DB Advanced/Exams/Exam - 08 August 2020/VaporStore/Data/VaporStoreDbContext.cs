namespace VaporStore.Data
{
	using Microsoft.EntityFrameworkCore;
    using VaporStore.Data.Models;

    public class VaporStoreDbContext : DbContext
	{
		public VaporStoreDbContext()
		{
		}

		public VaporStoreDbContext(DbContextOptions options)
			: base(options)
		{
		}

        public DbSet<Card> Cards { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameTag> GameTags { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			if (!options.IsConfigured)
			{
				options
					.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder model)
		{
			model.Entity<GameTag>().HasKey(gt => new
			{
				gt.GameId,
				gt.TagId
			});

			model.Entity<GameTag>()
				.HasOne(gt => gt.Game)
					.WithMany(g => g.GameTags)
					.HasForeignKey(gt => gt.GameId);

			model.Entity<GameTag>()
				.HasOne(gt => gt.Tag)
					.WithMany(g => g.GameTags)
					.HasForeignKey(gt => gt.TagId);

			model.Entity<Purchase>()
				.HasOne(p => p.Card)
				.WithMany(c => c.Purchases)
				.HasForeignKey(p => p.CardId);

			model.Entity<Purchase>()
				.HasOne(p => p.Game)
				.WithMany(c => c.Purchases)
				.HasForeignKey(p => p.GameId);

			model.Entity<Game>()
				.HasOne(g => g.Developer)
				.WithMany(d => d.Games)
				.HasForeignKey(g => g.DeveloperId);

			model.Entity<Game>()
				.HasOne(g => g.Genre)
				.WithMany(d => d.Games)
				.HasForeignKey(g => g.GenreId);

			model.Entity<Card>()
				.HasOne(c => c.User)
				.WithMany(u => u.Cards)
				.HasForeignKey(c => c.UserId);
		}
	}
}