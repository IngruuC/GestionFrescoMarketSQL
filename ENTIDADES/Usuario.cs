using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ENTIDADES
{

    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [Required]
        public string Rol { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? UltimoAcceso { get; set; }

        [Required]
        public int IntentosIngreso { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Grupo> Grupos { get; set; }




        public Usuario()
        {
            Estado = true;
            FechaCreacion = DateTime.Now;
            IntentosIngreso = 0;
            Grupos = new HashSet<Grupo>();
        }
    }
}
