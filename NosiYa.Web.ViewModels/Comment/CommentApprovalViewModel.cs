namespace NosiYa.Web.ViewModels.Comment
{
	public class CommentApprovalViewModel
	{
		public string EventName { get; set; } = null!;
		public int EventId { get; set; }
		public string Content { get; set; } = null!;
		public string UserName { get; set; } = null!;
		public int CommentId { get; set; }
	}
}
