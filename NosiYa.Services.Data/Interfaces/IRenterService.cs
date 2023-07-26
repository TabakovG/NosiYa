namespace NosiYa.Services.Data.Interfaces
{
	using NosiYa.Web.ViewModels.Rent;

	public interface IRenterService
	{
		Task<ICollection<string>> GetReservedDates();

		Task<int> GetCartIdByUserIdAsync(string user);

		Task CreateRentRequestAsync(CartFormModel model);
	}
}
