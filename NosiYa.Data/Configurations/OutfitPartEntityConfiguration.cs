namespace NosiYa.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Outfit;
    using Models.Enums;
    using static NosiYa.Common.SeedingConstants;

    public class OutfitPartEntityConfiguration : IEntityTypeConfiguration<OutfitPart>
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
                            Рамене - 31
                            Гръдна обиколка - 73",
                Color = "бял",
                RenterType = (RenterType)3,
                OutfitPartType = (OutfitPartType)4,
                Size = "-XS-",
                OutfitSetId = 1,
                OwnerId = Guid.Parse(FirstUserId)
            };

            outfitParts.Add(outfitPart);

            outfitPart = new OutfitPart()
            {
                Id = 2,
                Name = "Детски Елек",
                Description = @"Елек: 
                            Рамене - 32
                            Дължина - 34
                            Отвор за ръкав - 27",
                Color = "кафяв",
                RenterType = (RenterType)3,
                OutfitPartType = (OutfitPartType)4,
                Size = "-XS-",
                OutfitSetId = 1,
                OwnerId = Guid.Parse(SecondUserId)
            };

            outfitParts.Add(outfitPart);

            return outfitParts.ToArray();
        }
    };

}
