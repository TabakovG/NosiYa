using NosiYa.Services.Data;
using NosiYa.Services.Data.Interfaces;

namespace NosiYa.Services.Tests
{
	using Microsoft.EntityFrameworkCore;
	using static DbSeedData;
	using NosiYa.Data;
	

	public class CalendarServiceTests
	{
		private NosiYaDbContext dbContext;
		private DbContextOptions<NosiYaDbContext> dbOptions;
		private ICalendarService calendarService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.dbOptions = new DbContextOptionsBuilder<NosiYaDbContext>()
				.UseInMemoryDatabase("NosiYaInMemory" + Guid.NewGuid().ToString())
				.Options;

			this.dbContext = new NosiYaDbContext(this.dbOptions);
			dbContext.Database.EnsureCreated();

			SeedDatabase(dbContext);
			this.calendarService = new CalendarService(this.dbContext);
		}

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public async Task GetReservedDatesForItemAsyncShouldReturnTwoOrdersWithCorrectDates()
		{
			//Arange
			var validId = 1;
			var expectedTitle = "Reserved";

			var expectedStartForFirstEvent = DateTime.Parse("27-09-2023");
			var expectedEndForFirstEvent = DateTime.Parse("27-09-2023").AddDays(1);

			var expectedStartForSecondEvent = DateTime.Parse("30-08-2023");
			var expectedEndForSecondEvent = DateTime.Parse("02-09-2023").AddDays(1);

			var backgroundExpected = true;

			var backgroundFirstEvent = "gray";
			var backgroundSecondEvent = "green";

			//Act
			var reservations = await
				this.calendarService.GetReservedDatesForItemAsync(DateTime.UtcNow, DateTime.UtcNow.AddMonths(3), validId);
			//Assert

			Assert.That(reservations.Count, Is.EqualTo(2));
			Assert.That(reservations.First().start, Is.EqualTo(expectedStartForFirstEvent.ToString("yyyy-MM-ddTHH:mm:ss")));
			Assert.That(reservations.Last().start, Is.EqualTo(expectedStartForSecondEvent.ToString("yyyy-MM-ddTHH:mm:ss")));
			Assert.That(reservations.First().end, Is.EqualTo(expectedEndForFirstEvent.ToString("yyyy-MM-ddTHH:mm:ss")));
			Assert.That(reservations.Last().end, Is.EqualTo(expectedEndForSecondEvent.ToString("yyyy-MM-ddTHH:mm:ss")));
			Assert.That(reservations.First().background, Is.EqualTo(backgroundExpected));
			Assert.That(reservations.Last().background, Is.EqualTo(backgroundExpected));
			Assert.That(reservations.First().backgroundColor, Is.EqualTo(backgroundFirstEvent));
			Assert.That(reservations.Last().backgroundColor, Is.EqualTo(backgroundSecondEvent));
		}

		[Test]
		public async Task GetReservedDatesForItemAsyncShouldReturnNoInactiveOrdersWithValidDates()
		{
			//Arrange
			var validId = 2;
			var start = DateTime.Parse("27-09-2023");
			var end = DateTime.Parse("29-09-2023");
			//Act
			//Act
			var reservations = await
				this.calendarService.GetReservedDatesForItemAsync(DateTime.UtcNow, DateTime.UtcNow.AddMonths(3), validId);

			//Assert
		}

		[Test]
		public async Task GetReservedDatesForItemAsync_ShouldReturnNoReservations()
		{
			// Arrange
			var setId = 3;
			var start = DateTime.Parse("2023-09-01");
			var end = DateTime.Parse("2023-09-05");

			// Act
			var result = await calendarService.GetReservedDatesForItemAsync(start, end, setId);

			// Assert
			Assert.IsEmpty(result);
		}


		[Test]
		public async Task GetAllRentsAsync_ShouldReturnReservationsInDateRange()
		{
			// Arrange
			var start = DateTime.Parse("2023-06-01");
			var end = DateTime.Parse("2024-09-01").AddDays(1);

			// Act
			var result = await calendarService.GetAllRentsAsync(start, end);

			// Assert
			Assert.That(result.Count, Is.EqualTo(4));
			Assert.IsTrue(result.All(r => DateTime.Parse(r.start) >= start && DateTime.Parse(r.start) <= end));
			Assert.IsTrue(result.All(r => DateTime.Parse(r.end) >= start && DateTime.Parse(r.end) <= end));
			Assert.IsTrue(result.All(r => r.backgroundColor == "green" || r.backgroundColor == "gray"));
			Assert.IsTrue(result.All(r => r.title != null));
		}

		[Test]
		public async Task GetAllRentsAsync_ShouldReturnNoReservations()
		{
			// Arrange
			var start = DateTime.Parse("2023-10-01");
			var end = DateTime.Parse("2023-11-01");

			// Act
			var result = await calendarService.GetAllRentsAsync(start, end);

			// Assert
			Assert.IsEmpty(result);
		}

		[Test]
		public async Task ValidateDatesIfItemIsReservedAsync_ShouldReturnTrueWhenDatesOverlap()
		{
			// Arrange
			var start = DateTime.Parse("2023-08-01");
			var end = DateTime.Parse("2023-08-02");
			var setId = 2;

			// Act
			var result = await calendarService.ValidateDatesIfItemIsReservedAsync(start, end, setId);

			// Assert
			Assert.IsTrue(result);
		}

		[Test]
		public async Task ValidateDatesIfItemIsReservedAsync_ShouldReturnFalseWhenNoOverlap()
		{
			// Arrange
			var start = DateTime.Parse("2023-08-10");
			var end = DateTime.Parse("2023-08-12");
			var setId = 1;

			// Act
			var result = await calendarService.ValidateDatesIfItemIsReservedAsync(start, end, setId);

			// Assert
			Assert.IsFalse(result);
		}

		[Test]
		public async Task ValidateDatesIfItemIsReservedAsync_ShouldReturnFalseWhenInvalidId()
		{
			// Arrange
			var start = DateTime.Parse("2023-08-10");
			var end = DateTime.Parse("2023-08-12");
			var setId = 13;

			// Act
			var result = await calendarService.ValidateDatesIfItemIsReservedAsync(start, end, setId);

			// Assert
			Assert.IsFalse(result);
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			// Dispose of the DbContext to release the in-memory database
			dbContext.Dispose();
		}

	}
}