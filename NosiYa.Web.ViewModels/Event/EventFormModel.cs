namespace NosiYa.Web.ViewModels.Event
{
	using System.ComponentModel.DataAnnotations;
	
	using Image;
	using static Common.EntityValidationConstants.Event;

	public class EventFormModel
	{
		public EventFormModel()
		{
			this.Images = new HashSet<ImageFormModel>();
		}

		[Required]
		[StringLength(NameMaxLength, MinimumLength = NameMinLength)]
		public string Name { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Required]
		[StringLength(LocationMaxLength, MinimumLength = LocationMinLength)]
		public string Location { get; set; } = null!;

		[Required]
		public DateTime EventStartDate { get; set; } //TODO validation required in controller

		[Required]
		public DateTime EventEndDate { get; set; }

		public ICollection<ImageFormModel> Images { get; set; }

	}
}
