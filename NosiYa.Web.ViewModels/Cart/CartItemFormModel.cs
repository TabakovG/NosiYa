namespace NosiYa.Web.ViewModels.Cart
{
	using System.ComponentModel.DataAnnotations;

	public class CartItemFormModel
	{
		[Required]
		public int CartId { get; set; }

		[Required]
		public int OutfitSetId { get; set; }

		[Required]
		public DateTime FromDate { get; set; }

		[Required]
		public DateTime ToDate { get; set; }

	}
}
