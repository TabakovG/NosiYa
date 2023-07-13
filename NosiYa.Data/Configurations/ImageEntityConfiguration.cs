
namespace NosiYa.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class ImageEntityConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {

            builder.HasData(this.CreateImages());
        }

        private Image[] CreateImages()
        {
            ICollection<Image> images = new HashSet<Image>();

            Image image;

            image = new Image()
            {
                Id = 1,
                Url = "Set image",
                OutfitSetId = 1
            };
            images.Add(image);

            image = new Image()
            {
                Id = 2,
                Url = "Set image",
                OutfitPartId = 1
            };
            images.Add(image);
            image = new Image()
            {
                Id = 3,
                Url = "Set image",
                OutfitPartId = 2
            };
            images.Add(image);

            return images.ToArray();
        }
    }
}
