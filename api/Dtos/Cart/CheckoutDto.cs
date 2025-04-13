using System.ComponentModel.DataAnnotations;

namespace DemoShop.Dtos.Cart
{
    public class CheckoutDto
    {
        public string PaymentMethod { get; set; } = string.Empty;
    }
}
