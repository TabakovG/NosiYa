using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NosiYa.Data.Models.Outfit;

namespace NosiYa.Data.Configurations
{
    public class OutfitEntityConfiguration : IEntityTypeConfiguration<OutfitSet>
    {
        public void Configure(EntityTypeBuilder<OutfitSet> builder)
        {
            builder
                .Property(x => x.PricePerDay)
                .HasPrecision(18, 2);
        }
    }
}
