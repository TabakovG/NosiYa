namespace NosiYa.Data.Configurations.SeedData
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class CartEntityConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            //builder.HasData(CreateCarts());
        }

        /*  private Cart[] CreateCarts()
          {
              return new Cart[]
              {
                  new Cart()
                  {
                      Id = 1,
                      OwnerId = Guid.Parse("5D55B9B4-9CFF-485B-8B71-1B12E9B7FE8F")
                  },
                  new Cart()
                  {
                      Id = 2,
                      OwnerId = Guid.Parse("2225B9B4-9CFF-8668-8B71-1B12E9B74545")
                  }
              };
          }*/
    }
}
