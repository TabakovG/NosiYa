using NosiYa.Data.Models;

namespace NosiYa.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using NosiYa.Data.Models.Outfit;

    public class OutfitForCartEntityConfiguration : IEntityTypeConfiguration<OutfitForCart>
    {
        public void Configure(EntityTypeBuilder<OutfitForCart> builder)
        {
            builder
                .HasOne(c => c.Cart)  
                .WithMany(o => o.Outfits)
                .HasForeignKey(c=>c.CartId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
