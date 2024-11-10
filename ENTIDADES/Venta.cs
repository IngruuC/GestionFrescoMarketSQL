using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{

    public class Venta
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public string FormaPago { get; set; }
        public Cliente Cliente { get; set; }
        public List<DetalleVenta> Detalles { get; set; }

        public Venta()
        {
            Detalles = new List<DetalleVenta>();
            FechaVenta = DateTime.Now;
        }
    }
}
