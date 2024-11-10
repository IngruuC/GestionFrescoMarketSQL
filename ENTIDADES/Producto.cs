using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{

    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoBarra { get; set; }
        public bool EsPerecedero { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public List<DetalleVenta> DetallesVenta { get; set; }

        public Producto()
        {
            DetallesVenta = new List<DetalleVenta>();
        }
    }
}
