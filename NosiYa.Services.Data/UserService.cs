using Microsoft.EntityFrameworkCore;
using NosiYa.Data;
using NosiYa.Services.Data.Interfaces;
using NosiYa.Web.ViewModels.User;

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
				.AnyAsync(u => u.Email == email);
		}

		public async Task<Guid> GetUserIdFromEmailAsync(string email)
		{
			return await this.context
				.Users
				.Where(u => u.Email == email)
				.Select(u => u.Id)
				.FirstAsync();

		}


		public async Task<IEnumerable<UserViewModel>> AllAsync()
		{
			var users = await this.context
				.Users
				.AsNoTracking()
				.Where(u=>u.Email != null)
				.Select(u => new UserViewModel
				{
					Id = u.Id.ToString(),
					Email = u.Email,
					UserName = u.UserName,
					PhoneNumber = u.PhoneNumber,
					PhoneConfirmed = u.PhoneNumberConfirmed
				})
				.ToListAsync();

			return users;
		}

        public async Task ConfirmPhoneAsync(string id)
        {
            var user = await this.context
                .Users
                .FindAsync(Guid.Parse(id));

			user!.PhoneNumberConfirmed = true;

            await this.context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
	        var user = await this.context
		        .Users
		        .FindAsync(Guid.Parse(id));

	        user.UserName = null;
			user.Email = null;
			user.PhoneNumber = null;
			user.NormalizedEmail = null;
			user.NormalizedUserName = null;

			await this.context.SaveChangesAsync();
        }
	}
}
