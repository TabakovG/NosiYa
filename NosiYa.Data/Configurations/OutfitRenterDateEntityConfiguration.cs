using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NosiYa.Data.Models;
using NosiYa.Data.Models.Outfit;

namespace NosiYa.Data.Configurations
{
    public class OutfitRenterDateEntityConfiguration : IEntityTypeConfiguration<OutfitRenterDate>
    {
        public void Configure(EntityTypeBuilder<OutfitRenterDate> builder)
        {
            builder
                .HasKey(x => new { x.OutfitId, x.RenterId });

            builder
                .HasOne(x => x.Outfit)
                .WithMany(u => u.OutfitRenterDates)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x=>x.Renter)
                .WithMany(ord=>ord.OutfitRenterDates)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
