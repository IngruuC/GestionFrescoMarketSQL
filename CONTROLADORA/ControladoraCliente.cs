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
        private readonly Contexto _contexto;

        public ControladoraCliente()
        {
            _contexto = new Contexto();
        }

        public List<Cliente> ObtenerClientes()
        {
            return _contexto.Clientes.ToList();
        }

        public void AgregarCliente(Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.Documento))
                throw new Exception("El documento es requerido");

            if (_contexto.Clientes.Any(c => c.Documento == cliente.Documento))
                throw new Exception("Ya existe un cliente con ese documento");

            _contexto.Clientes.Add(cliente);
            _contexto.SaveChanges();
        }

        public void ActualizarCliente(Cliente cliente)
        {
            var clienteExistente = _contexto.Clientes.Find(cliente.Id);
            if (clienteExistente == null)
                throw new Exception("Cliente no encontrado");

            if (_contexto.Clientes.Any(c => c.Documento == cliente.Documento && c.Id != cliente.Id))
                throw new Exception("Ya existe otro cliente con ese documento");

            _contexto.Entry(clienteExistente).CurrentValues.SetValues(cliente);
            _contexto.SaveChanges();
        }

        public void EliminarCliente(int id)
        {
            var cliente = _contexto.Clientes.Find(id);
            if (cliente != null)
            {
                _contexto.Clientes.Remove(cliente);
                _contexto.SaveChanges();
            }
        }

    }
}
