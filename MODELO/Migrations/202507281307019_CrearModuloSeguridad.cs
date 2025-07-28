namespace MODELO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearModuloSeguridad : DbMigration
    {
        public override void Up()
        {
            // ===== CREAR SOLO LAS TABLAS QUE NO EXISTEN =====

            CreateTable(
                "dbo.Acciones",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ComponenteId = c.Int(nullable: false),
                    Codigo = c.String(nullable: false, maxLength: 100),
                    Nombre = c.String(nullable: false, maxLength: 100),
                    Descripcion = c.String(maxLength: 200),
                    Asignada = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.EstadosGrupo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Nombre = c.String(nullable: false, maxLength: 50),
                    Descripcion = c.String(maxLength: 200),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.EstadosUsuario",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Nombre = c.String(nullable: false, maxLength: 50),
                    Descripcion = c.String(maxLength: 200),
                })
                .PrimaryKey(t => t.Id);

            // ===== ACTUALIZAR TABLA GRUPOS EXISTENTE =====
            // Tu tabla Grupos solo tiene: Id, NombreGrupo, Descripcion
            // Necesita: ComponenteId, Codigo, Nombre, Asignado, GrupoId, EstadoGrupoId

            AddColumn("dbo.Grupos", "ComponenteId", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.Grupos", "Codigo", c => c.String(maxLength: 50));
            AddColumn("dbo.Grupos", "Nombre", c => c.String(maxLength: 100));
            AddColumn("dbo.Grupos", "Asignado", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Grupos", "GrupoId", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.Grupos", "EstadoGrupoId", c => c.Int());

            // Agregar foreign key después de crear EstadosGrupo
            AddForeignKey("dbo.Grupos", "EstadoGrupoId", "dbo.EstadosGrupo", "Id");
            CreateIndex("dbo.Grupos", "EstadoGrupoId");

            // ===== ACTUALIZAR TABLA USUARIOS EXISTENTE =====
            // Agregar columnas que podrían faltar

            AddColumn("dbo.Usuarios", "NombreyApellido", c => c.String(maxLength: 100));
            AddColumn("dbo.Usuarios", "Clave", c => c.String());
            AddColumn("dbo.Usuarios", "EstadoUsuarioId", c => c.Int());

            // Agregar foreign key
            AddForeignKey("dbo.Usuarios", "EstadoUsuarioId", "dbo.EstadosUsuario", "Id");
            CreateIndex("dbo.Usuarios", "EstadoUsuarioId");

            // ===== CREAR SOLO LA TABLA DE RELACIÓN QUE FALTA =====
            // GruposAcciones es la única que probablemente no existe

            CreateTable(
                "dbo.GruposAcciones",
                c => new
                {
                    GrupoId = c.Int(nullable: false),
                    AccionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.GrupoId, t.AccionId })
                .ForeignKey("dbo.Grupos", t => t.GrupoId, cascadeDelete: true)
                .ForeignKey("dbo.Acciones", t => t.AccionId, cascadeDelete: true)
                .Index(t => t.GrupoId)
                .Index(t => t.AccionId);

            // ===== TABLAS QUE YA EXISTEN Y NO SE TOCAN =====
            // - dbo.Permisos ✓
            // - dbo.UsuariosGrupos ✓  
            // - dbo.GruposPermisos ✓
            // - dbo.AuditoriasSesion ✓
            // - dbo.Productos ✓
            // - dbo.Proveedores ✓
            // - dbo.Clientes ✓
            // - dbo.Ventas, etc. ✓
        }

        public override void Down()
        {
            // ===== ELIMINAR SOLO LO QUE CREAMOS =====

            // Eliminar tabla de relación nueva
            DropForeignKey("dbo.GruposAcciones", "AccionId", "dbo.Acciones");
            DropForeignKey("dbo.GruposAcciones", "GrupoId", "dbo.Grupos");
            DropIndex("dbo.GruposAcciones", new[] { "AccionId" });
            DropIndex("dbo.GruposAcciones", new[] { "GrupoId" });
            DropTable("dbo.GruposAcciones");

            // Eliminar foreign keys agregados
            DropForeignKey("dbo.Grupos", "EstadoGrupoId", "dbo.EstadosGrupo");
            DropForeignKey("dbo.Usuarios", "EstadoUsuarioId", "dbo.EstadosUsuario");
            DropIndex("dbo.Grupos", new[] { "EstadoGrupoId" });
            DropIndex("dbo.Usuarios", new[] { "EstadoUsuarioId" });

            // Eliminar tablas nuevas
            DropTable("dbo.EstadosUsuario");
            DropTable("dbo.EstadosGrupo");
            DropTable("dbo.Acciones");

            // Eliminar columnas agregadas a tablas existentes
            DropColumn("dbo.Grupos", "EstadoGrupoId");
            DropColumn("dbo.Grupos", "GrupoId");
            DropColumn("dbo.Grupos", "Asignado");
            DropColumn("dbo.Grupos", "Nombre");
            DropColumn("dbo.Grupos", "Codigo");
            DropColumn("dbo.Grupos", "ComponenteId");

            DropColumn("dbo.Usuarios", "EstadoUsuarioId");
            DropColumn("dbo.Usuarios", "Clave");
            DropColumn("dbo.Usuarios", "NombreyApellido");


        }
    }
}
