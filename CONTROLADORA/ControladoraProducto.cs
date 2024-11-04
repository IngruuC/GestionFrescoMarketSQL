using ENTIDADES;
using MODELO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BCrypt.Net;

namespace CONTROLADORA
{
    public class ControladoraProducto
    {
        private readonly Contexto _contexto;

        public ControladoraProducto()
        {
            _contexto = new Contexto();
        }

        public List<Producto> ObtenerProductos()
        {
            return _contexto.Productos.ToList();
        }

        public Producto ObtenerProductoPorId(int id)
        {
            return _contexto.Productos.Find(id);
        }

        public void AgregarProducto(Producto producto)
        {
            if (string.IsNullOrEmpty(producto.CodigoBarra))
                throw new Exception("El código de barras es requerido");

            if (_contexto.Productos.Any(p => p.CodigoBarra == producto.CodigoBarra))
                throw new Exception("Ya existe un producto con ese código de barras");

            if (producto.Precio <= 0)
                throw new Exception("El precio debe ser mayor que cero");

            if (producto.Stock < 0)
                throw new Exception("El stock no puede ser negativo");

            if (producto.EsPerecedero && !producto.FechaVencimiento.HasValue)
                throw new Exception("Los productos perecederos deben tener una fecha de vencimiento");

            _contexto.Productos.Add(producto);
            _contexto.SaveChanges();
        }

        public void ActualizarProducto(Producto producto)
        {
            var productoExistente = _contexto.Productos.Find(producto.Id);
            if (productoExistente == null)
                throw new Exception("Producto no encontrado");

            if (_contexto.Productos.Any(p => p.CodigoBarra == producto.CodigoBarra && p.Id != producto.Id))
                throw new Exception("Ya existe otro producto con ese código de barras");

            _contexto.Entry(productoExistente).CurrentValues.SetValues(producto);
            _contexto.SaveChanges();
        }

        public void EliminarProducto(int id)
        {
            var producto = _contexto.Productos.Find(id);
            if (producto != null)
            {
                _contexto.Productos.Remove(producto);
                _contexto.SaveChanges();
            }
        }

        public void ActualizarStock(int productoId, int cantidad)
        {
            var producto = _contexto.Productos.Find(productoId);
            if (producto == null)
                throw new Exception("Producto no encontrado");

            producto.Stock += cantidad;
            if (producto.Stock < 0)
                throw new Exception("No hay suficiente stock disponible");

            _contexto.SaveChanges();
        }
    }
}
