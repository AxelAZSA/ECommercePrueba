using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entitys.Request
{
    public class Login
    {
        [Required]
        public string correo { get; set; }
        [Required]
        public string password { get; set; }
    }
}
