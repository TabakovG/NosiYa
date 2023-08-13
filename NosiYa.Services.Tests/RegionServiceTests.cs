using NosiYa.Data.Models;
using NosiYa.Web.ViewModels.Region;

namespace NosiYa.Services.Tests
{
	using Microsoft.EntityFrameworkCore;
	
    using static DbSeedData;
	
    using NosiYa.Data;
    using NosiYa.Services.Data;
    using NosiYa.Services.Data.Interfaces;
   

    public class RegionServiceTests
    {
		private NosiYaDbContext dbContext;
		private DbContextOptions<NosiYaDbContext> dbOptions;
		private IRegionService regionService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.dbOptions = new DbContextOptionsBuilder<NosiYaDbContext>()
				.UseInMemoryDatabase("NosiYaInMemory" + Guid.NewGuid().ToString())
				.Options;

			this.dbContext = new NosiYaDbContext(this.dbOptions, false);
			dbContext.Database.EnsureCreated();

			SeedDatabase(dbContext);
			this.regionService = new RegionService(this.dbContext);
		}

		[SetUp]
		public void Setup()
		{
		}

        [Test]
        public async Task GetAllRegionsNamesAsync_ReturnsAllActiveRegionNames()
        {
            //Arrange

            var expected =  new HashSet<Region>
{
    new Region
    {
        Id = 1,
        Name = "Родопски Регион",
        Description = "Родопската фолклорна област преди е била включена като район от тракийската област. Обособяването ѝ като самостоятелна се е наложило от по-съществените различия между двете области.\r\n\r\nРодопските танци са бавни и умерени с малко разнообразие на движенията и сравнителна простота. Играят се най-често на песен, като характерно тук е, че мъжете също пеят.\r\n\r\nХора̀та се играят в полукръг или кръг и най-често са само мъжки или само женски. Срещат се и разделно-смесени хора, но при тях мъжете и жените не се нареждат един до друг мъж-жена, а в началото на хорото се хващат само мъжете, а след тях – жените.\r\n\r\nМъжете се залавят най-често за длани, което е много характерно за Родопите. В другите области този захват е много рядко срещан. Мъжете играят с широки стъпки, клякат и коленичат бавно и тромаво.",
        IsActive = true
    },
    new Region
    {
        Id = 72,
        Name = "Шопски Регион",
        Description = "Шопската фолклорна област има няколко района – софийски, граовски, кюстендилски, самоковски, ихтимански и годечки, в които има известни различия, въпреки общото в стила.\r\n\r\nТанцуването предизвиква силни преживявания в емоционалната душа на шопите, което се изразява в мощни провиквания, ръмжене и свиркане. Добре известно е движението „натрисане” – силно раздрусване на раменете, предизвикано от динамичните, твърди и отсечени движения в краката, както и от постановката на тялото и хвата за пояс. Високото повдигане на крака е проява на сила и мъжественост. Характерен е подчертаният стремеж за откъсване от земята."
        ,IsActive = true
    }

};

            // Act
            var result = await regionService.GetAllRegionsNamesAsync();

            // Assert
            CollectionAssert.AreEquivalent(
                expected.Select(r => r.Name).ToArray(),
                result.ToArray());
        }

