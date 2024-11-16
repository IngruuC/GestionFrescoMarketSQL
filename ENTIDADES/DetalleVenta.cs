using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{

    [Table("DetallesVenta")]
    public class DetalleVenta
    {
        [Key]
        public int Id { get; set; }
        public int? VentaId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public string ProductoNombre { get; set; }

        public virtual Venta Venta { get; set; }
        public virtual Producto Producto { get; set; }
    }
}

