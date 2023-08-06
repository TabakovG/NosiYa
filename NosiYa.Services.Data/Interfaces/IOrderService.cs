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
		Task<bool> ApprovedByIdAsync(int id);
		Task<ICollection<OrderViewModel>> GetOrdersByUserIdAsync(string userId);
		Task<OrderDetailsViewModel> GetOrderDetailsByIdAsync(string orderId);
		Task<IEnumerable<ApprovalViewModel>> GetAllForApproval();

		//Delete:
		Task DeleteOrderAsync(string orderId);
		Task<bool> IsOwnedByTheUserAsync(string orderId, string userId);
		Task<bool> IsOnTimeAsync(string orderId);


	}
}
