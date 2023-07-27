namespace NosiYa.Web.ViewModels.Cart
{
	using System.ComponentModel.DataAnnotations;

	using OutfitSet;

	public class CartPreOrderFormModel
	{

		[Required]
		public int CartId { get; set; }

		public OutfitSetForRentViewModel OutfitModel { get; set; } = null!;

		[Required]
		public DateTime FromDate { get; set; }

		[Required]
		public DateTime ToDate { get; set; }

	}
}
