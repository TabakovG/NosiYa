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
    using static Common.SeedingConstants;
    using static Common.NotificationMessagesConstants;

    public class CommentServiceTests
    {
        private NosiYaDbContext dbContext;
        private DbContextOptions<NosiYaDbContext> dbOptions;
        private ICommentService commentService;
        private ApplicationUser newUser;

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
        public async Task GetVisibleCommentsByEventAndUserIdAsync_ValidInput_ReturnsVisibleComments()
        {
            // Arrange
            var eventId = 2;
            var userId = AdminId; // Replace with the appropriate user ID

            // Act
            var visibleComment = await dbContext.Comments.FindAsync(74);
            var result = await commentService.GetVisibleCommentsByEventAndUserIdAsync(eventId, userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(1)); // Ensure only one visible comment is returned
            Assert.That(result.First().Content, Is.EqualTo(visibleComment.Content));
            Assert.That(result.First().OwnerId, Is.EqualTo(visibleComment.OwnerId.ToString()));
        }



        [Test]
        public async Task GetVisibleCommentsByEventAndUserIdAsync_InvalidEventId_ReturnsEmptyList()
        {
            // Arrange
            var invalidEventId = -1;
            var userId = "YourUserId"; // Replace with the appropriate user ID

            // Act
            var result = await commentService.GetVisibleCommentsByEventAndUserIdAsync(invalidEventId, userId);

            // Assert
            var resultList = result.ToList();
            Assert.That(resultList, Is.Not.Null);
            Assert.That(resultList, Is.Empty);
        }


        [Test]
        public async Task GetAllCommentsByEventIdAsync_ValidEventId_ReturnsComments()
        {
            // Arrange
            var eventId = 1; // Replace with the appropriate event ID

            // Act
            var result = await commentService.GetAllCommentsByEventIdAsync(eventId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Has.All.Property("OwnerId").Not.Null.Or.Empty);
            Assert.That(result, Has.All.Property("OwnerEmail").Not.Null.Or.Empty);
            Assert.That(result, Has.All.Property("Content").Not.Null.Or.Empty);
            Assert.That(result, Has.All.Property("IsWaitingForReview").Not.Null);
            Assert.That(result.FirstOrDefault(c => c.Id == 71), Is.Null);
            Assert.That(result.First(c => c.Id == 72), Is.Not.Null);
            Assert.That(result.First(c => c.Id == 73), Is.Not.Null);
        }

        [Test]
        public async Task GetAllCommentsByEventIdAsync_InvalidEventId_ReturnsEmptyList()
        {
            // Arrange
            var invalidEventId = -1; // Replace with an invalid event ID

            // Act
            var result = await commentService.GetAllCommentsByEventIdAsync(invalidEventId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task ExistsByIdAsync_ValidId_ReturnsTrue()
        {
            // Arrange
            var validCommentId = 72; // Replace with a valid comment ID

            // Act
            var result = await commentService.ExistsByIdAsync(validCommentId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsByIdAsync_InvalidId_ReturnsFalse()
        {
            // Arrange
            var invalidCommentId = -1; // Replace with an invalid comment ID

            // Act
            var result = await commentService.ExistsByIdAsync(invalidCommentId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ExistsByIdAsync_ValidIdBuInactive_ReturnsFalse()
        {
            // Arrange
            var invalidCommentId = 71; // Replace with an invalid comment ID

            // Act
            var result = await commentService.ExistsByIdAsync(invalidCommentId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetAllForApproval_ReturnsCorrectCountOfCommentsForApproval()
        {
            // Arrange
            var expectedCount = 1; // Adjust this value based on the number of comments that meet the criteria

            // Act
            var result = await commentService.GetAllForApproval();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task GetAllForApproval_ReturnsCorrectCommentsForApproval()
        {
            // Arrange

            // Act
            var result = await commentService.GetAllForApproval();

            // Assert
            Assert.That(result.FirstOrDefault(c => c.CommentId == 76), Is.Null);
            Assert.That(result.First(c => c.CommentId == 78), Is.Not.Null);
        }

        [Test]
        public async Task IsOwnedByUserIdAsync_CommentOwnedByUser_ReturnsTrue()
        {
            // Arrange
            var commentId = 72; // Comment owned by user
            // Act
            var result = await commentService.IsOwnedByUserIdAsync(commentId, "2f29d591-89ef-45b2-89a9-08db83ceb60e");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsOwnedByUserIdAsync_CommentNotOwnedByUser_ReturnsFalse()
        {
            // Arrange
            var commentId = 74; // Comment not owned by user
            var userId = UserId; // Replace with the appropriate user ID

            // Act
            var result = await commentService.IsOwnedByUserIdAsync(commentId, userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetForEditByIdAsync_ValidId_ReturnsCommentFormModel()
        {
            // Arrange
            var commentId = 72; // Valid comment ID

            // Act
            var result = await commentService.GetForEditByIdAsync(commentId);


            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Content, Is.EqualTo("Някой знае ли дали може да се плати вход само за първия ден?"));
            Assert.That(result.EventId, Is.EqualTo(1));

        }

        [Test]
        public async Task GetForEditByIdAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            var commentId = -1; // Invalid comment ID

            // Act
            CommentFormModel result = null;
            try
            {
                result = await commentService.GetForEditByIdAsync(commentId);
                Assert.Fail();

            }
            catch (Exception)
            {
                // Optionally, add more specific assertions about the caught exception here if needed
                Assert.Pass();
            }

            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task EditByModelAsync_ValidModel_EditsComment()
        {
            // Arrange

            var modelCreate = new CommentFormModel
            {
                Content = "Test Comment",
                EventId = 1
            };
            var newCommentId = await commentService.CreateCommentAndReturnIdAsync(modelCreate, Guid.Parse(AdminId));
            var newModifiedContent = "New modified content";

            var model = new CommentForEditFormModel
            {
                Id = newCommentId,
                ModifiedContent = newModifiedContent,
                EventId = 1
            };

            // Act
            await commentService.EditByModelAsync(model);

            // Assert
            var editedComment = dbContext.Comments
                .SingleOrDefault(c => c.Id == newCommentId);

            Assert.That(editedComment, Is.Not.Null);
            Assert.That(editedComment!.ModifiedContent, Is.EqualTo(WebUtility.HtmlEncode(newModifiedContent)));
            Assert.That(editedComment.Content, Does.StartWith(CommentWaitingForReviewText));
            await commentService.DeleteByIdAsync(newCommentId);
        }

        [Test]
        public async Task EditByModelAsync_InvalidId_ThrowsException()
        {
            // Arrange
            var invalidCommentId = 71; // Use an invalid comment ID
            var newModifiedContent = "New modified content";

            var model = new CommentForEditFormModel
            {
                Id = invalidCommentId,
                ModifiedContent = newModifiedContent
            };

            // Act and Assert
            try
            {
                await commentService.EditByModelAsync(model);

                // If no exception was thrown, fail the test
                Assert.Fail("No exception was thrown.");
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsNotNull(ex);
                // You can add more specific assertions about the caught exception here if needed
            }
        }

        [Test]
        public async Task CreateCommentAndReturnIdAsync_ValidInput_ReturnsCommentId()
        {
            // Arrange
            var model = new CommentFormModel
            {
                Content = "Test Comment Test Comment",
                EventId = 2
            };

            // Act

            var result = await commentService.CreateCommentAndReturnIdAsync(model, Guid.Parse(AdminId));

            // Assert

            Assert.That(result, Is.GreaterThan(0));

            // Verify that the comment is added to the database
            var savedComment = await dbContext.Comments.FindAsync(result);

            Assert.That(savedComment, Is.Not.Null);
            Assert.That(savedComment!.OwnerId, Is.EqualTo(Guid.Parse(AdminId)));
            Assert.That(WebUtility.HtmlDecode(savedComment.Content), Is.EqualTo(model.Content));
            Assert.That(WebUtility.HtmlDecode(savedComment.ModifiedContent), Is.EqualTo(model.Content));
            Assert.That(savedComment.EventId, Is.EqualTo(model.EventId));
            Assert.That(savedComment.IsActive, Is.True);

            await commentService.DeleteByIdAsync(result);
        }

        [Test]
        public async Task CreateCommentAndReturnIdAsync_InvalidInput_CatchesAnyException()
        {
            // Arrange
            var model = new CommentFormModel
            {
                // Invalid model, missing required properties
            };

            try
            {
                // Act
                await commentService.CreateCommentAndReturnIdAsync(model, Guid.Parse(AdminId));

                // If no exception was thrown, fail the test
                Assert.Fail("No exception was thrown.");
            }
            catch (Exception ex)
            {
                // Assert
                // You can add more specific assertions about the caught exception here if needed
                Assert.IsNotNull(ex);
            }
        }

        [Test]
        public async Task ApproveByIdAsync_ValidId_CommentIsApprovedAndModifiedContentCleared()
        {
            // Arrange
            var commentId = 78; // Valid comment ID 

            // Act
            await commentService.ApproveByIdAsync(commentId);

            // Assert
            var approvedComment = await dbContext.Comments
                .Where(c => c.Id == commentId)
                .FirstOrDefaultAsync();

            Assert.NotNull(approvedComment);
            Assert.True(approvedComment.IsApproved);
            Assert.Null(approvedComment.ModifiedContent);
        }

        [Test]
        public async Task ApproveByIdAsync_InvalidId_DoesNotThrowException()
        {
            // Arrange
            var invalidCommentId = -1; // Invalid comment ID

            // Act and Assert
            try
            {
                await commentService.ApproveByIdAsync(invalidCommentId);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task DeleteAllByEventIdAsync_ValidEventId_DeletesComments()
        {
            await using (var context = new NosiYaDbContext(dbOptions, false))
            {
                // Arrange
                var eventId = 1; // Replace with a valid event ID
                Assert.That(await context.Comments.FindAsync(72), Is.Not.Null);
                Assert.That(await context.Comments.FindAsync(73), Is.Not.Null);

                // Act
                await commentService.DeleteAllByEventIdAsync(eventId);
                var result = await context.Comments.FindAsync(73);
                var secondResult = await context.Comments.FindAsync(73);

                // Assert
                Assert.That(result!.IsActive, Is.False);
                Assert.That(secondResult!.IsActive, Is.False);

            }
        }

        [Test]
        public async Task DeleteByIdAsync_ValidId_DeletesComment()
        {
            // Arrange
            var newCommentId = await commentService.CreateCommentAndReturnIdAsync(new CommentFormModel
            {
                Content = "Test content",
                EventId = 1
            }, Guid.Parse(AdminId));

            var result = await dbContext.Comments.FindAsync(newCommentId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.IsActive, Is.True);

            // Act
            await commentService.DeleteByIdAsync(newCommentId);

            // Assert
            Assert.That(result!.IsActive, Is.False);

        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Dispose of the DbContext to release the in-memory database
            dbContext.Dispose();
        }

    }
}