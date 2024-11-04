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
    public class ControladoraVenta
    {
        private readonly Contexto _contexto;
        private readonly ControladoraProducto _controladoraProducto;

        public ControladoraVenta()
        {
            _contexto = new Contexto();
            _controladoraProducto = new ControladoraProducto();
        }

        public List<Venta> ObtenerVentas()
        {
            return _contexto.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                .ToList();
        }

        public Venta ObtenerVentaPorId(int id)
        {
            return _contexto.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                .FirstOrDefault(v => v.VentaId == id);
        }

        public void RealizarVenta(Venta venta)
        {
            using (var transaction = _contexto.Database.BeginTransaction())
            {
                try
                {
                    if (venta.ClienteId == 0)
                        throw new Exception("Debe seleccionar un cliente");

                    if (!venta.Detalles.Any())
                        throw new Exception("La venta debe tener al menos un producto");

                    // Verificar y actualizar stock
                    foreach (var detalle in venta.Detalles)
                    {
                        var producto = _contexto.Productos.Find(detalle.ProductoId);
                        if (producto == null)
                            throw new Exception($"Producto no encontrado: {detalle.ProductoId}");

                        if (producto.Stock < detalle.CantidadProducto)
                            throw new Exception($"Stock insuficiente para el producto: {producto.Nombre}");

                        producto.Stock -= detalle.CantidadProducto;

                        // Actualizar detalles
                        detalle.PrecioUnitario = producto.Precio;
                        detalle.Subtotal = detalle.CantidadProducto * detalle.PrecioUnitario;
                        detalle.ProductoNombre = producto.Nombre;
                    }

                    // Calcular total
                    venta.Total = venta.Detalles.Sum(d => d.Subtotal);

                    // Aplicar descuentos o recargos según forma de pago
                    switch (venta.FormaPago?.ToUpper())
                    {
                        case "EFECTIVO":
                            venta.Total *= 0.85M; // 15% descuento
                            break;
                        case "TARJETA DE CREDITO":
                            venta.Total *= 1.10M; // 10% recargo
                            break;
                    }

                    venta.FechaVenta = DateTime.Now;
                    _contexto.Ventas.Add(venta);
                    _contexto.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void EliminarVenta(int ventaId)
        {
            using (var transaction = _contexto.Database.BeginTransaction())
            {
                try
                {
                    var venta = _contexto.Ventas
                        .Include(v => v.Detalles)
                        .FirstOrDefault(v => v.VentaId == ventaId);

                    if (venta == null)
                        throw new Exception("Venta no encontrada");

                    // Restaurar stock
                    foreach (var detalle in venta.Detalles)
                    {
                        var producto = _contexto.Productos.Find(detalle.ProductoId);
                        if (producto != null)
                        {
                            producto.Stock += detalle.CantidadProducto;
                        }
                    }

                    _contexto.Ventas.Remove(venta);
                    _contexto.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public decimal CalcularTotalVentas(DateTime fecha)
        {
            return _contexto.Ventas
                .Where(v => v.FechaVenta.Date == fecha.Date)
                .Sum(v => v.Total);
        }

        public List<Venta> ObtenerVentasPorFecha(DateTime fecha)
        {
            return _contexto.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                .Where(v => v.FechaVenta.Date == fecha.Date)
                .ToList();
        }

        public List<Venta> ObtenerVentasPorCliente(int clienteId)
        {
            return _contexto.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                .Where(v => v.ClienteId == clienteId)
                .ToList();
        }
    }
}

