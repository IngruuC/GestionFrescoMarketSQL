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
        private Contexto contexto;

        private ControladoraUsuario()
        {
            contexto = new Contexto();
        }

        public static ControladoraUsuario ObtenerInstancia()
        {
            if (instancia == null)
                instancia = new ControladoraUsuario();
            return instancia;
        }

        public bool ValidarCredenciales(string usuario, string contraseña)
        {
            var usuarioEncontrado = contexto.Usuarios
                .FirstOrDefault(u => u.NombreUsuario == usuario);

            if (usuarioEncontrado == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(contraseña, usuarioEncontrado.Contraseña);
        }

        public void AgregarUsuario(Usuario usuario)
        {
            if (contexto.Usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario))
                throw new Exception("El nombre de usuario ya existe.");

            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }

        public void ModificarUsuario(Usuario usuario)
        {
            var usuarioExistente = contexto.Usuarios.Find(usuario.Id);
            if (usuarioExistente == null)
                throw new Exception("Usuario no encontrado.");

            if (contexto.Usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario && u.Id != usuario.Id))
                throw new Exception("El nombre de usuario ya existe.");

            usuarioExistente.NombreUsuario = usuario.NombreUsuario;
            if (!string.IsNullOrEmpty(usuario.Contraseña))
                usuarioExistente.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);
            usuarioExistente.Rol = usuario.Rol;

            contexto.SaveChanges();
        }

        public void EliminarUsuario(int id)
        {
            var usuario = contexto.Usuarios.Find(id);
            if (usuario == null)
                throw new Exception("Usuario no encontrado.");

            contexto.Usuarios.Remove(usuario);
            contexto.SaveChanges();
        }
    }
}

