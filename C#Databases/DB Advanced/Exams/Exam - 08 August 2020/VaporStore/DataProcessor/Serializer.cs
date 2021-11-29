namespace VaporStore.DataProcessor
{
	using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Export;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
			var genres = context.Genres
				.ToArray()
				.Where(g => genreNames.Contains(g.Name))
				.Select(g => new
                {
					Id = g.Id,
					Genre = g.Name,
					Games = g.Games
						.Where(ga => ga.Purchases.Any())
						.Select(ga => new 
						{
							Id = ga.Id,
							Title = ga.Name,
							Developer = ga.Developer.Name,
							Tags = string.Join(", ", ga.GameTags
										.Select(gt => gt.Tag.Name)),
							Players = ga.Purchases.Count
						})
						.OrderByDescending(ga => ga.Players)
						.ThenBy(ga => ga.Id)
						.ToArray(),
						TotalPlayers = g.Games.Sum(ga => ga.Purchases.Count)
				})
				.OrderByDescending(g => g.TotalPlayers)
				.ThenBy(g => g.Id)
				.ToArray();

			var genresJson = JsonConvert.SerializeObject(genres, Formatting.Indented);

			return genresJson;
		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
			var purchaseType = Enum.Parse<PurchaseType>(storeType);

			var users = context.Users
				.ToArray()
				.Where(u => u.Cards.Any(c => c.Purchases.Any()))
				.Select(u => new ExportUserDto()
				{
					Username = u.Username,
					Purchases = context.Purchases
							.ToArray()
							.Where(p => p.Card.User.Username == u.Username && p.Type == purchaseType)
							.OrderBy(p => p.Date)
							.Select(p => new ExportPurchaseDto()
							{
								Card = p.Card.Number,
								Cvc = p.Card.Cvc,
								Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
								Game = new ExportGameDto()
								{
									Name = p.Game.Name,
									Genre = p.Game.Genre.Name,
									Price = p.Game.Price
								}
							})
							.ToArray(),
					TotalSpent = context.Purchases
									.ToArray()
									.Where(p => p.Card.User.Username == u.Username && p.Type == purchaseType)
									.Sum(p => p.Game.Price)
				})
				.Where(u => u.Purchases.Length > 0)
				.OrderByDescending(u => u.TotalSpent)
				.ThenBy(u => u.Username)
				.ToArray();

			var writer = new StringWriter();

			var ns = new XmlSerializerNamespaces();
			ns.Add(string.Empty, string.Empty);

			var serializer = new XmlSerializer(typeof(ExportUserDto[]), new XmlRootAttribute("Users"));
			serializer.Serialize(writer, users, ns);

			var usersXml = writer.GetStringBuilder();

			return usersXml.ToString().TrimEnd();
		}
	}
}