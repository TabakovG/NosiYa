namespace NosiYa.Data.Configurations.SeedData
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using NosiYa.Data.Models.Enums;
    using NosiYa.Data.Models.Outfit;
    using static Common.SeedingConstants;

    public class SeedOutfitPartEntityConfiguration : IEntityTypeConfiguration<OutfitPart>
    {
        public void Configure(EntityTypeBuilder<OutfitPart> builder)
        {

            builder.HasData(this.CreateOutfitParts());
        }

        private OutfitPart[] CreateOutfitParts()
        {
            ICollection<OutfitPart> outfitParts = new HashSet<OutfitPart>();

            OutfitPart outfitPart;

            outfitPart = new OutfitPart()
            {
                Id = 1,
                Name = "Детска Риза",
                Description = @"Риза: 
                            Рамене - 31см
                            Гръдна обиколка - 73см,
							Дължина - 41см,
							Обиколка на ръкав - 30см,
							Дължина на ръкав от рамото - 42см",
                Color = "бял",
                RenterType = (RenterType)3,
                OutfitPartType = (OutfitPartType)4,
                Size = "-XS-",
                OutfitSetId = 2,
                OwnerId = Guid.Parse(AdminId)
            };

            outfitParts.Add(outfitPart);

            outfitPart = new OutfitPart()
            {
                Id = 2,
                Name = "Детски Елек",
                Description = @"Елек: 
                            Рамене - 32см
                            Дължина - 34см
                            Отвор за ръкав - 27см",
                Color = "кафяв",
                RenterType = (RenterType)3,
                OutfitPartType = (OutfitPartType)5,
                Size = "-XS-",
                OutfitSetId = 2,
                OwnerId = Guid.Parse(AdminId)
            };

            outfitParts.Add(outfitPart);

            outfitPart = new OutfitPart()
            {
	            Id = 3,
	            Name = "Панталон",
	            Description = @"Талия - регулира се с връзки,
								Дължина - 71см",
	            Color = "кафяв",
	            RenterType = (RenterType)3,
	            OutfitPartType = (OutfitPartType)8,
	            Size = "-XS-",
	            OutfitSetId = 2,
	            OwnerId = Guid.Parse(UserId)
            };

            outfitParts.Add(outfitPart);

            outfitPart = new OutfitPart()
            {
	            Id = 4,
	            Name = "Пояс",
	            Description = @"Ширина - 27см, Дължина - 146см",
	            Color = "червен",
	            RenterType = (RenterType)3,
	            OutfitPartType = (OutfitPartType)1,
	            Size = "-XS-",
	            OutfitSetId = 2,
	            OwnerId = Guid.Parse(AdminId)
            };

            outfitParts.Add(outfitPart);

			outfitPart = new OutfitPart()
			{
				Id = 5,
				Name = "Престилка",
				Description = @"Ширина - 46см, Дължина - 57см",
				Color = "черен",
				RenterType = (RenterType)2,
				OutfitPartType = (OutfitPartType)2,
				Size = "-S-М-",
				OutfitSetId = 1,
				OwnerId = Guid.Parse(AdminId)
			};

			outfitParts.Add(outfitPart);

			outfitPart = new OutfitPart()
			{
				Id = 6,
				Name = "Риза",
				Description = @"Рамене - 41см, Гръдна обиколка - 106см, Дължина - 56см, Ръкав обиколка - 36см, Ръкав дължина от рамото - 49",
				Color = "бял",
				RenterType = (RenterType)2,
				OutfitPartType = (OutfitPartType)4,
				Size = "-S-М-",
				OutfitSetId = 1,
				OwnerId = Guid.Parse(AdminId)
			};

			outfitParts.Add(outfitPart);

			outfitPart = new OutfitPart()
			{
				Id = 7,
				Name = "Сукман",
				Description = @"Рамене - 40см, Гръдна обиколка - 90см, Талия - 74см, Дължина - 103см",
				Color = "червен",
				RenterType = (RenterType)2,
				OutfitPartType = (OutfitPartType)6,
				Size = "-S-М-",
				OutfitSetId = 1,
				OwnerId = Guid.Parse(AdminId)
			};

			outfitParts.Add(outfitPart);

			return outfitParts.ToArray();
		}
    };

}
