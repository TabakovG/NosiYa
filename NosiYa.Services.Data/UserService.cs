namespace NosiYa.Services.Data
{
	using Microsoft.EntityFrameworkCore;

    using Models;
	using NosiYa.Data;
    using NosiYa.Data.Models;
	using Interfaces;
	using Web.ViewModels.User;
    using Web.ViewModels.User.Enums;

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


		public async Task<AllUsersFilteredAndPagedServiceModel> AllAsync(AllUsersQueryModel queryModel)
		{

			IQueryable<ApplicationUser> usersQuery = this.context
				.Users
				.Where(u=>u.Email != null)
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
			{
				string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";

				usersQuery = usersQuery
					.Where(u => EF.Functions.Like(u.Email, wildCard) ||
								EF.Functions.Like(u.PhoneNumber ?? string.Empty, wildCard) ||  
								EF.Functions.Like(u.UserName, wildCard));
			}

			usersQuery = queryModel.UserSorting switch
			{
				UserSorting.NameAscending => usersQuery
					.OrderBy(o => o.UserName),
				UserSorting.NameDescending => usersQuery
					.OrderByDescending(o => o.UserName),
				UserSorting.PhoneConfirmed => usersQuery
					.OrderByDescending(o => o.PhoneNumberConfirmed),
				UserSorting.EmailAscending => usersQuery
					.OrderBy(o => o.Email),
				UserSorting.EmailDescending => usersQuery
					.OrderByDescending(o => o.Email),
				_ => usersQuery
					.OrderBy(o => o.Email)
			};

			IEnumerable<UserViewModel> allUsers = await usersQuery
				.Skip((queryModel.CurrentPage - 1) * queryModel.UsersPerPage)
				.Take(queryModel.UsersPerPage)
				.Select(u => new UserViewModel
				{
					Id = u.Id.ToString(),
					Email = u.Email,
					UserName = u.UserName,
					PhoneNumber = u.PhoneNumber,
					PhoneConfirmed = u.PhoneNumberConfirmed
				})
				.ToArrayAsync();

			int usersCount = usersQuery.Count();

			return new AllUsersFilteredAndPagedServiceModel
			{
				TotalUsers = usersCount,
				Users = allUsers
			};
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

	        user!.UserName = null;
			user.Email = null;
			user.PhoneNumber = null;
			user.NormalizedEmail = null;
			user.NormalizedUserName = null;

			await this.context.SaveChangesAsync();
        }
	}
}
