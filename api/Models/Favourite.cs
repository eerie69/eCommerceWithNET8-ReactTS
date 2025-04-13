using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoShop.Models
{
    [Table("Favourites")]
    public class Favourite
    {
        public string AppUserId { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
