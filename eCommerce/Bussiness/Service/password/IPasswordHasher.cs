namespace eCommerce.Bussiness.Service.password
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string passwordHasher);
    }
}
