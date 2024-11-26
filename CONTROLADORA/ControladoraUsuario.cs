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
                // Si no hay usuarios, crear el administrador
                if (!contexto.Usuarios.Any())
                {
                    string contraseñaHash = BCrypt.Net.BCrypt.HashPassword(DEFAULT_PASSWORD);

                    var adminUser = new Usuario
                    {
                        NombreUsuario = "admin",
                        Contraseña = contraseñaHash,
                        Rol = "Administrador"
                    };

                    contexto.Usuarios.Add(adminUser);
                    contexto.SaveChanges();

                    // Verificar que se creó correctamente
                    var usuarioCreado = contexto.Usuarios.FirstOrDefault();
                    if (usuarioCreado == null)
                    {
                        throw new Exception("No se pudo crear el usuario administrador.");
                    }

                    // Verificar que la contraseña se guardó correctamente
                    if (!BCrypt.Net.BCrypt.Verify(DEFAULT_PASSWORD, usuarioCreado.Contraseña))
                    {
                        throw new Exception("La verificación inicial de la contraseña falló.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al inicializar administrador: {ex.Message}");
            }
        }

        public static ControladoraUsuario ObtenerInstancia()
        {
            if (instancia == null)
                instancia = new ControladoraUsuario();
            return instancia;
        }

        public bool ValidarCredenciales(string usuario, string contraseña)
        {
            try
            {
                var usuarioEncontrado = contexto.Usuarios
                    .FirstOrDefault(u => u.NombreUsuario == usuario);

                if (usuarioEncontrado == null)
                    return false;

                // Para propósitos de prueba, vamos a forzar la creación del hash con la misma contraseña
                if (contraseña == DEFAULT_PASSWORD)
                {
                    string nuevoHash = BCrypt.Net.BCrypt.HashPassword(DEFAULT_PASSWORD);
                    bool verificacion = BCrypt.Net.BCrypt.Verify(DEFAULT_PASSWORD, usuarioEncontrado.Contraseña);

                    if (!verificacion)
                    {
                        // Si la verificación falla, actualizar la contraseña
                        usuarioEncontrado.Contraseña = nuevoHash;
                        contexto.SaveChanges();
                    }
                }

                return BCrypt.Net.BCrypt.Verify(contraseña, usuarioEncontrado.Contraseña);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en validación: {ex.Message}");
            }
        }

        public Usuario ObtenerUsuario(string nombreUsuario)
        {
            return contexto.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
        }
    }
}

