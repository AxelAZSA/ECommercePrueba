using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Entitys
{
    public class Carrito
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int idCliente { get; set; }


        [ForeignKey("idCliente")]
        public virtual Cliente? cliente { get; set; }
        public virtual ICollection<CarritoItem>? items { get; set; }
    }
}
