using System.Collections;
using NosiYa.Data.Models;

namespace NosiYa.Services.Tests
{
	using Microsoft.EntityFrameworkCore;
	using static DbSeedData;

	using NosiYa.Data;
	using Data;
	using Data.Interfaces;
	using NosiYa.Web.ViewModels.Cart;
	using NosiYa.Data.Models.Outfit;

	public class CartServiceTests
	{
		private NosiYaDbContext dbContext;
		private DbContextOptions<NosiYaDbContext> dbOptions;
		private ICartService cartService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.dbOptions = new DbContextOptionsBuilder<NosiYaDbContext>()
				.UseInMemoryDatabase("NosiYaInMemory" + Guid.NewGuid().ToString())
				.Options;

			this.dbContext = new NosiYaDbContext(this.dbOptions);
			dbContext.Database.EnsureCreated();

			SeedDatabase(dbContext);
			this.cartService = new CartService(this.dbContext);
		}

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task CartItemExistsById_ShouldReturnTrueWhenIdExists()
		{
			// Arrange
			var itemId = 171;

			// Act
			var result = await cartService.CartItemExistsById(itemId);
			var a = dbContext.Carts.First();
			// Assert
			Assert.IsTrue(result);
		}

		[Test]
		public async Task CartItemExistsById_ShouldReturnFalseWhenIdDoesNotExist()
		{
			// Arrange
			var itemId = 999; // Assuming this id does not exist

			// Act
			var result = await cartService.CartItemExistsById(itemId);

			// Assert
			Assert.IsFalse(result);
		}


