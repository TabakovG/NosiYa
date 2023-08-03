namespace NosiYa.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;

	using NosiYa.Services.Data.Interfaces;
	using ViewModels.OutfitSet;
	using Microsoft.AspNetCore.Mvc;
	using Common;
	using NosiYa.Services.Data.Models;
	using static Common.NotificationMessagesConstants;
	using static Common.SeedingConstants;

    [Authorize(Roles = AdminRoleName)]
    public class OutfitSetController : BaseController
    {
		private readonly IOutfitSetService outfitService;
		private readonly IRegionService regionService;
		private readonly IOutfitPartService partService;
		private readonly IImageService imageService;
		private readonly IWebHostEnvironment webHostEnvironment;

		public OutfitSetController(IOutfitSetService outfitService, IRegionService regionService, IOutfitPartService partService, IImageService imageService, IWebHostEnvironment webHostEnvironment)
		{
			this.outfitService = outfitService;
			this.regionService = regionService;
			this.partService = partService;
			this.imageService = imageService;
			this.webHostEnvironment = webHostEnvironment;
		}
		[HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllOutfitsQueryModel queryModel)
		{
			AllOutfitsFilteredAndPagedServiceModel queryAndSorting = await this.outfitService.AllAvailableOutfitSetsAsync(queryModel);

			queryModel.Outfits = queryAndSorting.OutfitSets;
			queryModel.OutfitsCount = queryAndSorting.TotalOutfits;
			queryModel.Regions = await this.regionService.GetAllRegionsNamesAsync();

			return View(queryModel);
		}

		[HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var outfitSet = new OutfitSetFormModel
                {
                    Regions = await this.regionService.GetAllRegionsAsync()
                };

                return this.View(outfitSet);
            }
            catch (Exception)
            {
                return this.GeneralError();

            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(OutfitSetFormModel model, [FromForm] ICollection<IFormFile> elementImages)
        {
	        try
	        {
		        //TODO To check if the user is admin

		        bool regionExists = await this.regionService.ExistsByIdAsync(model.RegionId);
		        if (!regionExists)
		        {
			        this.ModelState.AddModelError(nameof(model.RegionId), "Selected region does not exist!");
		        }

		        if (!this.ModelState.IsValid)
		        {
			        model.Regions = await this.regionService.GetAllRegionsAsync();

			        return this.View(model);
		        }

		        try
		        {

			        int outfitSetId =
				        await this.outfitService.CreateOutfitSetAndReturnIdAsync(model);

			        //Add images to the event
			        if (elementImages.Any())
			        {
				        // Call Add from ImageController without redirecting
				        var imageController = new ImageController(imageService, webHostEnvironment);
				        imageController.ControllerContext = ControllerContext;

				        string entityType = EntityTypesConst.OutfitSet;

				        // Invoke AddImagesOnEntityCreate Action 
				        await imageController.AddImagesOnEntityCreateAsync(outfitSetId, entityType, elementImages);

			        }

			        this.TempData[SuccessMessage] = "Носията е създадена успешно!";
			        this.TempData[WarningMessage] = "Моля добавете поне една част на носията, преди активация!";


					return this.RedirectToAction("Add", "OutfitPart", new {Id = outfitSetId});
		        }
		        catch (Exception)
		        {
			        model.Regions = await this.regionService.GetAllRegionsAsync();

			        return this.View(model);
		        }
	        }
	        catch (Exception)
	        {
		        return this.GeneralError();

	        }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
	        var outfitSetExists = await this.outfitService.ExistByIdAsync(id);

	        if (!outfitSetExists)
	        {
		        this.TempData[ErrorMessage] = "Носия с този идентификатор не съществува!";

		        return this.RedirectToAction("All", "OutfitSet");
	        }

	        try
	        {
		        OutfitSetDetailsViewModel viewModel = await this.outfitService
			        .GetDetailsByIdAsync(id);
                viewModel.OutfitParts = await this.partService.GetAllPartsBySetIdAsync(id);
		        return View(viewModel);
	        }
	        catch (Exception)
	        {
		        return this.GeneralError();
	        }

		}

		[HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var outfitSetExists = await this.outfitService.ExistByIdAsync(id);

            if (!outfitSetExists)
            {
                this.TempData[ErrorMessage] = "Носия с този идентификатор не съществува!";

                return this.RedirectToAction("All", "OutfitSet");
            }

            try
            {
                OutfitSetFormModel formModel = await this.outfitService
                    .GetForEditByIdAsync(id);

                formModel.Regions = await this.regionService.GetAllRegionsAsync();

                //Populate related Images
                formModel.Images = await this.imageService.GetRelatedImagesAsync(id, EntityTypesConst.OutfitSet);

				return View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, OutfitSetFormModel model, [FromForm] ICollection<IFormFile> elementImages)
        {
	        if (!this.ModelState.IsValid)
	        {
		        model.Regions = await this.regionService.GetAllRegionsAsync();

		        return this.View(model);
	        }

			var outfitSetExists = await this.outfitService.ExistByIdAsync(id);

	        if (!outfitSetExists)
	        {
		        this.TempData[ErrorMessage] = "Носия с този идентификатор не съществува!";

		        return this.RedirectToAction("All", "OutfitSet");
	        }

	        try
	        {
		        await this.outfitService.EditByIdAsync(id, model);

		        //Add images to the outfitSet
		        if (elementImages.Count > 0)
		        {
			        // Call Add from ImageController without redirecting
			        var imageController = new ImageController(imageService, webHostEnvironment);
			        imageController.ControllerContext = ControllerContext;

			        string entityType = EntityTypesConst.OutfitSet;

			        // Invoke AddImagesOnEntityCreate Action 
			        await imageController.UploadImagesAsync(id, entityType, elementImages);

		        }

				this.TempData[SuccessMessage] = "Промените са запазени успешно!";
		        return this.RedirectToAction("Details", "OutfitSet", new { id });
			}
	        catch (Exception)
	        {
				this.ModelState.AddModelError(string.Empty,
					"Възникна грешка при обработване на заявката. Моля опитайте отново или се свържете с администратор!");
				model.Regions = await this.regionService.GetAllRegionsAsync();

				return this.View(model);
			}
        }

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool outfitSetExists = await this.outfitService
				.ExistByIdAsync(id);
			if (!outfitSetExists)
			{
				this.TempData[ErrorMessage] = "Носия с този идентификатор не съществува!";

				return this.RedirectToAction("All", "OutfitSet");
			}

			try
			{
				OutfitSetForDelete viewModel =
					await this.outfitService.GetOutfitSetForDeleteAsync(id);

				return this.View(viewModel);
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id, OutfitSetForDelete model)
		{
			bool outfitSetExists = await this.outfitService
				.ExistByIdAsync(id);
			if (!outfitSetExists)
			{
				this.TempData[ErrorMessage] = "Носия с този идентификатор не съществува!";

				return this.RedirectToAction("All", "OutfitSet");
			}

			try
			{
				await this.outfitService.DeleteByIdAsync(id);
				await this.partService.DeleteByOutfitSetIdAsync(id);

				//TODO delete pictures async

				this.TempData[WarningMessage] = "Носията беше изтрита успешно!";
				return this.RedirectToAction("All", "OutfitSet");
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
