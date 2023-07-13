
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NosiYa.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class RegionEntityConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {

            builder.HasData(this.CreateRegions());
        }

        private Region[] CreateRegions()
        {
            ICollection<Region> regions = new HashSet<Region>();

            Region region;

            region = new Region
            {
                Id = 1,
                Name = "Родопски Регион",
                Description = "Обхваща Родопа планина."
            };
            regions.Add(region);

            return regions.ToArray();
        }
    }
}
