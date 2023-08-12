namespace NosiYa.Data.Configurations.SeedData
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using NosiYa.Data.Models;
    
    public class SeedImageEntityConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {

            builder.HasData(this.CreateImages());
        }

        private Image[] CreateImages()
        {
            ICollection<Image> images = new HashSet<Image>();

            Image image;

            //Outfitset 1 -----------

            image = new Image()
            {
                Id = 171,
                Url = "/images/common/nosiq1/3245e510-10ee-4ac5-a181-3141cbef91a7.jpg",
                OutfitSetId = 1,
                IsDefault = true
            };
            images.Add(image);

            image = new Image()
            {
                Id = 172,
                Url = "/images/common/nosiq1/da5b3ea8-df94-4602-a255-251ac228396a.jpg",
                OutfitSetId = 1
            };
            images.Add(image);

            image = new Image()
            {
	            Id = 173,
	            Url = "/images/common/nosiq1/f24a886e-d320-4f8d-afe3-2156eca67d0c.jpg",
	            OutfitSetId = 1
            };
            images.Add(image);

            //Outfitset 2 -----------


			image = new Image()
            {
	            Id = 174,
	            Url = "/images/common/nosiq17/054848d5-9b31-4b18-a1a2-bc9f91bac96d.jpg",
	            OutfitSetId = 2,
	            IsDefault = true
            };
            images.Add(image);

			image = new Image()
            {
                Id = 175,
                Url = "/images/common/nosiq17/82c31014-e3d5-42b6-b7ad-0bb533e401ef.jpg",
                OutfitSetId = 2
            };
            images.Add(image);

            image = new Image()
            {
	            Id = 176,
	            Url = "/images/common/nosiq17/ba4d583c-31d7-44c1-99a9-b02a7006d9eb.jpg",
	            OutfitSetId = 2
            };
            images.Add(image);

            //Event 1 -----------

			image = new Image()
            {
                Id = 177,
                Url = "/images/event/02b1f635-597e-421c-b7c0-25542c6bf6fe.jpg",
                EventId = 1
            };
            images.Add(image);

            image = new Image()
            {
                Id = 178,
                Url = "/images/event/92c33c52-81c0-4234-9540-d110fe071f97.jpeg",
                EventId = 1
            };
            images.Add(image);

            //Event 2 -----------

            image = new Image()
            {
	            Id = 179,
	            Url = "/images/event/92c33c52-81c0-4234-9540-d110fe071f97.jpeg",
	            EventId = 2,
	            IsDefault = true
            };
            images.Add(image);

            //Event 3 -----------

            image = new Image()
            {
	            Id = 180,
	            Url = "/images/event/f74ddf23-68d9-4b58-a982-18f3cba1a1f3.jpg",
	            EventId = 73,
	            IsDefault = true

			};
            images.Add(image);

            image = new Image()
            {
	            Id = 181,
	            Url = "/images/event/59b6dce3-8fe8-42a1-8998-7967e282c7b8.jpg",
	            EventId = 73
            };
            images.Add(image);

            image = new Image()
            {
	            Id = 182,
	            Url = "/images/event/30fde865-6e69-4408-b490-ff5b0c49e70e.jpg",
	            EventId = 73
            };
            images.Add(image);

			//Region 1 -----------

			image = new Image()
			{
				Id = 183,
				Url = "/images/region/12f52c5a-24ac-4b8b-99fb-34e245967555.jpg",
				RegionId = 1,
                IsDefault = true
			};
			images.Add(image);

			image = new Image()
			{
				Id = 184,
				Url = "/images/region/85deec0a-67cc-47ab-997b-a17ad7546041.jpg",
				RegionId = 1
			};
			images.Add(image);

			//Region 2 -----------

			image = new Image()
			{
				Id = 185,
				Url = "/images/region/04d45bb6-1100-453f-9377-862a4c10dda6.jpg",
				RegionId = 72,
				IsDefault = true
			};
			images.Add(image);

			image = new Image()
			{
				Id = 186,
				Url = "/images/region/1ccc5fc0-15c9-4fa9-bad8-a3f23ca301c1.jpg",
				RegionId = 72
			};
			images.Add(image);

			//Region 3 -----------

			image = new Image()
			{
				Id = 187,
				Url = "/images/region/ce68a779-2c71-488b-8cde-25b01df35ec5.jpg",
				RegionId = 73,
				IsDefault = true
			};
			images.Add(image);

			//OutfitParts with demo pictures -----------
			for (int i = 0; i < 7; i++)
			{
				image = new Image()
				{
					Id = 188 +i,
					Url = "/images/common/nosiq17/ba4d583c-31d7-44c1-99a9-b02a7006d9eb.jpg",
					OutfitPartId = 1+i,
					IsDefault = true
				};
				images.Add(image);
			}

			return images.ToArray();
        }
    }
}
