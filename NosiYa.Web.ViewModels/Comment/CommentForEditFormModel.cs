namespace NosiYa.Web.ViewModels.Comment
{
    using System.ComponentModel.DataAnnotations;
    using static NosiYa.Common.EntityValidationConstants.Comment;


    public class CommentForEditFormModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string ModifiedContent { get; set; } = null!;

        public int EventId { get; set; }
    }
}
