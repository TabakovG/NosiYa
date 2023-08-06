using System.ComponentModel.DataAnnotations;
using NosiYa.Web.ViewModels.User.Enums;

namespace NosiYa.Web.ViewModels.User
{
	using static Common.ApplicationConstants;

	public class AllUsersQueryModel
	{
		public AllUsersQueryModel()
		{
			CurrentPage = DefaultFirstPage;
			UsersPerPage = DefaultUsersPerPage;
			this.Users = new HashSet<UserViewModel>();
		}

		[Display(Name = "Търси...:")]
		public string? SearchTerm { get; set; }

		[Display(Name = "Сортирай по:")]
		public UserSorting UserSorting { get; set; }

		public int CurrentPage { get; set; }

		[Display(Name = "Показвай по:")]
		public int UsersPerPage { get; set; }

		public int UsersCount { get; set; }

		public IEnumerable<UserViewModel> Users { get; set; }

	}
}

