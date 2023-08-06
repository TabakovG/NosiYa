namespace NosiYa.Services.Data.Interfaces
{
    using NosiYa.Web.ViewModels.Cart;
    using NosiYa.Web.ViewModels.Order;

    public interface IOrderService
	{
		//Create:
		Task CreateOrderAsync(CartCompleteOrderFormModel model, string userId);

		//Read:
		Task<bool> ExistsByIdAsync(string orderId);
		Task<ICollection<OrderViewModel>> GetOrdersByUserIdAsync(string userId);
		Task<OrderDetailsViewModel> GetOrderDetailsByIdAsync(string orderId);

		//Delete:
		Task DeleteOrderAsync(string orderId);

	}
}
