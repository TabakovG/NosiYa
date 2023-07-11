namespace NosiYa.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class OutfitRenterDateEntityConfiguration : IEntityTypeConfiguration<OutfitRenterDate>
    {
        public void Configure(EntityTypeBuilder<OutfitRenterDate> builder)
        {
            builder
                .HasKey(x => new { OutfitId = x.OutfitSetId, x.RenterId });

            builder
                .HasOne(x => x.Outfit)
                .WithMany(u => u.OutfitRenterDates)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Renter)
                .WithMany(ord => ord.OutfitRenterDates)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
