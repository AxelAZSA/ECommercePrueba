 using System.ComponentModel.DataAnnotations;
using System;

namespace eCommerce.Entitys.Tokens
{
    public class RefreshToken
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string token { get; set; }
        [Required]
        public int idSesion { get; set; }
        [Required]
        public string role { get; set; }
    }
}
