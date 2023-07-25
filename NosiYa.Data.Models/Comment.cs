namespace NosiYa.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static NosiYa.Common.EntityValidationConstants.Comment;

    public class Comment
    {
	    public Comment()
	    {
		    this.IsActive = true;
		    this.IsApproved = false;
            this.ModifiedContent = null;
	    }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;

        [MaxLength(ContentMaxLength)]
        public string? ModifiedContent { get; set; }

        [Required]
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }
        public ApplicationUser Owner { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        [Required]
        public bool IsApproved { get; set; }

        [Required] 
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

    }
}
