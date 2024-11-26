using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTIDADES;
using MODELO;
using System.Data.Entity;
using BCrypt.Net;

namespace CONTROLADORA
{
    public class ControladoraCliente
    {
        private static ControladoraCliente instancia;
        private Contexto contexto;

        private ControladoraCliente()
        {
            contexto = new Contexto();
        }

        public static ControladoraCliente ObtenerInstancia()
        {
            if (instancia == null)
                instancia = new ControladoraCliente();
            return instancia;
        }

        public void AgregarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Documento) || cliente.Documento.Length != 8)
                throw new Exception("El documento debe tener 8 dígitos.");

            if (contexto.Clientes.Any(c => c.Documento == cliente.Documento))
                throw new Exception("Ya existe un cliente con ese documento.");

            contexto.Clientes.Add(cliente);
            contexto.SaveChanges();
        }

        public void ModificarCliente(Cliente cliente)
        {
            var clienteExistente = contexto.Clientes.Find(cliente.Id);
            if (clienteExistente == null)
                throw new Exception("Cliente no encontrado.");

            if (contexto.Clientes.Any(c => c.Documento == cliente.Documento && c.Id != cliente.Id))
                throw new Exception("Ya existe otro cliente con ese documento.");

            clienteExistente.Documento = cliente.Documento;
            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Apellido = cliente.Apellido;
            clienteExistente.Direccion = cliente.Direccion;

            contexto.SaveChanges();
        }

        public void EliminarCliente(int id)
        {
            var cliente = contexto.Clientes.Find(id);
            if (cliente == null)
                throw new Exception("Cliente no encontrado.");

            if (contexto.Ventas.Any(v => v.ClienteId == id))
                throw new Exception("No se puede eliminar el cliente porque tiene ventas asociadas.");

            contexto.Clientes.Remove(cliente);
            contexto.SaveChanges();
        }

        public List<Cliente> ObtenerClientes()
        {
            return contexto.Clientes.ToList();
        }

    }
}
