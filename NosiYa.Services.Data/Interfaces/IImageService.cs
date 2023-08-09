using NosiYa.Web.ViewModels.Image;

namespace NosiYa.Services.Data.Interfaces
{

	public interface IImageService
	{
		//Create:

		Task<int> AddImageAndReturnIdAsync(ImageFormModel model, Guid userId);

		//Read:
        Task<ICollection<ImageViewModel>> GetRelatedImagesAsync(int relatedEntityId, string entity);
        Task<bool> HasDefaultAsync(int relatedEntityId, string entity);
		Task<bool> ImageExistByIdAsync(int id);

		//Update:

		Task SetDefaultImageAsync(int relatedEntityId, string entity, int? imageId = null);

		//Delete:
		Task DeleteImageByIdAsync(int id, string root);


	}
}
