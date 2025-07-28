using ENTIDADES.SEGURIDAD;
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




        // NUEVAS PROPIEDADES AGREGADAS
        [StringLength(100)]
        public string NombreyApellido { get; set; }

        public string Clave { get; set; } // Alias para Contraseña (compatibilidad)

        // Relación con EstadoUsuario
        public int? EstadoUsuarioId { get; set; }
        [ForeignKey("EstadoUsuarioId")]
        public virtual EstadoUsuario EstadoUsuario { get; set; }

        // Relaciones existentes


        // NUEVA: Perfil para manejar tanto grupos como acciones
        [NotMapped]
        public List<object> Perfil { get; set; }

        public Usuario()
        {
            Estado = true;
            FechaCreacion = DateTime.Now;
            IntentosIngreso = 0;
            Grupos = new HashSet<Grupo>();
            Perfil = new List<object>();
        }

        // NUEVOS MÉTODOS para manejar permisos
        public void AgregarPermiso(object permiso)
        {
            if (Perfil == null) Perfil = new List<object>();

            if (!Perfil.Contains(permiso))
            {
                Perfil.Add(permiso);

                // Si es un grupo, agregarlo también a la colección Grupos
                if (permiso is Grupo grupo)
                {
                    if (!Grupos.Contains(grupo))
                        Grupos.Add(grupo);
                }
            }
        }

        public void EliminarPermiso(object permiso)
        {
            if (Perfil != null)
            {
                Perfil.Remove(permiso);

                // Si es un grupo, removerlo también de la colección Grupos
                if (permiso is Grupo grupo)
                {
                    Grupos.Remove(grupo);
                }
            }
        }

        // Propiedad de compatibilidad
        [NotMapped]
        public string Clave_Alias => Contraseña;
    

}
}
