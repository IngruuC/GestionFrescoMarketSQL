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
        private readonly Contexto _contexto;

        public ControladoraUsuario()
        {
            _contexto = new Contexto();
        }

        public bool ValidarUsuario(string username, string password)
        {
            var usuario = _contexto.Usuarios
                .FirstOrDefault(u => u.Username == username && u.Activo);

            if (usuario == null) return false;

            return BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash);
        }

        public void CrearUsuarioAdmin()
        {
            if (!_contexto.Usuarios.Any(u => u.Username == "admin"))
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword("123456");
                var usuario = new Usuario
                {
                    Username = "admin",
                    PasswordHash = passwordHash,
                    Rol = "Administrador",
                    Activo = true
                };
                _contexto.Usuarios.Add(usuario);
                _contexto.SaveChanges();
            }
        }

        public Usuario ObtenerUsuarioPorUsername(string username)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.Username == username);
        }
    }
}

