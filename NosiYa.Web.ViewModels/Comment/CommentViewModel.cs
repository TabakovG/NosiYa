namespace NosiYa.Web.ViewModels.Comment
{
	using Data.Models;

	public class CommentViewModel
	{
		public int Id { get; set; }

		public string Content { get; set; } = null!;

		public string OwnerId { get; set; }
		public ApplicationUser Owner { get; set; } = null!; //TODO to be replaced by stringOwnerId

		public DateTime CreatedOn { get; set; }


	}
}
