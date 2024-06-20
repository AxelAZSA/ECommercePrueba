using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Entitys
{
    public class imagenes
    {
        [Key]
        public int id { get; set; }
        [Required]
        public byte[] imagen { get; set; }
        [Required]
        public int idArticulo { get; set; }
        [Required]
        public string ContentType { get; set; }

        [ForeignKey("idArticulo")]
        public virtual Articulo? articulo { get; set; }
    }
}
