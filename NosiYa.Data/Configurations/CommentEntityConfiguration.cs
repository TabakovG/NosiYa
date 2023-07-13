namespace NosiYa.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static NosiYa.Common.SeedingConstants;

    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasOne(x => x.Event)
                .WithMany(x => x.Comments)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(this.CreateComments());

        }

        private Comment[] CreateComments()
        {
            return new Comment[]
            {
                new Comment
                {
                    Id = 1,
                    Content = "Страхотна локация! :) ",
                    OwnerId = Guid.Parse(FirstUserId),
                    EventId = 1,
                    IsApproved = true
                },
                new Comment
                {
                    Id = 2,
                    Content = "бля бля бля",
                    OwnerId = Guid.Parse(SecondUserId),
                    EventId = 1,
                    IsApproved = false
                },
                new Comment
                {
                    Id = 3,
                    Content = "Този фестивал вече е добавен.",
                    OwnerId = Guid.Parse(FirstUserId),
                    EventId = 2,
                    IsApproved = true
                }
            };
        }
    }
}
