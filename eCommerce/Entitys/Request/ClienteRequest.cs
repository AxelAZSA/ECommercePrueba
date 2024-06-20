using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entitys.Request
{
    public class ClienteRequest
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]
        public string direccion { get; set; }
        [Required]
        public string correo { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string confirmPassword { get; set; }
    }
}
