using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

using ClaseBase;
namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinABMClientes.xaml
    /// </summary>
    public partial class WinABMClientes : Window
    {
        private Cliente oCliente = new Cliente();
        private ObservableCollection<Cliente> listaClientes;
        private ObservableCollection<Cliente> listaClientesBusqueda;

        public WinABMClientes()
        {
            InitializeComponent();
            cargarListClientes();
        }

        /// <summary>
        /// Metodo para cargar todos los clientes en el listview
        /// </summary>
        private void cargarListClientes()
        {
            listaClientes = TrabajarCliente.listarClientes();
            lvwClientes.ItemsSource = listaClientes;
            DataContext = this;
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
        /// Metodo para minimizar el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Boton para cerrar el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            if (controlarCancelar() == true)
            {
                MessageBoxResult result = MessageBox.Show("¿Desea volver al menú principal?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    WinPrincipal winPri = new WinPrincipal();
                    winPri.Show();
                    this.Close();
                }
                else
                    limpiarForm();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("¿Desea cancelar la carga al formulario y volver al menú principal?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    WinPrincipal winPri = new WinPrincipal();
                    winPri.Show();
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Metodo para controlar que los textbox no esten cargados
        /// para poder cerrar el formulario.
        /// </summary>
        /// <returns></returns>
        private bool controlarCancelar()
        {

            if ((txtDniCliente.Text == "0" || txtDniCliente.Text == "") && txtApellido.Text == "" && txtNombre.Text == "" && txtTelefono.Text == "")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Metodo para cargar los datos del textbox al objeto cliente.
        /// </summary>
        private void cargarDatos()
        {
            if (txtIdCliModificar.Text!="")
                oCliente.Cli_Id1 = Convert.ToInt32(txtIdCliModificar.Text);
            oCliente.Cli_ClienteDni1 = Convert.ToInt32(txtDniCliente.Text);
            oCliente.Cli_Apellido1 = txtApellido.Text;
            oCliente.Cli_Nombre1 = txtNombre.Text;
            oCliente.Cli_Telefono1 = txtTelefono.Text;
            oCliente.Cli_Estado1 = "Habilitado";
        }

        /// <summary>
        /// Metodo para limpiar el formulario.
        /// </summary>
        private void limpiarForm()
        {
            txtIdCliModificar.Text = "";
            txtDniCliente.Text = "";
            txtApellido.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtDniCliente.Focus();
        }

        /// <summary>
        /// Botón para dar de alta el Cliente en la base de datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (controlarDatos() == true)
            {
                if (TrabajarCliente.buscarCliente(Convert.ToInt32(txtDniCliente.Text)) == null)
                {
                    cargarDatos();
                    if (btnGuardarCliente.Content.ToString() == "GUARDAR")
                    {
                        TrabajarCliente.insertar_Cliente(oCliente);
                        MessageBox.Show("El cliente " + txtApellido.Text + " con DNI " + txtDniCliente.Text + " se guardo correctamente");
                        txtDniCliente.Text = "0";

                        listaClientes = TrabajarCliente.listarClientes();
                        lvwClientes.ItemsSource = listaClientes;
                    }
                    else
                    {
                        TrabajarCliente.modificar_Cliente(oCliente);
                        MessageBox.Show("Cliente actualizado correctamente");

                        listaClientes = TrabajarCliente.listarClientes();
                        lvwClientes.ItemsSource = listaClientes;

                        txtDniCliente.Text = "0";
                        txtDniCliente.IsEnabled = true;
                        btnGuardarCliente.Content = "GUARDAR";

                    }
                    limpiarForm();
                }
                else
                {
                    MessageBox.Show("EL CLIENTE CON EL DNI"+txtDniCliente.Text+" YA SE ENCUENTRA DADO DE ALTA", "Aviso", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("TODOS LOS CAMPOS DEBEN ESTAR CARGADOS", "Aviso", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Metodo para de controlar que el formulario este completo para el alta del Cliente.
        /// </summary>
        /// <returns></returns>
        private bool controlarDatos()
        {
            if (txtDniCliente.Text == "" || txtApellido.Text == "" || txtNombre.Text == "" || txtTelefono.Text == "")
                return false;
            else
                return true;
        }

        /// <summary>
        /// Botón para modificar un Cliente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            cargarTextBox();
            btnGuardarCliente.Content = "ACTUALIZAR";
            txtDniCliente.IsEnabled = false;
        }

        /// <summary>
        /// Metodo para cargar los datos del Cliente a los textbox para realizar la modificación.
        /// </summary>
        private void cargarTextBox()
        {
            if (lvwClientes.SelectedItem != null)
            {
                //Para observable collection
                Cliente clienteMod = (Cliente)lvwClientes.SelectedItem;

                txtIdCliModificar.Text = clienteMod.Cli_Id1.ToString();
                txtApellido.Text = clienteMod.Cli_Apellido1;
                txtNombre.Text = clienteMod.Cli_Nombre1;
                txtDniCliente.Text = clienteMod.Cli_ClienteDni1.ToString();
                txtTelefono.Text = clienteMod.Cli_Telefono1;
            }
           
        }

        private void lvwClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// Botón para eliminar un Cliente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Cliente clienteEli = (Cliente)lvwClientes.SelectedItem;
            MessageBoxResult result = MessageBox.Show("¿Desea ELIMINAR al cliente "+clienteEli.Cli_Apellido1+" "+clienteEli.Cli_Nombre1, "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                clienteEli.Cli_Estado1 = "Deshabilitado";
                TrabajarCliente.eliminar_Cliente(clienteEli);
                listaClientes = TrabajarCliente.listarClientes();
                lvwClientes.ItemsSource = listaClientes; 
            }

        }

        /// <summary>
        /// Metodo para cuando el textbox tenga el focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBusqueda_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBusqueda.Text == "Ingrese el DNI del Cliente a buscar...")
            {
                txtBusqueda.Text = string.Empty; // Limpiar el texto predeterminado
              
            }
        }

        /// <summary>
        /// Metodo para cuando el textbox pierda el focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBusqueda_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                txtBusqueda.Text = "Ingrese el DNI del Cliente a buscar..."; // Restaurar el texto predeterminado
                cargarListClientes();
            }
        }

        /// <summary>
        /// Botón para buscar los clientes por DNI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtBusqueda.Text == "" || txtBusqueda.Text == "Ingrese el DNI del Cliente a buscar...")
            {
                cargarListClientes();
            }
            else
            {
                listaClientesBusqueda = TrabajarCliente.busquedaClientes(Convert.ToInt32( txtBusqueda.Text));
                if (listaClientesBusqueda.Count > 0)
                {
                    lvwClientes.ItemsSource = listaClientesBusqueda;
                    DataContext = this;
                }
                else
                {
                    MessageBox.Show("No se encontro al Cliente con el DNI: " + txtBusqueda.Text);
                    cargarListClientes();
                }
            }


        }

    }
}
