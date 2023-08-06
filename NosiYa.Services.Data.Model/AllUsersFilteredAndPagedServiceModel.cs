namespace NosiYa.Services.Data.Models
{
	using Web.ViewModels.User;
	using Web.ViewModels.User.Enums;


	public class AllUsersFilteredAndPagedServiceModel
	{
		public AllUsersFilteredAndPagedServiceModel()
		{
			this.Users = new HashSet<UserViewModel>();
		}
		public int TotalUsers { get; set; }

		public IEnumerable<UserViewModel> Users { get; set; }
	}
}
