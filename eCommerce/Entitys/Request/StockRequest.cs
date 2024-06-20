using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entitys.Request
{
    public class stockRequest
    {
        [Required]
        public int idArticulo { get; set; }
        [Required]
        public int idTienda { get; set; }
        [Required]
        public int cantidad { get; set; }
    }
}
