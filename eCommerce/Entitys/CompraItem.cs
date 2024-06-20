using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Entitys
{
    public class CompraItem
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int idCompra {  get; set; }
        [Required]
        public int idArticulo { get; set; }
        [Required]
        public decimal subtotal { get; set; }
        [Required]
        public int cantidad { get; set; }


        [ForeignKey("idCompra")]
        public virtual Compra? compra { get; set; }
        [ForeignKey("idArticulo")]
        public virtual Articulo? articulo { get; set; }

    }
}
