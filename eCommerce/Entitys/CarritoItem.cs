using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Entitys
{
    public class CarritoItem
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int idArticulo { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public int idCarrito { get; set; }


        [ForeignKey("idCarrito")]
        public virtual Carrito? carrito { get; set; }
        [ForeignKey("idArticulo")]
        public virtual Articulo? articulo { get; set; }
    }
}
