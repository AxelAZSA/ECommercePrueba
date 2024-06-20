using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Entitys
{
    public class Compra
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int idCliente { get; set; }
        [Required]
        public int idTienda { get; set; }
        [Required]
        public string direccion { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        public string estado { get; set; }
        [Required]
        public decimal total { get; set; }

        [ForeignKey("idCliente")]
        public virtual Cliente cliente { get; set; }
        [ForeignKey("idTienda")]
        public virtual Tienda tienda { get; set; }
        public virtual ICollection<CompraItem> items { get; set; }
    }
}
