namespace NosiYa.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Outfit;

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

        }
    }
}
