using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NosiYa.Data.Models.Outfit;

namespace NosiYa.Data.Configurations
{
    public class OutfitEntityConfiguration : IEntityTypeConfiguration<OutfitBase>
    {
        public void Configure(EntityTypeBuilder<OutfitBase> builder)
        {
            builder
                .Property(x => x.PricePerDay)
                .HasPrecision(18, 2);
        }
    }
}
