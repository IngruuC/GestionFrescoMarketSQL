using CONTROLADORA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VISTA
{
    public partial class FormAuditoriaSesion : Form
    {
        private ControladoraAuditoria controladoraAuditoria;
        private ControladoraUsuario controladoraUsuario;

        public FormAuditoriaSesion()
        {
            InitializeComponent();
            // Inicializar las controladoras
            controladoraAuditoria = new ControladoraAuditoria();
            controladoraUsuario = ControladoraUsuario.ObtenerInstancia();
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            // Configurar ComboBox de Usuarios
            cboUsuarios.DataSource = null;
            cboUsuarios.DataSource = controladoraUsuario.ObtenerUsuarios();
            cboUsuarios.DisplayMember = "NombreUsuario";
            cboUsuarios.ValueMember = "Id";
            cboUsuarios.SelectedIndex = -1;

            // Configurar rango de fechas
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;

            // Configurar DataGridView
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvAuditorias.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Usuario",
                DataPropertyName = "NombreUsuario"
            });
            dgvAuditorias.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha Ingreso",
                DataPropertyName = "FechaIngreso",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });
            // Más columnas...
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //*try
          //{
                // Debug: Imprimir valores seleccionados
      //        Console.WriteLine($"Usuario seleccionado: {cboUsuarios.SelectedValue}");
 //*            Console.WriteLine($"Fecha desde: {dtpDesde.Value}");
    //       Console.WriteLine($"Fecha hasta: {dtpHasta.Value}");
//
          //    int? usuarioId = cboUsuarios.SelectedIndex != -1
         //         ? (int?)cboUsuarios.SelectedValue
         //         : null;

       //       var sesiones = controladoraAuditoria.ObtenerHistorialSesiones(
     //             usuarioId,
   //               dtpDesde.Value,
 //               dtpHasta.Value
            //  );

                // Imprimir los resultados en la consola
                //      foreach (var sesion in sesiones)
              //{
                    //         Console.WriteLine($"Sesión - ID: {sesion.Id}, Usuario: {sesion.NombreUsuario}, Fecha Ingreso: {sesion.FechaIngreso}");
                    //      }

                    //   if (sesiones == null || sesiones.Count == 0)
                    //     {
                    //       MessageBox.Show("No se encontraron registros de sesión para los criterios seleccionados.",
                    //         "Sin Resultados",
                    //           MessageBoxButtons.OK,
                    //             MessageBoxIcon.Information);
                    //
                    //       dgvAuditorias.DataSource = null;
                    //     return;
                    //

                    //              dgvAuditorias.DataSource = sesiones;
                    //        }
                    //      catch (Exception ex)
                    {
                        // Logging de errores más detallado
                        //       Console.WriteLine($"Error en búsqueda: {ex.Message}");
                        //     Console.WriteLine($"Detalles internos: {ex.InnerException?.Message}");
                        //
                        //   MessageBox.Show($"Error al buscar registros de sesión: {ex.Message}",
                        //     "Error",
                        //    MessageBoxButtons.OK,
                        //   MessageBoxIcon.Error);
                    }

                }

                private void btnLimpiar_Click(object sender, EventArgs e)
                {
                    // Restablecer controles
                    cboUsuarios.SelectedIndex = -1;
                    dtpDesde.Value = DateTime.Today.AddMonths(-1);
                    dtpHasta.Value = DateTime.Today;

                    // Limpiar DataGridView
                    dgvAuditorias.DataSource = null;

                }
            }
        }
    
