using NosiYa.Data.Models.Enums;
using NosiYa.Services.Data.Model;

namespace NosiYa.Web.Controllers
{
	using NosiYa.Services.Data.Interfaces;
	using ViewModels.OutfitSet;
	using Microsoft.AspNetCore.Mvc;
	using NosiYa.Web.Infrastructure.Extensions;

	public class OutfitSetController : Controller
	{
		private readonly IOutfitSetService outfitService;
		private readonly IRegionService regionService;
		private readonly IOutfitPartService partService;

		public OutfitSetController(IOutfitSetService outfitService, IRegionService regionService, IOutfitPartService partService)
		{
			this.outfitService = outfitService;
			this.regionService = regionService;
			this.partService = partService;
		}
		[HttpGet]
		public async Task<IActionResult> All([FromQuery] AllOutfitsQueryModel queryModel)
		{
			var queryAndSorting = await this.outfitService.AllAvailableOutfitSetsAsync(queryModel);

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
        public async Task<IActionResult> Create(OutfitSetFormModel model)
        {
	        try
	        {
		        //TODO To check if the user is admin

		        bool regionExists = await this.regionService.RegionExistsByIdAsync(model.RegionId);
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

/*			        CreateSetToAddPartServiceModel input = new CreateSetToAddPartServiceModel
			        {
				        OutfitSetId = outfitSetId,
			        };*/

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
        public async Task<IActionResult> Details(int id)
        {
	        var outfitSetExists = await this.outfitService.ExistByIdAsync(id);

	        if (!outfitSetExists)
	        {
		        this.TempData["ErrorMessage"] = "Носия с този идентификатор не съществува!";

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
                this.TempData["ErrorMessage"] = "Носия с този идентификатор не съществува!";

                return this.RedirectToAction("All", "OutfitSet");
            }

            try
            {
                OutfitSetFormModel viewModel = await this.outfitService
                    .GetForEditByIdAsync(id);

                viewModel.Regions = await this.regionService.GetAllRegionsAsync();

				return View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, OutfitSetFormModel model)
        {
	        if (!this.ModelState.IsValid)
	        {
		        model.Regions = await this.regionService.GetAllRegionsAsync();

		        return this.View(model);
	        }

			var outfitSetExists = await this.outfitService.ExistByIdAsync(id);

	        if (!outfitSetExists)
	        {
		        this.TempData["ErrorMessage"] = "Носия с този идентификатор не съществува!";

		        return this.RedirectToAction("All", "OutfitSet");
	        }

	        try
	        {
		        await this.outfitService.EditByIdAsync(id, model);
		        this.TempData["SuccessMessage"] = "Промените са запазени успешно!";
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
				this.TempData["ErrorMessage"] = "Носия с този идентификатор не съществува!";

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
				this.TempData["ErrorMessage"] = "Носия с този идентификатор не съществува!";

				return this.RedirectToAction("All", "OutfitSet");
			}

			try
			{
				await this.outfitService.DeleteByIdAsync(id);

				this.TempData["WarningMessage"] = "Носията беше изтрита успешно!";
				return this.RedirectToAction("All", "OutfitSet");
			}
			catch (Exception)
			{
				return this.GeneralError();
			}
		}

		private IActionResult GeneralError()
        {
            this.TempData["ErrorMessage"] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return this.RedirectToAction("Index", "Home");
        }
    }
}
