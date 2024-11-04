using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Documento { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        public virtual ICollection<Venta> Ventas { get; set; }

        public override string ToString()
        {
            return $"{Nombre} {Apellido} - {Documento}";
        }
    }
}
