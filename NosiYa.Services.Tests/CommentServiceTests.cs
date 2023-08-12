using NosiYa.Data.Models;

namespace NosiYa.Services.Tests
{
	using Microsoft.EntityFrameworkCore;
	using static DbSeedData;

	using System.Net;

	using NosiYa.Data;
	using Data;
	using Data.Interfaces;
	using Web.ViewModels.Comment;

	public class CommentServiceTests
	{
		private NosiYaDbContext dbContext;
		private DbContextOptions<NosiYaDbContext> dbOptions;
		private ICommentService commentService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.dbOptions = new DbContextOptionsBuilder<NosiYaDbContext>()
				.UseInMemoryDatabase("NosiYaInMemory" + Guid.NewGuid().ToString())
				.Options;

            this.dbContext = new NosiYaDbContext(this.dbOptions, false);
			dbContext.Database.EnsureCreated();

			SeedDatabase(dbContext);
			this.commentService = new CommentService(this.dbContext);
		}

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task CreateCommentAndReturnIdAsync_ShouldAddCommentToDatabaseAndReturnId()
		{
			// Arrange
			var model = new CommentFormModel
			{
				Content = "Test Comment Content",
				EventId = 1 // a valid event ID
			};
			var userId = Guid.Parse("7C34FB52-0FDB-4CD7-027F-08DB822AA1B3"); // a valid user ID

			// Act
			var commentId = await commentService.CreateCommentAndReturnIdAsync(model, userId);

			// Assert
			var addedComment = dbContext.Comments.FirstOrDefault(c => c.Id == commentId);

			Assert.NotNull(addedComment);
			Assert.AreEqual(WebUtility.HtmlEncode(model.Content), addedComment.Content);
			Assert.AreEqual(WebUtility.HtmlEncode(model.Content), addedComment.ModifiedContent);
			Assert.AreEqual(userId, addedComment.OwnerId);
			Assert.AreEqual(model.EventId, addedComment.EventId);
			Assert.IsTrue(addedComment.IsActive);
		}


		[Test]
		public async Task GetVisibleCommentsByEventAndUserIdAsync_ShouldReturnVisibleCommentsForEventAndUser()
		{
			// Arrange
			var eventId = 77; // Replace with a valid event ID
			var userId = "7C34FB52-0FDB-4CD7-027F-08DB822AA1B3";

			// Act
			var result = await commentService.GetVisibleCommentsByEventAndUserIdAsync(eventId, userId);

			// Assert
			Assert.NotNull(result);
			Assert.AreEqual(3, result.Count()); // 2 active + 1 unapproved but owned

			var visibleCommentViewModel1 = result.FirstOrDefault(c => c.Id == 77);
			Assert.NotNull(visibleCommentViewModel1);
			Assert.AreEqual("Visible Comment 1", visibleCommentViewModel1.Content);
			Assert.AreEqual(userId, visibleCommentViewModel1.OwnerId);
			Assert.AreEqual("admin@example.com", visibleCommentViewModel1.OwnerEmail);
			Assert.IsFalse(visibleCommentViewModel1.IsWaitingForReview);

			var visibleCommentViewModel2 = result.FirstOrDefault(c => c.Id == 78);
			Assert.NotNull(visibleCommentViewModel2);
			Assert.AreEqual("Visible Comment 2", visibleCommentViewModel2.Content);
			Assert.AreEqual(userId, visibleCommentViewModel2.OwnerId);
			Assert.AreEqual("admin@example.com", visibleCommentViewModel2.OwnerEmail);
			Assert.IsTrue(visibleCommentViewModel2.IsWaitingForReview);
		}



		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			// Dispose of the DbContext to release the in-memory database
			dbContext.Dispose();
		}

	}
}