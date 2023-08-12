namespace NosiYa.Data.Configurations.SeedData
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using NosiYa.Data.Models.Outfit;
    using static Common.SeedingConstants;


	public class SeedOutfitRenterDateEntityConfiguration : IEntityTypeConfiguration<OutfitRenterDate>
	{
		public void Configure(EntityTypeBuilder<OutfitRenterDate> builder)
		{
            builder.HasData(this.CreateOrders());
		}

		private OutfitRenterDate[] CreateOrders()
		{
			ICollection<OutfitRenterDate> orders = new HashSet<OutfitRenterDate>();

			OutfitRenterDate order;

			order = new OutfitRenterDate
			{
				OrderId = Guid.NewGuid(),
				OutfitId = 1,
				RenterId = Guid.Parse(AdminId),
				DateRangeStart = DateTime.Parse("27-09-2023"),
				DateRangeEnd = DateTime.Parse("27-09-2023"),
				IsActive = true,
				IsApproved = false
			};

			orders.Add(order);

			order = new OutfitRenterDate
			{
				OrderId = Guid.NewGuid(),
				OutfitId = 1,
				RenterId = Guid.Parse(AdminId),
				DateRangeStart = DateTime.Parse("30-08-2023"),
				DateRangeEnd = DateTime.Parse("02-09-2023"),
				IsActive = true,
				IsApproved = true
			};

			orders.Add(order);

			order = new OutfitRenterDate
			{
				OrderId = Guid.NewGuid(),
				OutfitId = 2,
				RenterId = Guid.Parse(AdminId),
				DateRangeStart = DateTime.Parse("01-08-2023"),
				DateRangeEnd = DateTime.Parse("04-08-2023"),
				IsActive = true,
				IsApproved = true
			};

			orders.Add(order);

			order = new OutfitRenterDate
			{
				OrderId = Guid.NewGuid(),
				OutfitId = 2,
				RenterId = Guid.Parse(AdminId),
				DateRangeStart = DateTime.Parse("05-08-2023"),
				DateRangeEnd = DateTime.Parse("07-08-2023"),
				IsActive = true,
				IsApproved = false
			};

			orders.Add(order);

			return orders.ToArray();
		}
	}
};
