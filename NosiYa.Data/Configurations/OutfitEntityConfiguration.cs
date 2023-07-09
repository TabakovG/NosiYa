using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NosiYa.Data.Models.Outfit;

namespace NosiYa.Data.Configurations
{
    public class OutfitEntityConfiguration : IEntityTypeConfiguration<OutfitPart>
    {
        public void Configure(EntityTypeBuilder<OutfitPart> builder)
        {
            builder
                .Property(x => x.PricePerDay)
                .HasPrecision(18, 2);
        }
    }
}
