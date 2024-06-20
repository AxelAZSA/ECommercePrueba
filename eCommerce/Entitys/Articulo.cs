using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entitys
{
    public class Articulo
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string codigo {  get; set; }
        [Required]
        public string descripcion {  get; set; }
        [Required]
        public decimal precio { get; set; }

        public virtual ICollection<imagenes>? imagenes { get; set; }
        public virtual ICollection<Stock>? stocks { get; set; }
        public virtual ICollection<CompraItem>? coItems { get; set; }
        public virtual ICollection<CarritoItem>? items { get; set; }
    }
}
