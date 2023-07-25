using NosiYa.Web.ViewModels.Image;

namespace NosiYa.Services.Data.Interfaces
{

	public interface IImageService
	{
		//Create:

		Task AddImageAsync(ImageFormModel model, Guid userId);

		//Read:

		Task<bool> ImageExistByIdAsync(int id);

		//Update:

		Task SetDefaultImageAsync(int relatedEntityId, string entity, int? imageId);

		//Delete:
		Task DeleteImageByIdAsync(int id);

	}
}
