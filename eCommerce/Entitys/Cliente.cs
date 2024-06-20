using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entitys
{
    public class Cliente
    {

        [Key]
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]
        public string direccion {  get; set; }
        [Required]
        public string correo { get; set; }
        [Required]
        public string password { get; set; }
        public virtual Carrito? carrito { get; set; }

        public virtual ICollection<Compra>? compras { get; set; }
    }
}
