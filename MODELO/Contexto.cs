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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

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

            base.OnModelCreating(modelBuilder);
        
        }
    }
}

