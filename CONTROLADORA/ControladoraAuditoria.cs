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

        //  Método para obtener TODAS las sesiones (sin filtros)
        public List<AuditoriaSesion> ObtenerTodasLasSesiones()
        {
            try
            {
                Console.WriteLine("Obteniendo TODAS las sesiones de TODOS los usuarios");

                var resultados = contexto.AuditoriasSesion
                    .OrderByDescending(a => a.FechaIngreso)
                    .ToList();

                Console.WriteLine($"Total de sesiones encontradas: {resultados.Count}");

                // Debug: Mostrar usuarios únicos
                var usuariosUnicos = resultados.Select(r => r.NombreUsuario).Distinct().ToList();
                Console.WriteLine($"Usuarios en el historial: {string.Join(", ", usuariosUnicos)}");

                return resultados;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todas las sesiones: {ex.Message}");
                Console.WriteLine($"Detalles internos: {ex.InnerException?.Message}");
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
                Console.WriteLine($"=== INICIANDO CIERRE DE SESIÓN ===");
                Console.WriteLine($"Usuario ID: {usuarioId}");
                Console.WriteLine($"Fecha actual: {DateTime.Now}");

                // Buscar la sesión activa más reciente del usuario
                var sesionActiva = contexto.AuditoriasSesion
                    .Where(a => a.UsuarioId == usuarioId && a.SesionActiva)
                    .OrderByDescending(a => a.FechaIngreso)
                    .FirstOrDefault();

                if (sesionActiva != null)
                {
                    Console.WriteLine($"Sesión activa encontrada:");
                    Console.WriteLine($"  - ID Auditoría: {sesionActiva.Id}");
                    Console.WriteLine($"  - Usuario: {sesionActiva.NombreUsuario}");
                    Console.WriteLine($"  - Fecha Ingreso: {sesionActiva.FechaIngreso}");
                    Console.WriteLine($"  - Sesión Activa: {sesionActiva.SesionActiva}");

                    // Registrar la fecha de salida
                    sesionActiva.FechaSalida = DateTime.Now;
                    sesionActiva.SesionActiva = false;

                    Console.WriteLine($"  - Nueva Fecha Salida: {sesionActiva.FechaSalida}");
                    Console.WriteLine($"  - Nueva Sesión Activa: {sesionActiva.SesionActiva}");

                    // Guardar cambios
                    int cambiosGuardados = contexto.SaveChanges();
                    Console.WriteLine($"Cambios guardados en BD: {cambiosGuardados}");

                    if (cambiosGuardados > 0)
                    {
                        Console.WriteLine("✅ CIERRE DE SESIÓN REGISTRADO EXITOSAMENTE");
                    }
                    else
                    {
                        Console.WriteLine("⚠️ No se guardaron cambios en la BD");
                    }
                }
                else
                {
                    Console.WriteLine("❌ NO SE ENCONTRÓ SESIÓN ACTIVA");

                    // Debug: Mostrar todas las sesiones del usuario
                    var todasSesiones = contexto.AuditoriasSesion
                        .Where(a => a.UsuarioId == usuarioId)
                        .OrderByDescending(a => a.FechaIngreso)
                        .Take(3)
                        .ToList();

                    Console.WriteLine($"Últimas sesiones del usuario {usuarioId}:");
                    foreach (var s in todasSesiones)
                    {
                        Console.WriteLine($"  - ID: {s.Id}, Ingreso: {s.FechaIngreso}, Salida: {s.FechaSalida}, Activa: {s.SesionActiva}");
                    }
                }

                Console.WriteLine($"=== FIN CIERRE DE SESIÓN ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR al cerrar sesión: {ex.Message}");
                Console.WriteLine($"Detalles: {ex.InnerException?.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }

    }

    
}
