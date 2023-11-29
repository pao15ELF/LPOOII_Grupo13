using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;


using ClaseBase;
namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinRegistrarEntrada.xaml
    /// </summary>
    public partial class WinRegistrarEntrada : Window
    {
        Ticket oTicket = new Ticket();

        public WinRegistrarEntrada()
        {
            InitializeComponent();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yy");
            txtHora.Text = DateTime.Now.ToString("HH:mm");
            
            int numTicket=TrabajarTicket.traerTicket().Rows.Count+1;
            txtNTicket.Text = numTicket.ToString();

            cargarComboboxTipoVehiculo();
            cargarComboboxZonaSector();
        }

        private void cargarComboboxZonaSector()
        {
            DataTable dt = TrabajarTicket.traerZonaSectorDisponible();
            //MessageBox.Show("cantidad de datos: " + dt.Rows.Count);
            if (dt != null)
            {
                cmbZonaSector.ItemsSource = dt.DefaultView;
                cmbZonaSector.DisplayMemberPath = "Sec_Descripcion";
                cmbZonaSector.SelectedValuePath = "Sec_Codigo";
            }
            //cmbZonaSector.SelectionChanged += cmbZonaSector.SelectionChanged;
        }

        private void cargarComboboxTipoVehiculo()
        {
            DataTable dt = TrabajarTicket.traerTipoVehiculoCombobox();
            if (dt != null)
            {
                cmbTipoVehiculo.ItemsSource = dt.DefaultView;
                cmbTipoVehiculo.DisplayMemberPath = "TV_Descripcion";
                cmbTipoVehiculo.SelectedValuePath = "TV_Codigo";
            }
            cmbTipoVehiculo.SelectionChanged += cmbTipoVehiculo_SelectionChanged;
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

        public bool controlarTextBox() 
        {
            bool completo = false;
            if (!string.IsNullOrEmpty(txtPatente.Text) && cmbTipoVehiculo.SelectedItem != null && !string.IsNullOrEmpty(txtClienteDni.Text) && cmbZonaSector.SelectedItem !=null)
            {
                completo = true;
            }

            return completo;
        }

        public void cargarTicket() 
        {
            oTicket.Tick_TicketNro = Convert.ToInt32(txtNTicket.Text);
            oTicket.Tick_ClienteDni = Convert.ToInt32(txtClienteDni.Text);
            oTicket.Tick_Patente = txtPatente.Text.ToUpper();
            oTicket.Tick_Tarifa = Convert.ToDecimal(txtTarifa.Text);
            oTicket.Tick_TVCodigo = Convert.ToInt32(cmbTipoVehiculo.SelectedValue.ToString());
            oTicket.Tick_SectorCodigo = Convert.ToInt32(cmbZonaSector.SelectedValue.ToString());
            cargarFechaTicket();
        }

        private void cargarFechaTicket()
        {
            string fecha = txtFecha.Text;
            string hora = txtHora.Text;

            // Concatena la fecha y la hora para crear un formato válido
            string fechaHora = fecha + " " + hora;

            // Formato esperado: dd/MM/yy HH:mm
            DateTime fechaHoraEntrada;
            if (DateTime.TryParseExact(fechaHora, "dd/MM/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaHoraEntrada))
            {
                oTicket.Tick_FechaHoraEnt = fechaHoraEntrada;
            }
            else
            {
                // Manejar caso de error de conversión
                // Por ejemplo, mostrar un mensaje o asignar un valor por defecto
            }
        }

        private void cmbTipoVehiculo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipoVehiculo.SelectedItem != null)
            {
    
                DataTable tarifaVehiculo = TrabajarTicket.buscarTarifaVehiculo(Convert.ToInt32( cmbTipoVehiculo.SelectedValue.ToString()));
                if (tarifaVehiculo.Rows.Count > 0)
                {
                    txtTarifa.Text = tarifaVehiculo.Rows[0]["TV_Tarifa"].ToString();
                    //string tarifaSeleccionada = cmbTipoVehiculo.SelectedValue.ToString();
                    //txtTarifa.Text = tarifaSeleccionada;
                }
                else
                {
                    MessageBox.Show("No se encontro vehiculo");
                }

                
            }
        }

        private void lvwRegistro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSeleccionarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (lvwClientes.SelectedItem != null)
            {
                //Para observable collection
                Cliente clienteMod = (Cliente)lvwClientes.SelectedItem;

                txtApellido.Text = clienteMod.Cli_Apellido1;
                txtClienteDni.Text = clienteMod.Cli_ClienteDni1.ToString();
            }
        }

        private void cambiarEstadoSector(int codigoSector)
        {
            Sector cambiarEstadoSector = new Sector();
            cambiarEstadoSector.Sec_Codigo = codigoSector;
            cambiarEstadoSector.Sec_Habilitado= "Ocupado";
            //MessageBox.Show("id: " + cambiarEstadoSector.Sec_Codigo + "estado: " + cambiarEstadoSector.Sec_Habilitado);
            TrabajarSectores.cambiarEstadoSector(cambiarEstadoSector);
        }

        private void btnImprimirTicket_Click(object sender, RoutedEventArgs e)
        {
            if (controlarTextBox() == true)
            {
                cargarTicket();
                TrabajarTicket.registrarEntradaTicket(oTicket);
                cambiarEstadoSector(oTicket.Tick_SectorCodigo);
                MessageBox.Show("MOSTRAR TICKET");
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos del formulario para poder realizar el registro de entrada");
            }  
        }
    }
}
