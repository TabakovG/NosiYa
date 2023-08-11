using NosiYa.Data;
namespace NosiYa.Services.Tests
{
	using NosiYa.Data.Models.Outfit;
	using static Common.SeedingConstants;

	public static class DbSeedData
	{
		public static ICollection<OutfitRenterDate> OutfitRenterDates = new HashSet<OutfitRenterDate>();

		public static void SeedDatabase(NosiYaDbContext dbContext)
		{

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

			dbContext.OutfitRenterDates.AddRange(OutfitRenterDates);
			dbContext.SaveChanges();
		}
	}
}
