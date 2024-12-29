namespace MODELO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Documento = c.String(nullable: false, maxLength: 8),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Direccion = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Documento, unique: true);
            
            CreateTable(
                "dbo.Venta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        FechaVenta = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FormaPago = c.String(nullable: false),
                        Cliente_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Id)
                .Index(t => t.ClienteId)
                .Index(t => t.Cliente_Id);
            
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
                        ProductoNombre = c.String(nullable: false),
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Cuit, unique: true);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreUsuario = c.String(nullable: false, maxLength: 50),
                        Contraseña = c.String(),
                        Rol = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.NombreUsuario, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Venta", "Cliente_Id", "dbo.Clientes");
            DropForeignKey("dbo.DetallesVenta", "VentaId", "dbo.Venta");
            DropForeignKey("dbo.DetallesVenta", "ProductoId", "dbo.Producto");
            DropForeignKey("dbo.DetallesVenta", "Producto_Id", "dbo.Producto");
            DropForeignKey("dbo.DetallesCompra", "ProductoId", "dbo.Producto");
            DropForeignKey("dbo.Compra", "ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.DetallesCompra", "CompraId", "dbo.Compra");
            DropForeignKey("dbo.Venta", "ClienteId", "dbo.Clientes");
            DropIndex("dbo.Usuarios", new[] { "NombreUsuario" });
            DropIndex("dbo.Proveedores", new[] { "Cuit" });
            DropIndex("dbo.Compra", new[] { "ProveedorId" });
            DropIndex("dbo.DetallesCompra", new[] { "ProductoId" });
            DropIndex("dbo.DetallesCompra", new[] { "CompraId" });
            DropIndex("dbo.DetallesVenta", new[] { "Producto_Id" });
            DropIndex("dbo.DetallesVenta", new[] { "ProductoId" });
            DropIndex("dbo.DetallesVenta", new[] { "VentaId" });
            DropIndex("dbo.Venta", new[] { "Cliente_Id" });
            DropIndex("dbo.Venta", new[] { "ClienteId" });
            DropIndex("dbo.Clientes", new[] { "Documento" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Proveedores");
            DropTable("dbo.Compra");
            DropTable("dbo.DetallesCompra");
            DropTable("dbo.Producto");
            DropTable("dbo.DetallesVenta");
            DropTable("dbo.Venta");
            DropTable("dbo.Clientes");
        }
    }
}
