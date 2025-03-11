using ENTIDADES;
using MODELO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTIDADES.SEGURIDAD;

namespace CONTROLADORA
{
 public class ControladoraAuditoria
    {
        private Contexto contexto;

        public ControladoraAuditoria()
        {
            contexto = new Contexto();
        }

        // Método para obtener historial de sesiones
        public List<AuditoriaSesion> ObtenerHistorialSesiones(
    int? usuarioId = null,
    DateTime? fechaDesde = null,
    DateTime? fechaHasta = null)
        {
            try
            {
                // Debug: Imprimir parámetros de búsqueda
                Console.WriteLine($"Buscando historial de sesiones - UsuarioId: {usuarioId}, Desde: {fechaDesde}, Hasta: {fechaHasta}");

                var query = contexto.AuditoriasSesion.AsQueryable();

                if (usuarioId.HasValue)
                {
                    query = query.Where(a => a.UsuarioId == usuarioId.Value);
                }

                if (fechaDesde.HasValue)
                {
                    query = query.Where(a => a.FechaIngreso >= fechaDesde.Value);
                }

                if (fechaHasta.HasValue)
                {
                    query = query.Where(a => a.FechaIngreso <= fechaHasta.Value);
                }

                var resultados = query.OrderByDescending(a => a.FechaIngreso).ToList();

                // Debug: Imprimir número de resultados
                Console.WriteLine($"Resultados encontrados: {resultados.Count}");

                return resultados;
            }
            catch (Exception ex)
            {
                // Logging de errores más detallado
                Console.WriteLine($"Error al obtener historial de sesiones: {ex.Message}");
                Console.WriteLine($"Detalles internos: {ex.InnerException?.Message}");
                return new List<AuditoriaSesion>();
            }
        }
        public List<AuditoriaSesion> ObtenerAuditoriasPorUsuario(int usuarioId)
        {
            try
            {
                return contexto.AuditoriasSesion
                    .Where(a => a.UsuarioId == usuarioId)
                    .OrderByDescending(a => a.FechaIngreso)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener auditorías por usuario: {ex.Message}");
                return new List<AuditoriaSesion>();
            }
        }

        public void RegistrarInicioSesion(Usuario usuario, string direccionIP)
        {
            if (usuario == null)
            {
                Console.WriteLine("Intento de registrar inicio de sesión con usuario nulo");
                return;
            }

            try
            {
                using (var contextoLocal = new Contexto())
                {
                    var auditoria = new AuditoriaSesion
                    {
                        UsuarioId = usuario.Id,
                        NombreUsuario = usuario.NombreUsuario,
                        FechaIngreso = DateTime.Now,
                        DireccionIP = direccionIP,
                        Dispositivo = Environment.MachineName,
                        TipoSesion = usuario.Rol,
                        SesionActiva = true
                    };

                    // Debug: Imprimir detalles de la auditoría
                    Console.WriteLine($"Registrando sesión: Usuario={auditoria.NombreUsuario}, IP={auditoria.DireccionIP}, Dispositivo={auditoria.Dispositivo}");

                    // Guardar en base de datos
                    contextoLocal.AuditoriasSesion.Add(auditoria);
                    contextoLocal.SaveChanges();

                    Console.WriteLine("Sesión registrada exitosamente");
                }
            }
            catch (Exception ex)
            {
                // Logging de errores más detallado
                Console.WriteLine($"Error al registrar inicio de sesión: {ex.Message}");
                Console.WriteLine($"Detalles internos: {ex.InnerException?.Message}");
                throw; // Re-lanzar para que se pueda ver el error completo
            }
        }

        // Método para cerrar sesión
        public void RegistrarCierreSesion(int usuarioId)
        {
            try
            {
                var sesionActiva = contexto.AuditoriasSesion
                    .FirstOrDefault(a => a.UsuarioId == usuarioId && a.SesionActiva);

                if (sesionActiva != null)
                {
                    sesionActiva.FechaSalida = DateTime.Now;
                    sesionActiva.SesionActiva = false;
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cerrar sesión: {ex.Message}");
            }
        }

    }

    
}
