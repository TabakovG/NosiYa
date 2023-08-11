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

			builder.HasData(this.CreateComments());

        }

        private Comment[] CreateComments()
        {
            return new Comment[]
            {
                new Comment
                {
                    Id = 71,
                    Content = "Здравейте, Тази година входа за паркинга ще е зад сцената! ",
                    OwnerId = Guid.Parse(AdminId),
                    EventId = 1,
                    IsApproved = true
                },
                new Comment
                {
                    Id = 72,
                    Content = "Някой знае ли дали може да се плати вход само за първия ден?",
                    OwnerId = Guid.Parse(UserId),
                    EventId = 1,
                    IsApproved = true
				},
                new Comment
                {
	                Id = 73,
	                Content = "Миналата година можеше. Цената беше 10лв.",
	                OwnerId = Guid.Parse(AdminId),
	                EventId = 1,
	                IsApproved = true
				},
				new Comment
                {
                    Id = 74,
                    Content = "Този фестивал вече е добавен. Можете да премахнете това събитие.",
                    OwnerId = Guid.Parse(AdminId),
                    EventId = 2,
                    IsApproved = true
                },
				new Comment
				{
					Id = 75,
					Content = "Международен фолклорен фестивал „Витоша“ е неделима част от Културния календар на София. Провеждането му е уникална възможност за българската публика да се запознае с музикалната и танцова традиция на държави от цял свят. Това е шанс младата аудитория да види най-атрактивното лице на фолклора.",
					OwnerId = Guid.Parse(AdminId),
					EventId = 73,
					IsApproved = true
				}
			};
        }
    }
}
