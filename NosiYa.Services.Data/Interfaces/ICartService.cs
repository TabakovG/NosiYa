using NosiYa.Services.Data.Models;

namespace NosiYa.Services.Data.Interfaces
{
	using Web.ViewModels.Cart;
	public interface ICartService
	{
		Task<ICollection<ReservationsServiceModel>> GetReservedDates(DateTime start, DateTime end);

		Task<int> GetCartIdByUserIdAsync(string user);

		Task CreateCartItemAsync(CartFormModel model);
	}
}