		[Test]
		public async Task CartItemExistsById_ShouldReturnFalseWhenIdExistButItemInactive()
		{
			// Arrange
			var itemId = 222; //exists but inactive

			// Act
			var result = await cartService.CartItemExistsById(itemId);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public async Task GetForEditByIdAsync_ShouldReturnCartItemFormModel()
		{
			// Arrange
			var itemId = 171;

			// Act
			var result = await cartService.GetForEditByIdAsync(itemId);

			// Assert
			Assert.IsNotNull(result);
			Assert.That(1, Is.EqualTo(result.CartId));
			Assert.AreEqual(1, result.OutfitSetId);
			Assert.AreEqual(DateTime.Parse("17-08-2023"), result.FromDate);
			Assert.AreEqual(DateTime.Parse("19-08-2023"), result.ToDate);
		}

		[Test]
		public async Task GetForEditByIdAsync_ShouldThrowExceptionForNonExistingItem()
		{
			// Arrange
			var itemId = 999; // Non-existing item ID

			// Act & Assert
			Assert.ThrowsAsync<InvalidOperationException>(async () =>
			{
				await cartService.GetForEditByIdAsync(itemId);
			});
		}

		[Test]
		public async Task GetForEditByIdAsync_ShouldThrowExceptionForValidIdButInactiveItem()
		{
			// Arrange
			var itemId = 222; //exists but inactive

			// Act & Assert
			Assert.ThrowsAsync<InvalidOperationException>(async () =>
			{
				await cartService.GetForEditByIdAsync(itemId);
			});
		}

		[Test]
		public async Task EditByIdAsync_ShouldUpdateItemDates()
		{
			// Arrange
			var cartItem = new OutfitForCart
			{
				Id = 1212,
				OutfitId = 2,
				FromDate = DateTime.Parse("14-08-2023"),
				ToDate = DateTime.Parse("17-08-2023"),
				CartId = 2,
				IsActive = true
			};
			await this.dbContext.OutfitsForCarts.AddAsync(cartItem);
			await this.dbContext.SaveChangesAsync();

			var itemId = 1212; // Existing item ID
			var newFromDate = DateTime.Parse("2023-08-20");
			var newToDate = DateTime.Parse("2023-08-25");

			var model = new CartItemFormModel
			{
				FromDate = newFromDate,
				ToDate = newToDate
			};

			// Act
			await cartService.EditByIdAsync(itemId, model);

			// Assert
			var updatedItem = await cartService.GetForEditByIdAsync(itemId);

			Assert.NotNull(updatedItem);
			Assert.AreEqual(newFromDate, updatedItem.FromDate);
			Assert.AreEqual(newToDate, updatedItem.ToDate);

		}

		[Test]
		public async Task EditByIdAsync_ShouldThrowExceptionForInvalidId()
		{
			// Arrange
			var invalidItemId = 999; // Non-existing item ID
			var newFromDate = DateTime.Parse("2023-08-20");
			var newToDate = DateTime.Parse("2023-08-25");

			var model = new CartItemFormModel
			{
				FromDate = newFromDate,
				ToDate = newToDate
			};

			// Act and Assert
			Assert.ThrowsAsync<InvalidOperationException>(
				async () => await cartService.EditByIdAsync(invalidItemId, model)
			);
		}

		[Test]
		public async Task EditByIdAsync_ShouldThrowExceptionForValidIdButInactive()
		{
			// Arrange
			var itemId = 222; //exists but inactive

			// Act & Assert
			Assert.ThrowsAsync<InvalidOperationException>(async () =>
			{
				await cartService.GetForEditByIdAsync(itemId);
			});
		}

		[Test]
		public async Task DeleteItemFromUserCartAsync_ShouldDeactivateItemForValidId()
		{
			// Arrange

			var cartItem = new OutfitForCart
			{
				Id = 2222,
				OutfitId = 2,
				FromDate = DateTime.Parse("14-08-2023"),
				ToDate = DateTime.Parse("17-08-2023"),
				CartId = 2,
				IsActive = true
			};
			await this.dbContext.OutfitsForCarts.AddAsync(cartItem);
			await this.dbContext.SaveChangesAsync();

			var validItemId = 2222; // Existing item ID

			// Act
			await cartService.DeleteItemFromUserCartAsync(validItemId);

			// Assert
			var deactivatedItem = await dbContext.OutfitsForCarts.FindAsync(validItemId);
			Assert.IsFalse(deactivatedItem.IsActive);
		}

		[Test]
		public async Task DeleteItemFromUserCartAsync_ShouldThrowExceptionForInvalidId()
		{
			// Arrange
			var invalidItemId = 999; // Non-existing item ID

			// Act and Assert
			Assert.ThrowsAsync<InvalidOperationException>(
				async () => await cartService.DeleteItemFromUserCartAsync(invalidItemId)
			);
		}

		[Test]
		public async Task DeleteItemFromUserCartAsync_ShouldThrowExceptionForValidIdButInactive()
		{
			// Arrange
			var itemId = 222; //exists but inactive

			// Act & Assert
			Assert.ThrowsAsync<InvalidOperationException>(async () =>
			{
				await cartService.GetForEditByIdAsync(itemId);
			});
		}

		[Test]
		public async Task GetAllItemsFromUserCartAsync_ShouldReturnItemsForValidUserId()
		{
			// Arrange
			var validUserId = "7C34FB52-0FDB-4CD7-027F-08DB822AA1B3"; // Existing user ID

			// Act
			var result = await cartService.GetAllItemsFromUserCartAsync(validUserId);

			// Assert
			Assert.NotNull(result);
			Assert.IsTrue(result.Count > 0);
			Assert.IsTrue(result.All(item => item.Id > 0));
		}

		[Test]
		public async Task GetCartIdByUserIdAsync_ShouldThrowExceptionForInvalidUserId()
		{
			// Arrange
			var invalidUserId = "INVALID_USER_ID"; // Replace with an invalid user ID

			// Act and Assert
			Assert.ThrowsAsync<InvalidOperationException>(async () =>
			{
				await cartService.GetCartIdByUserIdAsync(invalidUserId);
			});
		}

		[Test]
		public async Task CreateCartItemAsync_ShouldAddCartItemToDatabase()
		{
			// Arrange
			var model = new CartItemFormModel
			{
				OutfitSetId = 1, // valid outfit set ID
				FromDate = DateTime.Now.Date,
				ToDate = DateTime.Now.Date.AddDays(3),
				CartId = 1 //  a valid cart ID
			};

			// Act
			await cartService.CreateCartItemAsync(model);

			// Assert
			var addedItem = dbContext.OutfitsForCarts.FirstOrDefault(item =>
				item.OutfitId == model.OutfitSetId &&
				item.FromDate == model.FromDate &&
				item.ToDate == model.ToDate &&
				item.CartId == model.CartId);

			Assert.NotNull(addedItem);
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			// Dispose of the DbContext to release the in-memory database
			dbContext.Dispose();
		}

	}
}