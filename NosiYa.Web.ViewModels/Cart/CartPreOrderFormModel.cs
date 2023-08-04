namespace NosiYa.Web.ViewModels.Cart
{
	using OutfitSet;

	public class CartPreOrderFormModel
	{
		public OutfitSetForRentViewModel OutfitModel { get; set; } = null!;
		public CartItemFormModel CardItemFormModel { get; set; } = null!;

	}
}
