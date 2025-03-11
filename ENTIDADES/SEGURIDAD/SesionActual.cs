﻿using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES.SEGURIDAD
{
    public static class SesionActual
    {
        public static Usuario Usuario { get; set; }

        public static bool TienePermiso(string permiso)
        {
            if (Usuario == null || Usuario.Grupos == null) return false;
            return Usuario.Grupos.Any(g => g.Permisos != null && g.Permisos.Any(p => p.NombrePermiso == permiso));
        }

        public static bool EsAdministrador()
        {
            if (Usuario == null || Usuario.Grupos == null) return false;
            return Usuario.Grupos.Any(g => g.NombreGrupo.ToUpper() == "ADMINISTRADOR");
        }

        public static bool EsProveedor()
        {
            if (Usuario == null || Usuario.Grupos == null) return false;
            return Usuario.Grupos.Any(g => g.NombreGrupo.ToUpper() == "PROVEEDOR");
        }

        public static bool EsCliente()
        {
            if (Usuario == null || Usuario.Grupos == null) return false;
            return Usuario.Grupos.Any(g => g.NombreGrupo.ToUpper() == "CLIENTE");
        }

        public static void CerrarSesion()
        {
            Usuario = null;
        }
    }
}
