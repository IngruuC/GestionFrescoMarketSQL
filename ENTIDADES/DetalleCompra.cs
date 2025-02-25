using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTIDADES
{
    [Table("DetallesCompra")]
    public class DetalleCompra
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("CompraId")] // Asegurar el nombre correcto
        public int CompraId { get; set; }

        [Required]
        [Column("ProductoId")] // Asegurar el nombre correcto
        public int ProductoId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Subtotal { get; set; }

        [Required]
        public string ProductoNombre { get; set; }


        [ForeignKey("CompraId")] // Vinculación explícita
        public virtual Compra Compra { get; set; }
        [ForeignKey("ProductoId")] // Vinculación explícita
        public virtual Producto Producto { get; set; }
    } 
}
