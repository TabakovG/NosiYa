
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

        public DbSet<Accessory> Accessories { get; set; } = null!;
        public DbSet<Belt> Belts { get; set; } = null!;
        public DbSet<Legs> Legs { get; set; } = null!;
        public DbSet<OutfitSet> OutfitSets { get; set; } = null!;
        public DbSet<Shirt> Shirts { get; set; } = null!;
        public DbSet<Vest> Vests { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<OutfitRenterDate> OutfitRenterDates { get; set; } = null!;
        public DbSet<Region> Regions { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(NosiYaDbContext)) ?? Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(builder);
        }
    }
}