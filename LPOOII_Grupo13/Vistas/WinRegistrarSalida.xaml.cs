using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using System.Collections.ObjectModel;


using ClaseBase;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinRegistrarSalida.xaml
    /// </summary>
    public partial class WinRegistrarSalida : Window
    {
        Ticket oTicket = new Ticket();
        Ticket ticketSalida = new Ticket();

        private ObservableCollection<Ticket> listarTickets;

        int codigoSectorCambiar;

        public WinRegistrarSalida()
        {
            InitializeComponent();

            cargarFechaYHora();

            cargarListaTickets();
        }

        /// <summary>
        /// Metodo para cargar la listas de ticket de entrada en el listview.
        /// </summary>
        private void cargarListaTickets()
        {
            listarTickets = TrabajarTicket.traerTicketConHoraSalNull();
            lvwTickets.ItemsSource = listarTickets;
            DataContext = this;
        }

        /// <summary>
        /// Metodo para cargar la fecha y hora actual del sistema.
        /// </summary>
        private void cargarFechaYHora() 
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yy");
            txtHora.Text = DateTime.Now.ToString("HH:mm");
        }

        /// <summary>
        /// Botón para cerrar el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            WinPrincipal winPri = new WinPrincipal();
            winPri.Show();
            this.Close();
        }

        /// <summary>
        /// Botón para minimizar el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            
        }

        /// <summary>
        /// Metodo para arrastrar el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        /// <summary>
        /// Botón para pasar los datos del ticket seleccionado para realizar el registro de salida del vehiculo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeleccionarTicket_Click(object sender, RoutedEventArgs e)
        {
            if (lvwTickets.SelectedItem != null)
            {
                Ticket ticket = (Ticket)lvwTickets.SelectedItem;
                ticketSalida = ticket; //guardar ticket para imprimir
                txtIdTicketSalida.Text = ticket.Tick_Id.ToString();
                txtNTicket.Text = ticket.Tick_TicketNro.ToString();
                txtFecYHoraEntrada.Text = ticket.Tick_FechaHoraEnt.ToString();
                txtApellido.Text = TrabajarCliente.buscarCliente(ticket.Tick_ClienteDni).Cli_Apellido1;
                txtPatente.Text = ticket.Tick_Patente;
                txtTipoVehiculo.Text = TrabajarTipoVehiculo.buscarTipoVehiculoXCodigo(ticket.Tick_TVCodigo).TypV_Descripcion;
                txtTarifaXHora.Text = ticket.Tick_Tarifa.ToString();
                calcularTotalYHora(ticket.Tick_FechaHoraEnt);

                codigoSectorCambiar = ticket.Tick_SectorCodigo;
            }
            else {
                MessageBox.Show("Debe seleccionar un ticket para poder realizar la salida del vehiculo");
            }
        }

        /// <summary>
        /// Metodo para calcular la cantidad de horas y minutos.
        /// </summary>
        /// <param name="fechaHoraEnt"></param>
        public void calcularTotalYHora(DateTime fechaHoraEnt)
        {
            //DateTime fechaHoraEntrada;
            //if (DateTime.TryParseExact(txtFecYHoraEntrada.Text, "d/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaHoraEntrada))
            //{
                // Continuar con el procesamiento
                DateTime ahora = DateTime.Now;
                //TimeSpan diferencia = ahora.Subtract(fechaHoraEntrada);
                TimeSpan diferencia = ahora.Subtract(fechaHoraEnt);

                int horas = diferencia.Hours;
                int minutos = diferencia.Minutes;

                txtDuracion.Text = horas.ToString() + "Hs - " + minutos.ToString() + "Min";

                if (horas > 0)
                {
                    if (minutos <= 10)
                    {
                        txtTotal.Text = (horas * Convert.ToDecimal(txtTarifaXHora.Text)).ToString();
                    }
                    else
                    {
                        txtTotal.Text = ((horas + 1) * Convert.ToDecimal(txtTarifaXHora.Text)).ToString();
                    }
                }
                else
                {
                    txtTotal.Text = txtTarifaXHora.Text;
                }
        }

        /// <summary>
        /// Botón para realizar el regitro de salida de un vehiculo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegistrarSalida_Click(object sender, RoutedEventArgs e)
        {
            if (txtNTicket.Text != "")
            {
                oTicket.Tick_Id =Convert.ToInt32( txtIdTicketSalida.Text);
                oTicket.Tick_Duracion = txtDuracion.Text;
                oTicket.Tick_Total = Convert.ToDecimal(txtTotal.Text);
                oTicket.Tick_FechaHoraSal = Convert.ToDateTime( DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                TrabajarTicket.registrarSalidaTicket(oTicket);
                cambiarEstadoSector(codigoSectorCambiar);
                MessageBox.Show("SE REGISTRO CORRECTAMENTE LA SALIDA DEL VEHICULO");

                cargarDatosTicket();
                Util.Util.setTicketSalida(ticketSalida);

                WinTicketRegistrarSalida winTicketSalida = new WinTicketRegistrarSalida();
                winTicketSalida.Topmost = true;
                winTicketSalida.Show();

                limpiarForm();

                cargarFechaYHora();
                cargarListaTickets();
            }
            else {
                MessageBox.Show("Debe seleccionar un ticket para poder realizar la salida del vehiculo");
            }
        }

        /// <summary>
        /// Metodo para cargar los datos faltantes al ticket.
        /// </summary>
        private void cargarDatosTicket()
        {
            ticketSalida.Tick_Id = Convert.ToInt32(txtIdTicketSalida.Text);
            ticketSalida.Tick_Duracion = txtDuracion.Text;
            ticketSalida.Tick_Total = Convert.ToDecimal(txtTotal.Text);
            ticketSalida.Tick_FechaHoraSal = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        /// <summary>
        /// Metodo para cambiar de estado un sector, de ocupado a disponible.
        /// </summary>
        /// <param name="codigoSector"></param>
        private void cambiarEstadoSector(int codigoSector)
        {
            Sector cambiarEstadoSector = new Sector();
            cambiarEstadoSector.Sec_Codigo = codigoSector;
            cambiarEstadoSector.Sec_Habilitado = "Disponible";
            TrabajarSectores.cambiarEstadoSector(cambiarEstadoSector);
        }

        /// <summary>
        /// Metodo para limpiar el formulario.
        /// </summary>
        private void limpiarForm()
        {
            txtNTicket.Text = "";
            txtFecYHoraEntrada.Text = "";
            txtFecha.Text = "";
            txtHora.Text = "";
            txtPatente.Text = "";
            txtApellido.Text = "";
            txtTipoVehiculo.Text = "";
            txtDuracion.Text = "";
            txtTarifaXHora.Text = "";
            txtTotal.Text = "";
        }
    }
}
