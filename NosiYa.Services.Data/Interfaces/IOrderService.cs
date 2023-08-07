using NosiYa.Web.ViewModels;

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
		Task<bool> IsApprovedByIdAsync(string orderId);
		Task<ICollection<OrderViewModel>> GetOrdersByUserIdAsync(string userId);
		Task<OrderDetailsViewModel> GetOrderDetailsByIdAsync(string orderId);
		Task<IEnumerable<ApprovalViewModel>> GetAllForApproval();

		//Update:

		Task ApproveByIdAsync(string orderId);


		//Delete:
		Task DeleteOrderAsync(string orderId);
		Task<bool> IsOwnedByTheUserAsync(string orderId, string userId);
		Task<bool> IsOnTimeAsync(string orderId);


	}
}
