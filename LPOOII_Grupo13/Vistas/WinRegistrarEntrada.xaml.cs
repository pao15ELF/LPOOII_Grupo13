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
            cargarNumTicketYFecHora();

            cargarComboboxTipoVehiculo();
            cargarComboboxZonaSector();
        }

        /// <summary>
        /// Metodo para cargar combobox con los sectores disponibles.
        /// </summary>
        private void cargarComboboxZonaSector()
        {
            DataTable dt = TrabajarTicket.traerZonaSectorDisponible();
            if (dt != null)
            {
                cmbZonaSector.ItemsSource = dt.DefaultView;
                cmbZonaSector.DisplayMemberPath = "Sec_Descripcion";
                cmbZonaSector.SelectedValuePath = "Sec_Codigo";
            }
        }

        /// <summary>
        /// Metodo para cargar Numero de Ticket y la fecha actual al formulario.
        /// </summary>
        private void cargarNumTicketYFecHora()
        {
            int numTicket = TrabajarTicket.traerTicket().Rows.Count + 1;
            txtNTicket.Text = numTicket.ToString();

            txtFecha.Text = DateTime.Now.ToString("dd/MM/yy");
            txtHora.Text = DateTime.Now.ToString("HH:mm");
        }

        /// <summary>
        /// Metodo para cargar el combobox con los tipos de Vehiculos.
        /// </summary>
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
        /// Función para controlar que todos los datos sean cargados en el formulario para el alta.
        /// </summary>
        /// <returns></returns>
        public bool controlarTextBox() 
        {
            bool completo = false;
            if (!string.IsNullOrEmpty(txtPatente.Text) && cmbTipoVehiculo.SelectedItem != null && !string.IsNullOrEmpty(txtClienteDni.Text) && cmbZonaSector.SelectedItem !=null)
            {
                completo = true;
            }

            return completo;
        }

        /// <summary>
        /// Metodo para cargar los datos del formulario a un objeto Ticket.
        /// </summary>
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

        /// <summary>
        /// Metodo para cargar la fecha en formato "dd/MM/yy HH:mm"
        /// </summary>
        private void cargarFechaTicket()
        {
            string fecha = txtFecha.Text;
            string hora = txtHora.Text;

            string fechaHora = fecha + " " + hora;

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

        /// <summary>
        /// Metodo para buscar la tarifa por el codigo del Vehiculo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTipoVehiculo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipoVehiculo.SelectedItem != null)
            {
                DataTable tarifaVehiculo = TrabajarTicket.buscarTarifaVehiculo(Convert.ToInt32( cmbTipoVehiculo.SelectedValue.ToString()));
                if (tarifaVehiculo.Rows.Count > 0)
                {
                    txtTarifa.Text = tarifaVehiculo.Rows[0]["TV_Tarifa"].ToString();
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

        /// <summary>
        /// Metodo que carga los textbox con los datos del cliente selecionado de la lista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Metodo para cambiar el estado del sector en que se realiza un entrada.
        /// </summary>
        /// <param name="codigoSector"></param>
        private void cambiarEstadoSector(int codigoSector)
        {
            Sector cambiarEstadoSector = new Sector();
            cambiarEstadoSector.Sec_Codigo = codigoSector;
            cambiarEstadoSector.Sec_Habilitado= "Ocupado";
            TrabajarSectores.cambiarEstadoSector(cambiarEstadoSector);
        }

        /// <summary>
        /// Metodo para dar de alta el ticket de entrada y mostrar su ticket.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImprimirTicket_Click(object sender, RoutedEventArgs e)
        {
            if (controlarTextBox() == true)
            {
                cargarTicket();
                TrabajarTicket.registrarEntradaTicket(oTicket);
                Util.Util.setTicketEntrada(oTicket);
                cambiarEstadoSector(oTicket.Tick_SectorCodigo);
                MessageBox.Show("EL VEHICULO FUE REGISTRADO EXITOSAMENTE");
                WinTicketRegistrarEntrada winTicketEntrada = new WinTicketRegistrarEntrada();
                winTicketEntrada.Topmost = true;
                winTicketEntrada.Show();

                limpiarForm();

                cargarNumTicketYFecHora();
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos del formulario para poder realizar el registro de entrada");
            }  
        }

        /// <summary>
        /// Metodo para limpiar el formulario.
        /// </summary>
        private void limpiarForm()
        {
            txtClienteDni.Text = "";
            txtApellido.Text = "";
            txtPatente.Text = ""; 
            cmbTipoVehiculo.SelectedItem = null;
            cmbZonaSector.SelectedItem = null;
            txtTarifa.Text = "";
        }
    }
}
