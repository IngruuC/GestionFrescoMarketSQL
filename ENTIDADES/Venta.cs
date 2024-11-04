using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Venta
    {
        [Key]
        public int VentaId { get; set; }

        public int ClienteId { get; set; }

        public DateTime FechaVenta { get; set; }

        public decimal Total { get; set; }

        [StringLength(50)]
        public string FormaPago { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}
