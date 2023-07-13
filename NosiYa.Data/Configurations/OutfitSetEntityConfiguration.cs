﻿namespace NosiYa.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Outfit;
    using Models.Enums;

    public class OutfitSetEntityConfiguration : IEntityTypeConfiguration<OutfitSet>
    {
        public void Configure(EntityTypeBuilder<OutfitSet> builder)
        {
            builder
                .Property(x => x.PricePerDay)
                .HasPrecision(18, 2);

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
                    @"Родопска детска носия за момче.Риза, елек, панталон и пояс,
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
                Size = "XS",
                
            };

            outfits.Add(outfit);

            return outfits.ToArray();

        }
    }
}
