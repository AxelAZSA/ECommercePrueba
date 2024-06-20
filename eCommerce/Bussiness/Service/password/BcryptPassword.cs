using Microsoft.AspNetCore.Identity;

namespace eCommerce.Bussiness.Service.password
{
    public class BcryptPassword : IPasswordHasher
    {
        //Encripta la contraseña
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        //Verifica la contraseña
        public bool Verify(string password, string passwordHasher)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHasher);
        }
    }
}
