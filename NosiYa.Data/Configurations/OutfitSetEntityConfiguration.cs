namespace NosiYa.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Outfit;
    using Models.Enums;
    using static NosiYa.Common.SeedingConstants;

    public class OutfitSetEntityConfiguration : IEntityTypeConfiguration<OutfitSet>
    {
        public void Configure(EntityTypeBuilder<OutfitSet> builder)
        {
            builder
                .Property(x => x.PricePerDay)
                .HasPrecision(18, 2);

            builder
                .Property(h => h.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.HasData(this.CreateOutfitSets());
        }

        private OutfitSet[] CreateOutfitSets()
        {
            ICollection<OutfitSet> outfits = new HashSet<OutfitSet>();

            OutfitSet outfit;

            outfit = new OutfitSet()
            {
                Id = 1,
                Name = "Носия 17",
                Description =
                    @"Родопска детска носия за момче.
                    Състои се от:
                    - Риза
                    - Елек
                    - Панталон
                    - Пояс

                    Подходяща за момче между 7 и 9 години.
                    ",
                RegionId = 1,
                PricePerDay = 25,
                Color = "Кафяв",
                RenterType = (RenterType)3,
                IsAvailable = true,
                Size = "XS,S"

            };

            outfits.Add(outfit);

            return outfits.ToArray();

        }
    }
}
