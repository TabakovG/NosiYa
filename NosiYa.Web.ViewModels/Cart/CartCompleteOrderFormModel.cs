namespace NosiYa.Web.ViewModels.Cart
{
	using System.ComponentModel.DataAnnotations;

	public class CartCompleteOrderFormModel
	{

		public int OutfitId { get; set; }

		[Required]
		public DateTime FromDate { get; set; }

		[Required]
		public DateTime ToDate { get; set; }
	}
}
