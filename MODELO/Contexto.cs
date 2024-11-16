using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ENTIDADES;
using System.Reflection.Emit;
using System.Data.Entity.ModelConfiguration.Conventions;


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

            // Configuración Producto
            modelBuilder.Entity<Producto>().ToTable("Productos");
            modelBuilder.Entity<Producto>().HasKey(p => p.Id);
            modelBuilder.Entity<Producto>().Property(p => p.CodigoBarra).IsRequired().HasMaxLength(13);
            modelBuilder.Entity<Producto>().HasIndex(p => p.CodigoBarra).IsUnique();

            // Configuración Venta y DetalleVenta
            modelBuilder.Entity<Venta>()
        .HasMany(v => v.Detalles)
        .WithOptional(d => d.Venta)
        .HasForeignKey(d => d.VentaId);

            modelBuilder.Entity<DetalleVenta>()
                .HasRequired(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.ProductoId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

