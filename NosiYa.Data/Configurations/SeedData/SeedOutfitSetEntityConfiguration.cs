namespace NosiYa.Data.Configurations.SeedData
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Enums;
    using Models.Outfit;
    using static Common.SeedingConstants;

    public class SeedOutfitSetEntityConfiguration : IEntityTypeConfiguration<OutfitSet>
    {
        public void Configure(EntityTypeBuilder<OutfitSet> builder)
        {
            builder.HasData(this.CreateOutfitSets());
        }

        private OutfitSet[] CreateOutfitSets()
        {
            ICollection<OutfitSet> outfits = new HashSet<OutfitSet>();

            OutfitSet outfit;

            outfit = new OutfitSet()
            {
                Id = 1,
                Name = "Носия 01",
                Description =
                    @"Тракийска женска носия.
                    Състои се от:
                    - Риза
					-Сукман
					-Престилка

                    Ръчно шити орнаменти. 
					Към носията Има възможност да се добавят различни аксесоари - цветя, накити и др.
                    ",
                RegionId = 73,
                PricePerDay = 25,
                Color = "Червен",
                RenterType = (RenterType)2,
                IsAvailable = true,
                Size = "-S-M-"

            };

            outfits.Add(outfit);

			outfit = new OutfitSet()
			{
				Id = 2,
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
				PricePerDay = 30,
				Color = "Кафяв",
				RenterType = (RenterType)3,
				IsAvailable = true,
				Size = "-XS-S-"

			};

			outfits.Add(outfit);

			outfit = new OutfitSet()
			{
				Id = InMaintenanceSetContainerId, //3
				Name = "In maintenance",
				Description =
					@"Тази носия е неактивна и служи като контейнер за елементите/частите, когато са временно неактивни.
					Тази носия не трябва да бъде активирана!
                    ",
				RegionId = 1,
				PricePerDay = 0,
				Color = "",
				RenterType = (RenterType)1,
				IsAvailable = false,
				Size = "-S-"

			};

			outfits.Add(outfit);

			return outfits.ToArray();

        }
    }
}
