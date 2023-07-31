namespace NosiYa.Web.ViewModels.Cart
{
	public class CartItemsViewModel
	{
		public int Id { get; set; }
		public int OutfitId { get; set; }

		public string Name { get; set; } = null!;

		public string SetImage { get; set; } = null!;

		public DateTime FromDate { get; set; }

		public DateTime ToDate { get; set; }
	}
}