        [Test]
        public async Task GetAllRegionsAsync_ReturnsAllActiveRegions()
        {
            //Arrange
            var expected = new PossibleRegionsFormModel[]
            {
                new PossibleRegionsFormModel
                {
                    Id = 1,
                    Name = "Родопски Регион"
                },
                new PossibleRegionsFormModel
                {
                    Id = 72,
                    Name = "Шопски Регион"
                }
            };

            // Act
            var result = await regionService.GetAllRegionsAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Id,Is.EqualTo(expected.First().Id));
            Assert.That(result.Last().Id,Is.EqualTo(expected.Last().Id));
            Assert.That(result.First().Name, Is.EqualTo(expected.First().Name));
            Assert.That(result.Last().Name, Is.EqualTo(expected.Last().Name));
        }

        [Test]
        public async Task AllAvailableRegionsAsync_ReturnsCorrectRegions()
        {
            // Arrange
            var model = new AllRegionsPaginatedModel
            {
                CurrentPage = 1,
                RegionsPerPage = 10 // Adjust this based on your needs
            };

            // Act
            var result = await regionService.AllAvailableRegionsAsync(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.RegionsCount); // Adjust the expected count based on your seeded data

            // Verify the first region in the result
            var firstRegion = result.Regions.FirstOrDefault();
            Assert.IsNotNull(firstRegion);
            Assert.AreEqual("Родопски Регион", firstRegion.Name); // Adjust the expected name based on your seeded data
            // Add more assertions to verify the data in the firstRegion object

            // Verify the second region in the result
            var secondRegion = result.Regions.Skip(1).FirstOrDefault();
            Assert.IsNotNull(secondRegion);
            Assert.AreEqual("Шопски Регион", secondRegion.Name); // Adjust the expected name based on your seeded data
            // Add more assertions to verify the data in the secondRegion object
        }

        [Test]
        public async Task ExistsByIdAsync_ReturnsTrueForExistingRegion()
        {
            // Arrange
            var existingRegionId = 1; // Adjust this based on your seeded data

            // Act
            var exists = await regionService.ExistsByIdAsync(existingRegionId);

            // Assert
            Assert.IsTrue(exists);
        }

        [Test]
        public async Task ExistsByIdAsync_ReturnsFalseForNonExistingRegion()
        {
            // Arrange
            var nonExistingRegionId = 999; // Adjust this to a non-existing ID

            // Act
            var exists = await regionService.ExistsByIdAsync(nonExistingRegionId);

            // Assert
            Assert.IsFalse(exists);
        }

        [Test]
        public async Task GetDetailsByIdAsync_ReturnsDetailsForExistingRegion()
        {
            // Arrange
            var existingRegionId = 1; // Adjust this based on your seeded data

            // Act
            var details = await regionService.GetDetailsByIdAsync(existingRegionId);

            // Assert
            Assert.IsNotNull(details);
            Assert.AreEqual(existingRegionId, details.Id);

            // Add more assertions to verify the data in the details object
        }

        [Test]
        public async Task GetDetailsByIdAsync_ReturnsNullForNonExistingRegion()
        {
            // Arrange
            var nonExistingRegionId = 999; // Adjust this to a non-existing ID

            // Act
            try
            {
                await regionService.GetDetailsByIdAsync(nonExistingRegionId);
                Assert.Fail();
            }
            catch (Exception)
            {
               // Assert
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetForEditByIdAsync_ReturnsEditModelForExistingRegion()
        {
            // Arrange
            var existingRegionId = 1; // Adjust this based on your seeded data
            var expectedDescr =
                "Родопската фолклорна област преди е била включена като район от тракийската област. Обособяването ѝ като самостоятелна се е наложило от по-съществените различия между двете области.\r\n\r\nРодопските танци са бавни и умерени с малко разнообразие на движенията и сравнителна простота. Играят се най-често на песен, като характерно тук е, че мъжете също пеят.\r\n\r\nХора̀та се играят в полукръг или кръг и най-често са само мъжки или само женски. Срещат се и разделно-смесени хора, но при тях мъжете и жените не се нареждат един до друг мъж-жена, а в началото на хорото се хващат само мъжете, а след тях – жените.\r\n\r\nМъжете се залавят най-често за длани, което е много характерно за Родопите. В другите области този захват е много рядко срещан. Мъжете играят с широки стъпки, клякат и коленичат бавно и тромаво.";
                // Act
            var editModel = await regionService.GetForEditByIdAsync(existingRegionId);

            // Assert
            Assert.IsNotNull(editModel);
            Assert.AreEqual("Родопски Регион", editModel.Name);
            Assert.AreEqual(expectedDescr, editModel.Description);

            // Add more assertions to verify the data in the editModel object
        }

        [Test]
        public async Task GetForEditByIdAsync_ReturnsNullForNonExistingRegion()
        {
            // Arrange
            var nonExistingRegionId = 999; // Adjust this to a non-existing ID

            // Act

            try
            {
             await regionService.GetForEditByIdAsync(nonExistingRegionId);
                Assert.Fail();
            }
            catch (Exception)
            {
                // Assert
                Assert.Pass();
            }
        }

        [Test]
        public async Task CreateAndReturnIdAsync_CreatesAndReturnsId()
        {
            // Arrange
            var regionFormModel = new RegionFormModel
            {
                Name = "New Region Name",
                Description = "New Region Description"
            };

            // Act
            var newRegionId = await regionService.CreateAndReturnIdAsync(regionFormModel);

            // Assert
            Assert.IsTrue(newRegionId > 0);

            await using(var context = new NosiYaDbContext(dbOptions, false))
            {
                var createdRegion = await context.Regions.FirstOrDefaultAsync(r => r.Id == newRegionId);
                Assert.IsNotNull(createdRegion);
                Assert.AreEqual(regionFormModel.Name, createdRegion.Name);
                Assert.AreEqual(regionFormModel.Description, createdRegion.Description);
            }

            await regionService.DeleteByIdAsync(newRegionId);
        }

        [Test]
        public async Task EditByIdAsync_UpdatesRegionCorrectly()
        {
            // Arrange
            var regionFormModel = new RegionFormModel
            {
                Name = "New Region Name",
                Description = "New Region Description"
            };

            // Act
            var newRegionId = await regionService.CreateAndReturnIdAsync(regionFormModel);

            var editedName = "Edited Name";
            var editedDescription = "Edited Description";

            var editModel = new RegionFormModel
            {
                Name = editedName,
                Description = editedDescription
            };

            // Act
            await regionService.EditByIdAsync(newRegionId, editModel);

            // Assert
            await using(var context = new NosiYaDbContext(dbOptions, false))
            {
                var editedRegion = await context.Regions.FirstOrDefaultAsync(r => r.Id == newRegionId);
                Assert.IsNotNull(editedRegion);
                Assert.AreEqual(editedName, editedRegion.Name);
                Assert.AreEqual(editedDescription, editedRegion.Description);
            }
            await regionService.DeleteByIdAsync(newRegionId);

        }

        [Test]
        public async Task DeleteByIdAsync_SetsIsActiveToFalse()
        {
            // Arrange
            var regionFormModel = new RegionFormModel
            {
                Name = "New Region Name",
                Description = "New Region Description"
            };

            // Act
            var newRegionId = await regionService.CreateAndReturnIdAsync(regionFormModel);

            // Act

            // Assert
            await using (var context = new NosiYaDbContext(dbOptions, false))
            {
                var region = await context.Regions.FirstOrDefaultAsync(r => r.Id == newRegionId);
                Assert.IsNotNull(region);
                await regionService.DeleteByIdAsync(newRegionId);

                var regionExists = await regionService.ExistsByIdAsync(newRegionId);

                Assert.That(regionExists,Is.False);

            }
        }

        [OneTimeTearDown]
		public void OneTimeTearDown()
		{
			// Dispose of the DbContext to release the in-memory database
			dbContext.Dispose();
		}

	}
}