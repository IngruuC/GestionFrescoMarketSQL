using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Grupo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreGrupo { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }

        public virtual ICollection<Permiso> Permisos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }

        public Grupo()
        {
            Permisos = new HashSet<Permiso>();
            Usuarios = new HashSet<Usuario>();
        }
    }
}
