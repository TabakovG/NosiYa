namespace NosiYa.Web.ViewModels.Comment
{
	using System.ComponentModel.DataAnnotations;

	using static NosiYa.Common.EntityValidationConstants.Comment;

	public class CommentFormModel
	{
		[Required]
		[StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
		public string Content { get; set; } = null!;

		public int EventId { get; set; }
	}
}
