namespace NosiYa.Services.Data.Interfaces
{
	using NosiYa.Web.ViewModels.Cart;

	public interface IOrderService
	{
		//Create:
		Task CreateOrderAsync(CartCompleteOrderFormModel model, string userId);

		//Read:
		Task<bool> OrderExistsById(string orderId);
		Task<ICollection<ReservedItemsViewModel>> GetOrdersByUserIdAsync(string userId);

		//Delete:
		Task DeleteOrderAsync(string orderId);

	}
}
