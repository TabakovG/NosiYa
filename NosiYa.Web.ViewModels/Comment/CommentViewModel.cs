namespace NosiYa.Web.ViewModels.Comment
{

	public class CommentViewModel
	{
		public int Id { get; set; }

		public string Content { get; set; } = null!;

		public string OwnerId { get; set; } = null!;
		public string OwnerEmail { get; set; } = null!;

		public DateTime CreatedOn { get; set; }

		public bool IsWaitingForReview { get; set; }


	}
}
