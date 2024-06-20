using eCommerce.Entitys;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entitys
{
    public class Tienda
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string sucursal {  get; set; }
        [Required]
        public string direccion { get; set; }

        public virtual ICollection<Stock>? stocks { get; set; }
        public virtual ICollection<Compra>? compras { get; set; } 
    }
}
