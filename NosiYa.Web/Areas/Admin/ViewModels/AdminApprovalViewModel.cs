namespace NosiYa.Web.Areas.Admin.ViewModels
{
	using NosiYa.Web.ViewModels;
	public class AdminApprovalViewModel
	{
		public AdminApprovalViewModel()
		{
			this.Comments = new HashSet<ApprovalViewModel>();
			this.Events = new HashSet<ApprovalViewModel>();
			this.Rents = new HashSet<ApprovalViewModel>();
		}

		public IEnumerable<ApprovalViewModel> Rents { get; set; }
		public IEnumerable<ApprovalViewModel> Events { get; set; }
		public IEnumerable<ApprovalViewModel> Comments { get; set; }
	}
}
