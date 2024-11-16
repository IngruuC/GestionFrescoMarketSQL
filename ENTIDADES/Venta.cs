using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{

    [Table("Ventas")]
    public class Venta
    {
        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public string FormaPago { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<DetalleVenta> Detalles { get; set; }

        public Venta()
        {
            Detalles = new List<DetalleVenta>();
        }
    }
}
