namespace NosiYa.Services.Data.Interfaces
{
	using Web.ViewModels.Cart;
	public interface ICartService
	{
		//Create:
		Task CreateCartItemAsync(CartItemFormModel model);
		Task CartOrderCompleteAsync(CartCompleteOrderFormModel model, string userId);


		//Read:
		Task<ICollection<CartItemsViewModel>> GetAllItemsFromUserCartAsync(string userId);
		Task<ICollection<ReservedItemsViewModel>> GetReservedItemsByUserIdAsync(string userId);
		Task<int> GetCartIdByUserIdAsync(string user);
		Task<bool> CartItemExistsById(int id);


        //Update:
        Task<CartItemFormModel> GetForEditByIdAsync(int id);
        Task EditByIdAsync(int id, CartItemFormModel model);


        //Delete:
        Task DeleteItemFromUserCartAsync(int id);
		Task DeleteCompletedOrderAsync(int id, DateTime date);

	}
}
