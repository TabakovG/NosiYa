namespace NosiYa.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static NosiYa.Common.EntityValidationConstants.Image;

    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string Url { get; set; } = null!;

    }
}
