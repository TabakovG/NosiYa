namespace NosiYa.Web.ViewModels.Region
{
	using System.ComponentModel.DataAnnotations;

	using static Common.EntityValidationConstants.Region;
	using Image;

	public class RegionFormModel
	{
		public RegionFormModel()
		{
			this.Images = new HashSet<ImageFormModel>();
		}

		[Required]
		[StringLength(NameMaxLength, MinimumLength = NameMinLength)]
		public string Name { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		public IEnumerable<ImageFormModel> Images { get; set; }
	}
}
