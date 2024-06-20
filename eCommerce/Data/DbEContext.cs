using eCommerce.Entitys;
using eCommerce.Entitys.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eCommerce.Data
{
    public class DbEContext : DbContext
    {
        public DbEContext(DbContextOptions<DbEContext> options)
          : base(options)
        {
        }

        public DbSet<Admin> admins { get; set; }
        public DbSet<Articulo> articulos { get; set; }
        public DbSet<Carrito> carritos { get; set; }
        public DbSet<CarritoItem> carritoItems { get; set; }
        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Compra> compras { get; set; }
        public DbSet<CompraItem> compraItems { get; set; }
        public DbSet<imagenes> imagenes { get; set; }
        public DbSet<Stock> stocks { get; set; }
        public DbSet<Tienda> tiendas { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cliente
            modelBuilder.Entity<Carrito>()
            .HasOne(co => co.cliente).WithOne(c => c.carrito)
            .HasForeignKey<Carrito>(co => co.idCliente);

            //Compra
            modelBuilder.Entity<Compra>()
               .HasOne(co => co.cliente).WithMany(c => c.compras)
               .HasForeignKey(co => co.idCliente);

            modelBuilder.Entity<Compra>()
           .HasOne(co => co.tienda).WithMany(t => t.compras)
           .HasForeignKey(co => co.idTienda);

            modelBuilder.Entity<CompraItem>()
               .HasOne(ci => ci.compra).WithMany(co => co.items)
               .HasForeignKey(ci=>ci.idCompra);

           modelBuilder.Entity<CompraItem>()
               .HasOne(ci=>ci.articulo).WithMany(a=>a.coItems)
               .HasForeignKey(ci=>ci.idArticulo);

            //Carrito
            modelBuilder.Entity<CarritoItem>()
               .HasOne(ci =>ci.carrito).WithMany(c => c.items)
               .HasForeignKey(ci=>ci.idCarrito);

            modelBuilder.Entity<CarritoItem>()
               .HasOne(ci => ci.articulo).WithMany(c => c.items)
               .HasForeignKey(ci => ci.idArticulo);

            //Tienda
            modelBuilder.Entity<Stock>()
               .HasOne(s => s.articulo).WithMany(c => c.stocks)
               .HasForeignKey(s => s.idArticulo);

            modelBuilder.Entity<Stock>()
               .HasOne(s => s.tienda).WithMany(t=>t.stocks)
               .HasForeignKey(s => s.idTienda);

            modelBuilder.Entity<imagenes>()
          .HasOne(ci => ci.articulo).WithMany(c => c.imagenes)
          .HasForeignKey(ci => ci.idArticulo);

            base.OnModelCreating(modelBuilder);
        }
    }
}
