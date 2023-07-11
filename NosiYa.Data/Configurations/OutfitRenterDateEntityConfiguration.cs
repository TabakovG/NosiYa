namespace NosiYa.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Outfit;

    public class OutfitRenterDateEntityConfiguration : IEntityTypeConfiguration<OutfitRenterDate>
    {
        public void Configure(EntityTypeBuilder<OutfitRenterDate> builder)
        {
            builder
                .HasKey(x => new { x.OutfitId, x.Date });

            builder
                .HasOne(x => x.Outfit)
                .WithMany(u => u.OutfitRenterDates)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Renter)
                .WithMany(ord => ord.OutfitRenterDates)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
