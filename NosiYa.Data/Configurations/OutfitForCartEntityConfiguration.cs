using NosiYa.Data.Models;

namespace NosiYa.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using Models.Outfit;

    public class OutfitForCartEntityConfiguration : IEntityTypeConfiguration<OutfitForCart>
    {
        public void Configure(EntityTypeBuilder<OutfitForCart> builder)
        {
            builder
                .HasOne(c => c.Cart)  
                .WithMany(o => o.Outfits)
                .HasForeignKey(c=>c.CartId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(this.CreateCartItem());
        }

		private OutfitForCart[] CreateCartItem()
		{
			return new OutfitForCart[]
			{
				new OutfitForCart
				{
					Id = 171,
					OutfitId = 1,
					FromDate = DateTime.Parse("17-08-2023"),
					ToDate = DateTime.Parse("19-08-2023"),
					CartId = 1
				},
				new OutfitForCart
				{
					Id = 172,
					OutfitId = 1,
					FromDate = DateTime.Parse("22-08-2023"),
					ToDate = DateTime.Parse("22-08-2023"),
					CartId = 1
				},
				new OutfitForCart
				{
					Id = 173,
					OutfitId = 1,
					FromDate = DateTime.Parse("22-08-2023"),
					ToDate = DateTime.Parse("24-08-2023"),
					CartId = 2
				},
				new OutfitForCart
				{
					Id = 174,
					OutfitId = 2,
					FromDate = DateTime.Parse("14-08-2023"),
					ToDate = DateTime.Parse("17-08-2023"),
					CartId = 2
				},
				new OutfitForCart
				{
					Id = 175,
					OutfitId = 2,
					FromDate = DateTime.Parse("22-08-2023"),
					ToDate = DateTime.Parse("24-08-2023"),
					CartId = 2
				},
			};
		}
	}

}
