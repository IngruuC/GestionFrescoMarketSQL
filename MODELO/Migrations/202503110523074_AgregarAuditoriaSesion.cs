namespace MODELO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarAuditoriaSesion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditoriasSesion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        NombreUsuario = c.String(nullable: false, maxLength: 50),
                        FechaIngreso = c.DateTime(nullable: false),
                        FechaSalida = c.DateTime(),
                        DireccionIP = c.String(maxLength: 50),
                        Dispositivo = c.String(maxLength: 100),
                        TipoSesion = c.String(maxLength: 20),
                        SesionActiva = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreUsuario = c.String(nullable: false, maxLength: 50),
                        Contraseña = c.String(nullable: false),
                        Rol = c.String(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        UltimoAcceso = c.DateTime(),
                        IntentosIngreso = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.NombreUsuario, unique: true);
            
            CreateTable(
                "dbo.Grupos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreGrupo = c.String(nullable: false, maxLength: 50),
                        Descripcion = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permisos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombrePermiso = c.String(nullable: false, maxLength: 50),
                        Descripcion = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Documento = c.String(nullable: false, maxLength: 8),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Direccion = c.String(),
                        UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .Index(t => t.Documento, unique: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Venta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FormaPago = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.DetallesVenta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VentaId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductoNombre = c.String(nullable: false, maxLength: 100),
                        Producto_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producto", t => t.Producto_Id)
                .ForeignKey("dbo.Producto", t => t.ProductoId)
                .ForeignKey("dbo.Venta", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.VentaId)
                .Index(t => t.ProductoId)
                .Index(t => t.Producto_Id);
            
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        CodigoBarra = c.String(nullable: false),
                        EsPerecedero = c.Boolean(nullable: false),
                        FechaVencimiento = c.DateTime(),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DetallesCompra",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompraId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductoNombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Compra", t => t.CompraId, cascadeDelete: true)
                .ForeignKey("dbo.Producto", t => t.ProductoId)
                .Index(t => t.CompraId)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.Compra",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProveedorId = c.Int(nullable: false),
                        FechaCompra = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FormaPago = c.String(nullable: false),
                        NumeroFactura = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Proveedores", t => t.ProveedorId)
                .Index(t => t.ProveedorId);
            
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cuit = c.String(nullable: false, maxLength: 11),
                        RazonSocial = c.String(),
                        Telefono = c.String(),
                        Email = c.String(),
                        Direccion = c.String(),
                        UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .Index(t => t.Cuit, unique: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.ProveedorProducto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProveedorId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        PrecioCompra = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producto", t => t.ProductoId, cascadeDelete: true)
                .ForeignKey("dbo.Proveedores", t => t.ProveedorId, cascadeDelete: true)
                .Index(t => new { t.ProveedorId, t.ProductoId }, unique: true);
            
            CreateTable(
                "dbo.GruposPermisos",
                c => new
                    {
                        GrupoId = c.Int(nullable: false),
                        PermisoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GrupoId, t.PermisoId })
                .ForeignKey("dbo.Grupos", t => t.GrupoId, cascadeDelete: true)
                .ForeignKey("dbo.Permisos", t => t.PermisoId, cascadeDelete: true)
                .Index(t => t.GrupoId)
                .Index(t => t.PermisoId);
            
            CreateTable(
                "dbo.UsuariosGrupos",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false),
                        GrupoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioId, t.GrupoId })
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .ForeignKey("dbo.Grupos", t => t.GrupoId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.GrupoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProveedorProducto", "ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.ProveedorProducto", "ProductoId", "dbo.Producto");
            DropForeignKey("dbo.DetallesVenta", "VentaId", "dbo.Venta");
            DropForeignKey("dbo.DetallesVenta", "ProductoId", "dbo.Producto");
            DropForeignKey("dbo.DetallesVenta", "Producto_Id", "dbo.Producto");
            DropForeignKey("dbo.DetallesCompra", "ProductoId", "dbo.Producto");
            DropForeignKey("dbo.Compra", "ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.Proveedores", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.DetallesCompra", "CompraId", "dbo.Compra");
            DropForeignKey("dbo.Venta", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Clientes", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.AuditoriasSesion", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.UsuariosGrupos", "GrupoId", "dbo.Grupos");
            DropForeignKey("dbo.UsuariosGrupos", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.GruposPermisos", "PermisoId", "dbo.Permisos");
            DropForeignKey("dbo.GruposPermisos", "GrupoId", "dbo.Grupos");
            DropIndex("dbo.UsuariosGrupos", new[] { "GrupoId" });
            DropIndex("dbo.UsuariosGrupos", new[] { "UsuarioId" });
            DropIndex("dbo.GruposPermisos", new[] { "PermisoId" });
            DropIndex("dbo.GruposPermisos", new[] { "GrupoId" });
            DropIndex("dbo.ProveedorProducto", new[] { "ProveedorId", "ProductoId" });
            DropIndex("dbo.Proveedores", new[] { "UsuarioId" });
            DropIndex("dbo.Proveedores", new[] { "Cuit" });
            DropIndex("dbo.Compra", new[] { "ProveedorId" });
            DropIndex("dbo.DetallesCompra", new[] { "ProductoId" });
            DropIndex("dbo.DetallesCompra", new[] { "CompraId" });
            DropIndex("dbo.DetallesVenta", new[] { "Producto_Id" });
            DropIndex("dbo.DetallesVenta", new[] { "ProductoId" });
            DropIndex("dbo.DetallesVenta", new[] { "VentaId" });
            DropIndex("dbo.Venta", new[] { "ClienteId" });
            DropIndex("dbo.Clientes", new[] { "UsuarioId" });
            DropIndex("dbo.Clientes", new[] { "Documento" });
            DropIndex("dbo.Usuarios", new[] { "NombreUsuario" });
            DropIndex("dbo.AuditoriasSesion", new[] { "UsuarioId" });
            DropTable("dbo.UsuariosGrupos");
            DropTable("dbo.GruposPermisos");
            DropTable("dbo.ProveedorProducto");
            DropTable("dbo.Proveedores");
            DropTable("dbo.Compra");
            DropTable("dbo.DetallesCompra");
            DropTable("dbo.Producto");
            DropTable("dbo.DetallesVenta");
            DropTable("dbo.Venta");
            DropTable("dbo.Clientes");
            DropTable("dbo.Permisos");
            DropTable("dbo.Grupos");
            DropTable("dbo.Usuarios");
            DropTable("dbo.AuditoriasSesion");
        }
    }
}
