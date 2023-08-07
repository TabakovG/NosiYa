namespace NosiYa.Web.Areas.Admin.ViewModels
{
	using NosiYa.Web.ViewModels.Comment;
	using NosiYa.Web.ViewModels.Event;
	using NosiYa.Web.ViewModels.Order;


	public class AdminApprovalViewModel
	{
		public AdminApprovalViewModel()
		{
			this.Comments = new HashSet<CommentApprovalViewModel>();
			this.Events = new HashSet<EventApprovalViewModel>();
			this.Orders = new HashSet<OrderApprovalViewModel>();
		}

		public IEnumerable<OrderApprovalViewModel> Orders { get; set; }
		public IEnumerable<EventApprovalViewModel> Events { get; set; }
		public IEnumerable<CommentApprovalViewModel> Comments { get; set; }
	}
}
