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
        private static ControladoraVenta instancia;
        private Contexto contexto;

        private ControladoraVenta()
        {
            contexto = new Contexto();
        }

        public static ControladoraVenta ObtenerInstancia()
        {
            if (instancia == null)
                instancia = new ControladoraVenta();
            return instancia;
        }

        public void RealizarVenta(Venta venta)
        {
            using (var transaction = contexto.Database.BeginTransaction())
            {
                try
                {
                    // Validar y obtener cliente
                    var cliente = contexto.Clientes.Find(venta.ClienteId);
                    if (cliente == null)
                        throw new Exception("Cliente no encontrado.");

                    // Crear una nueva venta
                    var nuevaVenta = new Venta
                    {
                        ClienteId = venta.ClienteId,
                        FechaVenta = DateTime.Now,
                        FormaPago = venta.FormaPago
                    };

                    // Procesar cada detalle
                    decimal totalOriginal = 0;
                    foreach (var detalle in venta.Detalles)
                    {
                        var producto = contexto.Productos.Find(detalle.ProductoId);
                        if (producto == null)
                            throw new Exception($"Producto no encontrado: {detalle.ProductoId}");

                        if (producto.Stock < detalle.Cantidad)
                            throw new Exception($"Stock insuficiente para el producto: {producto.Nombre}");

                        var nuevoDetalle = new DetalleVenta
                        {
                            ProductoId = producto.Id,
                            Cantidad = detalle.Cantidad,
                            PrecioUnitario = producto.Precio,
                            ProductoNombre = producto.Nombre,
                            Subtotal = producto.Precio * detalle.Cantidad
                        };

                        totalOriginal += nuevoDetalle.Subtotal;
                        nuevaVenta.Detalles.Add(nuevoDetalle);

                        // Actualizar stock
                        producto.Stock -= detalle.Cantidad;
                        contexto.Entry(producto).State = EntityState.Modified;
                    }

                    // Calcular total con descuentos/recargos
                    switch (nuevaVenta.FormaPago)
                    {
                        case "Efectivo":
                            nuevaVenta.Total = totalOriginal * 0.85m;
                            break;
                        case "Tarjeta de Crédito":
                            nuevaVenta.Total = totalOriginal * 1.10m;
                            break;
                        default:
                            nuevaVenta.Total = totalOriginal;
                            break;
                    }

                    // Guardar la venta
                    contexto.Ventas.Add(nuevaVenta);
                    contexto.SaveChanges();
                    transaction.Commit();

                    // Actualizar el ID de la venta original
                    venta.Id = nuevaVenta.Id;
                    venta.Total = nuevaVenta.Total;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Error al realizar la venta: {ex.Message}");
                }
            }
        }

        public List<Venta> ObtenerVentas()
        {
            return contexto.Ventas
                .Include("Cliente")
                .Include("Detalles")
                .Include("Detalles.Producto")
                .ToList();
        }

        public Venta ObtenerVentaPorId(int id)
        {
            return contexto.Ventas
                .Include("Cliente")
                .Include("Detalles")
                .Include("Detalles.Producto")
                .FirstOrDefault(v => v.Id == id);
        }

        public List<Venta> ObtenerVentasPorFecha(DateTime inicio, DateTime fin)
        {
            return contexto.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                .Where(v => v.FechaVenta >= inicio && v.FechaVenta <= fin)
                .OrderBy(v => v.FechaVenta)
                .ToList();
        }

        public List<object> ObtenerProductosMasVendidos(DateTime inicio, DateTime fin, int top = 10)
        {
            return contexto.DetallesVenta
                .Where(d => d.Venta.FechaVenta >= inicio && d.Venta.FechaVenta <= fin)
                .GroupBy(d => new { d.ProductoId, d.ProductoNombre })
                .Select(g => new
                {
                    Producto = g.Key.ProductoNombre,
                    CantidadVendida = g.Sum(d => d.Cantidad),
                    TotalVendido = g.Sum(d => d.Subtotal)
                })
                .OrderByDescending(x => x.CantidadVendida)
                .Take(top)
                .ToList<object>();
        }

        public List<object> ObtenerVentasPorFormaPago(DateTime inicio, DateTime fin)
        {
            return contexto.Ventas
                .Where(v => v.FechaVenta >= inicio && v.FechaVenta <= fin)
                .GroupBy(v => v.FormaPago)
                .Select(g => new
                {
                    FormaPago = g.Key,
                    TotalVentas = g.Count(),
                    MontoTotal = g.Sum(v => v.Total)
                })
                .OrderByDescending(x => x.MontoTotal)
                .ToList<object>();
        }

        public void EliminarVenta(int id)
        {
            using (var transaction = contexto.Database.BeginTransaction())
            {
                try
                {
                    var venta = contexto.Ventas
                        .Include("Detalles")
                        .FirstOrDefault(v => v.Id == id);

                    if (venta == null)
                        throw new Exception("Venta no encontrada.");

                    // Restaurar stock
                    foreach (var detalle in venta.Detalles)
                    {
                        var producto = contexto.Productos.Find(detalle.ProductoId);
                        if (producto != null)
                            producto.Stock += detalle.Cantidad;
                    }

                    contexto.Ventas.Remove(venta);
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
    }
}


