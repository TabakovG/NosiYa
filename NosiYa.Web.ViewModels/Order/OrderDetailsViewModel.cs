namespace NosiYa.Web.ViewModels.Order
{
    public class OrderDetailsViewModel : OrderViewModel
    {
        public string Username { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}