using NosiYa.Services.Data.Models;

namespace NosiYa.Services.Data.Interfaces
{
	using Web.ViewModels.Cart;
	public interface ICartService
	{
		//Create:
		Task CreateCartItemAsync(CartPreOrderFormModel model);
		Task CartOrderCompleteAsync(CartCompleteOrderFormModel model, string userId);


		//Read:
		Task<ICollection<CartPreOrderViewModel>> GetAllItemsFromUserCartAsync(string userId);
		Task<int> GetCartIdByUserIdAsync(string user);
		Task<bool> CartItemExistsById(int id);

		//Update:


		//Delete:
		Task DeleteItemFromUserCartAsync(int id);
		Task DeleteCompletedOrderAsync(int id, DateTime date);

		//TODO:

		Task<ICollection<ReservationsServiceModel>> GetReservedDates(DateTime start, DateTime end); //TODO move to calendar controller!!


	}
}
