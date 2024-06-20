using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entitys.Request
{
    public class CompraRequest
    {
        [Required]
        public int idTienda { get; set; }
    }

    public class EstadoRequest
    {
        [Required]
        public int idCompra{ get; set; }
        [Required]
        public string estado { get; set; }
    }
}
