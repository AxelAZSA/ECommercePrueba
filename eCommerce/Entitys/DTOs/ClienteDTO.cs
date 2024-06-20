using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entitys.DTOs
{
    public class ClienteDTO
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]
        public string direccion { get; set; }
        [Required]
        public string correo { get; set; }
    }
}
