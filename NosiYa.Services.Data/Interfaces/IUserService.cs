using NosiYa.Services.Data.Models;

namespace NosiYa.Services.Data.Interfaces
{
	using Web.ViewModels.User;

	public interface IUserService
	{
		Task<bool> UserExistByEmail(string email);
		Task<Guid> GetUserIdFromEmailAsync(string email);

		Task<AllUsersFilteredAndPagedServiceModel> AllAsync(AllUsersQueryModel queryModel);

        Task ConfirmPhoneAsync(string id);

        Task DeleteByIdAsync(string id);
	}
}
