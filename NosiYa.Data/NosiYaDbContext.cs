using NosiYa.Data.Configurations;
using NosiYa.Data.Configurations.SeedData;

namespace NosiYa.Data
{

    using System.Reflection;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Outfit;

    public class NosiYaDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly bool SeedDb;

        public NosiYaDbContext(DbContextOptions<NosiYaDbContext> options, bool seed = true)
            : base(options)
        {
            this.SeedDb = seed;
        }


        public DbSet<OutfitPart> OutfitParts { get; set; } = null!;
        public DbSet<OutfitSet> OutfitSets { get; set; } = null!;
        public DbSet<OutfitForCart> OutfitsForCarts { get; set; } = null!;
        public DbSet<OutfitRenterDate> OutfitRenterDates { get; set; } = null!;
        public DbSet<Region> Regions { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*Assembly assembly = Assembly.GetAssembly(typeof(NosiYaDbContext)) ?? Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(builder);*/


            builder.ApplyConfiguration(new CommentEntityConfiguration());
            builder.ApplyConfiguration(new OutfitForCartEntityConfiguration());
            builder.ApplyConfiguration(new OutfitRenterDateEntityConfiguration());
            builder.ApplyConfiguration(new OutfitSetEntityConfiguration());

            if (this.SeedDb)
            {
                builder.ApplyConfiguration(new SeedCommentEntityConfiguration());
                builder.ApplyConfiguration(new SeedEventEntityConfiguration());
                builder.ApplyConfiguration(new SeedImageEntityConfiguration());
                builder.ApplyConfiguration(new SeedOutfitForCartEntityConfiguration());
                builder.ApplyConfiguration(new SeedOutfitPartEntityConfiguration());
                builder.ApplyConfiguration(new SeedOutfitRenterDateEntityConfiguration());
                builder.ApplyConfiguration(new SeedOutfitSetEntityConfiguration());
                builder.ApplyConfiguration(new SeedRegionEntityConfiguration());
            }
            base.OnModelCreating(builder);
        }
    }
}