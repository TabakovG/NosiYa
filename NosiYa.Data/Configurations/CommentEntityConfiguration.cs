namespace NosiYa.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static Common.SeedingConstants;

    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasOne(x => x.Event)
                .WithMany(x => x.Comments)
                .OnDelete(DeleteBehavior.NoAction);

			builder
				.Property(h => h.CreatedOn)
				.HasDefaultValueSql("GETDATE()");
        }
    }
}
