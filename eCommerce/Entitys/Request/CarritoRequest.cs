namespace eCommerce.Entitys.Request
{
    public class CarritoRequest
    {
        public int idArticulo {  get; set; }
    }

    public class CarritoItemoRequest
    {
        public int idCarritoItem { get; set; }
        public int cantidad { get; set; }
    }
}
