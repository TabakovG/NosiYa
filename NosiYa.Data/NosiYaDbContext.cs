
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
        public NosiYaDbContext(DbContextOptions<NosiYaDbContext> options)
            : base(options)
        {
        }


        public DbSet<OutfitPart> OutfitParts { get; set; } = null!;
        public DbSet<OutfitSet> OutfitSets { get; set; } = null!;
        public DbSet<OutfitForCart> OutfitsForCarts { get; set; } = null!;
        public DbSet<OutfitRenterDate> OutfitRenterDates { get; set; } = null!;
        public DbSet<Region> Regions { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(NosiYaDbContext)) ?? Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(builder);
        }
    }
}