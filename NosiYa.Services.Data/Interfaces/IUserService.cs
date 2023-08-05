namespace NosiYa.Services.Data.Interfaces
{
	using Web.ViewModels.User;

	public interface IUserService
	{
		Task<bool> UserExistByEmail(string email);
		Task<Guid> GetUserIdFromEmailAsync(string email);

		Task<IEnumerable<UserViewModel>> AllAsync();

        Task ConfirmPhoneAsync(string id);

    }
}
