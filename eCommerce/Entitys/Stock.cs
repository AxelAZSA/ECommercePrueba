using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Entitys
{
    public class Stock
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int idArticulo { get; set; }
        [Required]
        public int idTienda { get; set; }
        [Required]
        public DateTime fechaRegistro { get; set; }


        [ForeignKey("idTienda")]
        public virtual Tienda? tienda { get; set; }
        [ForeignKey("idArticulo")]
        public virtual Articulo? articulo { get; set; }
    }
}
