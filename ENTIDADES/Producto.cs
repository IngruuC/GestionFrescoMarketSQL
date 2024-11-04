using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(13)]
        public string CodigoBarra { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public bool EsPerecedero { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; }
    }
}
