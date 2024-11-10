using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
  
    public class Usuario
    {
        
        
            public int Id { get; set; }
            public string NombreUsuario { get; set; }
            public string Contraseña { get; set; }
            public string Rol { get; set; }
        
    }
}
