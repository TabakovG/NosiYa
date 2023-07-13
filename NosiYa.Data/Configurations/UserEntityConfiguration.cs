using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NosiYa.Data.Models;

namespace NosiYa.Data.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
           // builder.HasData(this.CreateUser());
        }

        /*private ApplicationUser[] CreateUser()
        {
            return new ApplicationUser[]
            {
                new ApplicationUser
                {
                    Id = Guid.Parse("2225B9B4-9CFF-8668-8B71-1B12E9B74545"),
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@abv.bg",
                    NormalizedEmail = "ADMIN@ABV.BG",
                },
                new ApplicationUser
                {
                    Id = Guid.Parse("5D55B9B4-9CFF-485B-8B71-1B12E9B7FE8F"),
                    UserName = "TabakovG",
                    NormalizedUserName = "TABAKOVG",
                    Email = "gtab@abv.bg",
                    NormalizedEmail = "GTAB@ABV.BG",
                }
            };
        }*/
    }
}
