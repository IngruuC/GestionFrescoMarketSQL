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
                    // Validar cliente
                    if (!contexto.Clientes.Any(c => c.Id == venta.ClienteId))
                        throw new Exception("Cliente no encontrado.");

                    // Validar y actualizar stock de productos
                    foreach (var detalle in venta.Detalles)
                    {
                        var producto = contexto.Productos.Find(detalle.ProductoId);
                        if (producto == null)
                            throw new Exception($"Producto no encontrado: {detalle.ProductoId}");

                        if (producto.Stock < detalle.Cantidad)
                            throw new Exception($"Stock insuficiente para el producto: {producto.Nombre}");

                        detalle.PrecioUnitario = producto.Precio;
                        detalle.Subtotal = detalle.PrecioUnitario * detalle.Cantidad;
                        producto.Stock -= detalle.Cantidad;
                    }

                    // Calcular total según forma de pago
                    decimal totalOriginal = venta.Detalles.Sum(d => d.Subtotal);
                    switch (venta.FormaPago)
                    {
                        case "Efectivo":
                            venta.Total = totalOriginal * 0.85m; // 15% descuento
                            break;
                        case "Tarjeta de Crédito":
                            venta.Total = totalOriginal * 1.10m; // 10% recargo
                            break;
                        default:
                            venta.Total = totalOriginal;
                            break;
                    }

                    contexto.Ventas.Add(venta);
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


