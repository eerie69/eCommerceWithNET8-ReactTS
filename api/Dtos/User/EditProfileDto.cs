using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DemoShop.Dtos.User
{
    public class EditProfileDto
    {
        [Required, DisplayName("User Image")]
        public IFormFile? Image { get; set; }
    }
}
