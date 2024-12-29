using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ENTIDADES;
using System.Reflection.Emit;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace MODELO
{
    public class Contexto : DbContext
    {
        public Contexto(): base(@"Data Source=.\SQLEXPRESS;Initial Catalog=SupermercadoDB;Integrated Security=True")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }

        // Nuevos DBSets para Compras
        public DbSet<Proveedor> Proveedores { get; set; } //
        public DbSet<Compra> Compras { get; set; } //
        public DbSet<DetalleCompra> DetallesCompra { get; set; } //


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Producto>().ToTable("Producto");

            // Configuración Usuario
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
            modelBuilder.Entity<Usuario>().Property(u => u.NombreUsuario).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Usuario>().HasIndex(u => u.NombreUsuario).IsUnique();

            // Configuración Cliente
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Cliente>().HasKey(c => c.Id);
            modelBuilder.Entity<Cliente>().Property(c => c.Documento).IsRequired().HasMaxLength(8);
            modelBuilder.Entity<Cliente>().HasIndex(c => c.Documento).IsUnique();

            // Configuración de decimal para precio en Producto
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal")
                .HasPrecision(18, 2);

            // Configuración para Total en Venta
            modelBuilder.Entity<Venta>()
                .Property(v => v.Total)
                .HasColumnType("decimal")
                .HasPrecision(18, 2);

            // Configuración para PrecioUnitario y Subtotal en DetalleVenta
            modelBuilder.Entity<DetalleVenta>()
                .Property(d => d.PrecioUnitario)
                .HasColumnType("decimal")
                .HasPrecision(18, 2);

            modelBuilder.Entity<DetalleVenta>()
                .Property(d => d.Subtotal)
                .HasColumnType("decimal")
                .HasPrecision(18, 2);

            // Configuración de relaciones
            modelBuilder.Entity<Venta>()
                .HasRequired(v => v.Cliente)
                .WithMany()
                .HasForeignKey(v => v.ClienteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Venta>()
                .HasMany(v => v.Detalles)
                .WithRequired(d => d.Venta)
                .HasForeignKey(d => d.VentaId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<DetalleVenta>()
                .HasRequired(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.ProductoId)
                .WillCascadeOnDelete(false);



            // Nuevas configuraciones para Compras

            // Configuración Proveedor
            modelBuilder.Entity<Proveedor>().ToTable("Proveedores");
            modelBuilder.Entity<Proveedor>().HasKey(p => p.Id);
            modelBuilder.Entity<Proveedor>().Property(p => p.Cuit).IsRequired().HasMaxLength(11);
            modelBuilder.Entity<Proveedor>().HasIndex(p => p.Cuit).IsUnique();


            // Configuración para Total en Compra
            modelBuilder.Entity<Compra>()
                .Property(c => c.Total)
                .HasColumnType("decimal")
                .HasPrecision(18, 2);

            // Configuración para PrecioUnitario y Subtotal en DetalleCompra
            modelBuilder.Entity<DetalleCompra>()
                .Property(d => d.PrecioUnitario)
                .HasColumnType("decimal")
                .HasPrecision(18, 2);

            modelBuilder.Entity<DetalleCompra>()
                .Property(d => d.Subtotal)
                .HasColumnType("decimal")
                .HasPrecision(18, 2);

            // Configuración de relaciones
            modelBuilder.Entity<Compra>()
                .HasRequired(c => c.Proveedor)
                .WithMany(p => p.Compras)
                .HasForeignKey(c => c.ProveedorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Compra>()
                .HasMany(c => c.Detalles)
                .WithRequired(d => d.Compra)
                .HasForeignKey(d => d.CompraId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<DetalleCompra>()
                .HasRequired(d => d.Producto)
                .WithMany(p => p.DetallesCompra)
                .HasForeignKey(d => d.ProductoId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}

