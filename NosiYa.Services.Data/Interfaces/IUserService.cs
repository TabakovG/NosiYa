namespace NosiYa.Services.Data.Interfaces
{
	public interface IUserService
	{
		Task<bool> UserExistByEmail(string email);
		Task<Guid> GetUserIdFromEmailAsync(string email);
	}
}
