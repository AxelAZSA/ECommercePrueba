using System.ComponentModel.DataAnnotations;

namespace eCommerce.Entitys.Tokens
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
