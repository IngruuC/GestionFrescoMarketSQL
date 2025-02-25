using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using MODELO;
using ENTIDADES;
using System.Data.Entity;
using BCrypt.Net;

namespace CONTROLADORA
{
    public class ControladoraUsuario
    {
        private static ControladoraUsuario instancia;
        private readonly Contexto contexto;
        private const string DEFAULT_PASSWORD = "admin123";

        private ControladoraUsuario()
        {
            contexto = new Contexto();
            InicializarAdministrador();
        }

        private void InicializarAdministrador()
        {
            try
            {
                using (var transaction = contexto.Database.BeginTransaction())
                {
                    try
                    {
                        // Verificar si ya existe el usuario admin
                        var usuarioAdmin = contexto.Usuarios.FirstOrDefault(u => u.NombreUsuario == "admin");

                        if (usuarioAdmin != null)
                        {
                            ResetearClave(usuarioAdmin.Id);
                        }

                        // Crear grupo de administrador si no existe
                        var grupoAdmin = contexto.Grupos.FirstOrDefault(g => g.NombreGrupo == "Administrador");
                        if (grupoAdmin == null)
                        {
                            grupoAdmin = new Grupo
                            {
                                NombreGrupo = "Administrador",
                                Descripcion = "Grupo con todos los permisos del sistema"
                            };
                            contexto.Grupos.Add(grupoAdmin);
                            contexto.SaveChanges();
                        }

                        // Si no existe el usuario admin, crearlo
                        if (usuarioAdmin == null)
                        {
                            string contraseña = DEFAULT_PASSWORD;
                            string contraseñaHash = BCrypt.Net.BCrypt.HashPassword(contraseña);

                            // Verificación del hash
                            bool verificacionHash = BCrypt.Net.BCrypt.Verify(contraseña, contraseñaHash);

                            // Mostrar información de depuración (para Windows Forms)
                            System.Diagnostics.Debug.WriteLine(
                                $"Contraseña: {contraseña}\n" +
                                $"Hash generado: {contraseñaHash}\n" +
                                $"Verificación del hash: {verificacionHash}"
                            );

                            usuarioAdmin = new Usuario
                            {
                                NombreUsuario = "admin",
                                Contraseña = contraseñaHash,
                                Rol = "Administrador",
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                IntentosIngreso = 0,
                                Email = "admin@sistema.com",
                                Grupos = new List<Grupo> { grupoAdmin }
                            };
                            contexto.Usuarios.Add(usuarioAdmin);
                            contexto.SaveChanges();
                        }
                        else
                        {
                            // Asegurar que el usuario admin tenga el grupo correcto
                            if (!usuarioAdmin.Grupos.Any(g => g.NombreGrupo == "Administrador"))
                            {
                                usuarioAdmin.Grupos.Add(grupoAdmin);
                                contexto.SaveChanges();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Error al crear usuario administrador: {ex.Message}", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la transacción: {ex.Message}", ex);
            }
        }

        public static ControladoraUsuario ObtenerInstancia()
        {
            if (instancia == null)
                instancia = new ControladoraUsuario();
            return instancia;
        }

        public class ResultadoLogin
        {
            public bool Exitoso { get; set; }
            public string Mensaje { get; set; }
            public Usuario Usuario { get; set; }
        }

        public ResultadoLogin ValidarCredenciales(string usuario, string contraseña)
        {
            try
            {
                // Primero verificamos si el usuario existe
                var usuarioEncontrado = contexto.Usuarios
                    .Include(u => u.Grupos)  // Aseguramos que se carguen los grupos
                    .FirstOrDefault(u => u.NombreUsuario == usuario);

                if (usuarioEncontrado == null)
                {
                    return new ResultadoLogin
                    {
                        Exitoso = false,
                        Mensaje = "Usuario no encontrado en la base de datos"
                    };
                }

                // Verificamos el estado del usuario
                if (!usuarioEncontrado.Estado)
                {
                    return new ResultadoLogin
                    {
                        Exitoso = false,
                        Mensaje = "Usuario desactivado"
                    };
                }

                // Verificamos la contraseña
                bool credencialesValidas = false;
                try
                {
                    // DEBUG: Imprimir información detallada
                    System.Diagnostics.Debug.WriteLine($"Usuario: {usuario}");
                    System.Diagnostics.Debug.WriteLine($"Contraseña ingresada: {contraseña}");
                    System.Diagnostics.Debug.WriteLine($"Hash almacenado: {usuarioEncontrado.Contraseña}");

                    // Intenta verificar la contraseña
                    credencialesValidas = BCrypt.Net.BCrypt.Verify(contraseña, usuarioEncontrado.Contraseña);

                    System.Diagnostics.Debug.WriteLine($"Verificación de contraseña: {credencialesValidas}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error en verificación: {ex.Message}");
                    return new ResultadoLogin
                    {
                        Exitoso = false,
                        Mensaje = $"Error al verificar contraseña: {ex.Message}"
                    };
                }

                if (!credencialesValidas)
                {
                    return new ResultadoLogin
                    {
                        Exitoso = false,
                        Mensaje = "Contraseña incorrecta"
                    };
                }

                // Verificamos que el usuario tenga grupos asignados
                if (usuarioEncontrado.Grupos == null || !usuarioEncontrado.Grupos.Any())
                {
                    return new ResultadoLogin
                    {
                        Exitoso = false,
                        Mensaje = "El usuario no tiene grupos asignados"
                    };
                }

                // Login exitoso
                return new ResultadoLogin
                {
                    Exitoso = true,
                    Usuario = usuarioEncontrado,
                    Mensaje = "Login exitoso"
                };
            }
            catch (Exception ex)
            {
                return new ResultadoLogin
                {
                    Exitoso = false,
                    Mensaje = $"Error en validación: {ex.Message}"
                };
            }
        }

        public void ActualizarAcceso(int usuarioId)
        {
            var usuario = contexto.Usuarios.Find(usuarioId);
            if (usuario != null)
            {
                usuario.UltimoAcceso = DateTime.Now;
                usuario.IntentosIngreso = 0;
                contexto.SaveChanges();
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return contexto.Usuarios
                .Include(u => u.Grupos)
                .ToList();
        }

        public Usuario ObtenerUsuario(string nombreUsuario)
        {
            return contexto.Usuarios
                .Include(u => u.Grupos)
                .FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
        }

        public void AgregarUsuario(Usuario usuario)
        {
            if (contexto.Usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario))
                throw new Exception("Ya existe un usuario con ese nombre");

            if (contexto.Usuarios.Any(u => u.Email == usuario.Email))
                throw new Exception("Ya existe un usuario con ese email");

            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }

        public void ModificarUsuario(Usuario usuario)
        {
            var usuarioExistente = contexto.Usuarios.Find(usuario.Id);
            if (usuarioExistente == null)
                throw new Exception("Usuario no encontrado");

            // Actualizar propiedades
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Estado = usuario.Estado;
            usuarioExistente.Grupos = usuario.Grupos;

            contexto.SaveChanges();
        }

        public void EliminarUsuario(int id)
        {
            var usuario = contexto.Usuarios.Find(id);
            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            if (usuario.NombreUsuario == "admin")
                throw new Exception("No se puede eliminar el usuario administrador");

            contexto.Usuarios.Remove(usuario);
            contexto.SaveChanges();
        }

        public void ResetearClave(int id)
        {
            var usuario = contexto.Usuarios.Find(id);
            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(DEFAULT_PASSWORD);
            usuario.IntentosIngreso = 0;
            contexto.SaveChanges();

            // Aquí deberías implementar el envío de email con la nueva contraseña
            EnviarEmailNuevaContraseña(usuario.Email, DEFAULT_PASSWORD);
        }

        private void EnviarEmailNuevaContraseña(string email, string nuevaContraseña)
        {
            // Implementar el envío de email
            // Por ahora solo logueamos
            Console.WriteLine($"Email enviado a {email} con la nueva contraseña: {nuevaContraseña}");
        }

        public List<Grupo> ObtenerGrupos()
        {
            return contexto.Grupos
                .Include(g => g.Permisos)
                .ToList();
        }

        public bool TienePermiso(int usuarioId, string permiso)
        {
            var usuario = contexto.Usuarios
                .Include(u => u.Grupos)
                .FirstOrDefault(u => u.Id == usuarioId);

            if (usuario == null)
                return false;

            return usuario.Grupos.Any(g => g.Permisos.Any(p => p.NombrePermiso == permiso));
        }


        public void RegistrarCliente(Usuario usuario, Cliente cliente)
        {

            using (var transaction = contexto.Database.BeginTransaction())
            {
                try
                {
                    // Verificaciones
                    if (contexto.Usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario))
                        throw new Exception("El nombre de usuario ya está en uso");

                    if (contexto.Clientes.Any(c => c.Documento == cliente.Documento))
                        throw new Exception("Ya existe un cliente con ese documento");

                    // Obtener grupo Cliente
                    var grupoCliente = contexto.Grupos.FirstOrDefault(g => g.NombreGrupo == "Cliente");
                    if (grupoCliente == null)
                        throw new Exception("No se encontró el grupo Cliente");

                    // Preparar y guardar usuario
                    usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
                    usuario.Grupos = new List<Grupo> { grupoCliente };
                    contexto.Usuarios.Add(usuario);
                    contexto.SaveChanges();

                    // Preparar y guardar cliente
                    cliente.UsuarioId = usuario.Id;
                    contexto.Clientes.Add(cliente);
                    contexto.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Error al registrar cliente: {ex.Message}", ex);
                }
            }
        }

        public void RegistrarProveedor(Usuario usuario, Proveedor proveedor)
        {
            using (var transaction = contexto.Database.BeginTransaction())
            {
                try
                {
                    // Verificar si ya existe el usuario o CUIT
                    if (contexto.Usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario))
                        throw new Exception("El nombre de usuario ya está en uso");

                    if (contexto.Proveedores.Any(p => p.Cuit == proveedor.Cuit))
                        throw new Exception("Ya existe un proveedor con ese CUIT");

                    // Obtener el grupo Proveedor
                    var grupoProveedor = contexto.Grupos.FirstOrDefault(g => g.NombreGrupo == "Proveedor");
                    if (grupoProveedor == null)
                        throw new Exception("No se encontró el grupo Proveedor");

                    // Hashear la contraseña
                    usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
                    usuario.Grupos = new List<Grupo> { grupoProveedor };

                    // Guardar el usuario
                    contexto.Usuarios.Add(usuario);
                    contexto.SaveChanges();

                    // Asociar el proveedor con el usuario
                    proveedor.UsuarioId = usuario.Id;
                    contexto.Proveedores.Add(proveedor);
                    contexto.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool ExisteUsuario(string nombreUsuario)
        {
            return contexto.Usuarios.Any(u => u.NombreUsuario == nombreUsuario);
        }

        public bool ExisteCliente(string documento)
        {
            return contexto.Clientes.Any(c => c.Documento == documento);
        }

        public bool ExisteProveedor(string cuit)
        {
            return contexto.Proveedores.Any(p => p.Cuit == cuit);
        }


        public Grupo ObtenerGrupoPorNombre(string nombreGrupo)
        {
            return contexto.Grupos.FirstOrDefault(g => g.NombreGrupo == nombreGrupo);
        }
    }
}

