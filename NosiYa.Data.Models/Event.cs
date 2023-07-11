namespace NosiYa.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static NosiYa.Common.EntityValidationConstants.Event;

    public  class Event
    {
        public Event()
        {
            this.Comments = new HashSet<Comment>();
            this.Images = new HashSet<Image>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(LocationMaxLength)]
        public string Location { get; set; } = null!;

        [Required]
        public bool IsApproved { get; set; } = false;

        public Guid OwnerId { get; set; }
        public ApplicationUser Owner { get; set; } = null!;

        [Required]
        public DateTime EventStartDate { get; set; }
        
        [Required]
        public DateTime EventEndDate { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
