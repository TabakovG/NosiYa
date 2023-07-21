using Microsoft.EntityFrameworkCore;
using NosiYa.Data;
using NosiYa.Services.Data.Interfaces;

namespace NosiYa.Services.Data
{
	public class UserService : IUserService
	{
		private readonly NosiYaDbContext context;

		public UserService(NosiYaDbContext _context)
		{
			this.context = _context;
		}
		public async Task<bool> UserExistByEmail(string email)
		{
			return await this.context
				.Users
				.AnyAsync(u => u.Email == email); //TODO user is active ?
		}

		public async Task<Guid> GetUserIdFromEmailAsync(string email)
		{
			return await this.context
				.Users
				.Where(u => u.Email == email)
				.Select(u => u.Id)
				.FirstAsync();

		}
	}
}
