using Microsoft.EntityFrameworkCore;
using NosiYa.Data;
using NosiYa.Data.Models;

namespace NosiYa.Services.Tests
{
	using NosiYa.Data.Models.Outfit;
	using static Common.SeedingConstants;

	public static class DbSeedData
	{
		public static ICollection<OutfitRenterDate> OutfitRenterDates = new HashSet<OutfitRenterDate>();
		public static ICollection<Cart> Carts = new HashSet<Cart>();
		public static ICollection<ApplicationUser> Users = new HashSet<ApplicationUser>();
		public static ICollection<OutfitForCart> OutfitsForCarts = new HashSet<OutfitForCart>();

		public static void SeedDatabase(NosiYaDbContext dbContext)
		{
			var admin = new ApplicationUser
			{
				Id = Guid.Parse("7C34FB52-0FDB-4CD7-027F-08DB822AA1B3"),
				UserName = "admin",
				NormalizedUserName = "ADMIN",
				Email = "admin@example.com",
				NormalizedEmail = "ADMIN@EXAMPLE.COM",
				EmailConfirmed = false,
				PasswordHash = "random_password_hash",
				SecurityStamp = "random_security_stamp",
				ConcurrencyStamp = "random_concurrency_stamp",
				PhoneNumber = "1234567890",


			};
			admin.Cart.Outfits = new HashSet<OutfitForCart>()
			{
				new OutfitForCart
				{
					Id = 333,
					OutfitId = 2,
					FromDate = DateTime.Parse("14-08-2023"),
					ToDate = DateTime.Parse("17-08-2023"),
					CartId = admin.Cart.Id,
					IsActive = true
				}};
			Users.Add(admin);
			dbContext.Users.AddRange(Users);
			dbContext.SaveChanges();

			OutfitRenterDates = new HashSet<OutfitRenterDate>()
			{
				new OutfitRenterDate
				{
					OrderId = Guid.NewGuid(),
					OutfitId = 2,
					RenterId = Guid.Parse(AdminId),
					DateRangeStart = DateTime.Parse("27-09-2023"),
					DateRangeEnd = DateTime.Parse("28-09-2023"),
					IsActive = false,
					IsApproved = false
				},
			};

			Carts = new HashSet<Cart>()
			{

				new Cart()
				{
					Id = 11,
					OwnerId = Guid.Parse("5D55B9B4-9CFF-485B-8B71-1B12E9B7FE8F")
				},
				new Cart()
				{
					Id = 12,
					OwnerId = Guid.Parse("2225B9B4-9CFF-8668-8B71-1B12E9B74545")
				}

			};

			OutfitsForCarts = new HashSet<OutfitForCart>()
			{

				new OutfitForCart
			{
				Id = 222,
				OutfitId = 2,
				FromDate = DateTime.Parse("14-08-2023"),
				ToDate = DateTime.Parse("17-08-2023"),
				CartId = 12,
				IsActive = false
			}};

			dbContext.OutfitsForCarts.AddRange(OutfitsForCarts);
			dbContext.OutfitRenterDates.AddRange(OutfitRenterDates);
			dbContext.Carts.AddRange(Carts);
			dbContext.SaveChanges();
		}
	}
}
