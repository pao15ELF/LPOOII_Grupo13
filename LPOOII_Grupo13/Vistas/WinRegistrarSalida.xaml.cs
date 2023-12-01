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

        private void cargarListaTickets()
        {
            listarTickets = TrabajarTicket.traerTicketConHoraSalNull();
            lvwTickets.ItemsSource = listarTickets;
            DataContext = this;
        }

        private void cargarFechaYHora() 
        {
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yy");
            txtHora.Text = DateTime.Now.ToString("HH:mm");
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            WinPrincipal winPri = new WinPrincipal();
            winPri.Show();
            this.Close();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnSeleccionarTicket_Click(object sender, RoutedEventArgs e)
        {
            if (lvwTickets.SelectedItem != null)
            {
                Ticket ticket = (Ticket)lvwTickets.SelectedItem;
                ticketSalida = ticket; //guardar ticket para imprimir
                txtIdTicketSalida.Text = ticket.Tick_Id.ToString();
                txtNTicket.Text = ticket.Tick_TicketNro.ToString();
                txtFecYHoraEntrada.Text = ticket.Tick_FechaHoraEnt.ToString();
                //txtClienteDni.Text = ticket.Tick_ClienteDni.ToString();
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

        public void calcularTotalYHora(DateTime fechaHoraEnt)
        {
            
            // Obtiene la fecha y hora actual
            //DateTime ahora = DateTime.Now;

            // Parsea la fecha y hora de entrada a un objeto DateTime
            //DateTime fechaHoraEntrada = DateTime.ParseExact(txtFecYHoraEntrada.Text, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture);
            //DateTime fechaHoraEntrada = DateTime.ParseExact(txtFecYHoraEntrada.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            // Obtiene la diferencia
            //TimeSpan diferencia = ahora.Subtract(fechaHoraEntrada);

            // Accede a las horas y minutos de la diferencia
            //int horas = diferencia.Hours;
            //int minutos = diferencia.Minutes;

            //txtTiempoHsMin.Text= horas.ToString()+"Hs - "+minutos.ToString()+"Min";

            // Ahora puedes utilizar las variables horas y minutos como necesites
            //if(horas >0)
            //{
            //    txtTotal.Text = (horas * Convert.ToDecimal(txtTarifaXHora.Text)).ToString();
            //}

            DateTime fechaHoraEntrada;
            if (DateTime.TryParseExact(txtFecYHoraEntrada.Text, "d/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaHoraEntrada))
            {
                // Continuar con el procesamiento
                DateTime ahora = DateTime.Now;
                TimeSpan diferencia = ahora.Subtract(fechaHoraEntrada);

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
            else
            {
                MessageBox.Show("La fecha y hora ingresadas no tienen el formato esperado."+ fechaHoraEnt.ToString());
            }
        }

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

        private void cargarDatosTicket()
        {
            ticketSalida.Tick_Id = Convert.ToInt32(txtIdTicketSalida.Text);
            ticketSalida.Tick_Duracion = txtDuracion.Text;
            ticketSalida.Tick_Total = Convert.ToDecimal(txtTotal.Text);
            ticketSalida.Tick_FechaHoraSal = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }
        private void cambiarEstadoSector(int codigoSector)
        {
            Sector cambiarEstadoSector = new Sector();
            cambiarEstadoSector.Sec_Codigo = codigoSector;
            cambiarEstadoSector.Sec_Habilitado = "Disponible";
            //MessageBox.Show("id: " + cambiarEstadoSector.Sec_Codigo + "estado: " + cambiarEstadoSector.Sec_Habilitado);
            TrabajarSectores.cambiarEstadoSector(cambiarEstadoSector);
        }

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
