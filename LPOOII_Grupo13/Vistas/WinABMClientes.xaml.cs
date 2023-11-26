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

        public WinABMClientes()
        {
            InitializeComponent();

            listaClientes = TrabajarCliente.listarClientes(); // Obtener la colección
            lvwClientes.ItemsSource= listaClientes; // Asignar la colección al ListView
            DataContext = this; // Establecer el DataContext de la ventana
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

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

        private bool controlarCancelar()
        {

            if ((txtDniCliente.Text == "0" || txtDniCliente.Text == "") && txtApellido.Text == "" && txtNombre.Text == "" && txtTelefono.Text == "")
                return true;
            else
                return false;
        }

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

        private void limpiarForm()
        {
            txtIdCliModificar.Text = "";
            txtDniCliente.Text = "";
            txtApellido.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtDniCliente.Focus();
        }

        private void btnGuardarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (controlarDatos() == true)
            {
                cargarDatos();
                if (btnGuardarCliente.Content.ToString() == "GUARDAR")
                {
                    TrabajarCliente.insertar_Cliente(oCliente);
                    MessageBox.Show("El cliente " + txtApellido.Text + " con DNI " + txtDniCliente.Text + " se guardo correctamente");
                    txtDniCliente.Text = "0";

                    listaClientes = TrabajarCliente.listarClientes(); // Obtener la colección
                    lvwClientes.ItemsSource = listaClientes; // Asignar la colección al ListView
                }
                else
                {
                    TrabajarCliente.modificar_Cliente(oCliente);
                    MessageBox.Show("Cliente actualizado correctamente");

                    listaClientes = TrabajarCliente.listarClientes(); // Obtener la colección
                    lvwClientes.ItemsSource = listaClientes; // Asignar la colección al ListView

                    txtDniCliente.Text="0";
                    txtDniCliente.IsEnabled = true;
                    btnGuardarCliente.Content = "GUARDAR";
                    
                }
                limpiarForm();
            }
            else
            {
                MessageBox.Show("TODOS LOS CAMPOS DEBEN ESTAR CARGADOS", "Aviso", MessageBoxButton.OK);
            }
        }

        private bool controlarDatos()
        {
            if (txtDniCliente.Text == "" || txtApellido.Text == "" || txtNombre.Text == "" || txtTelefono.Text == "")
                return false;
            else
                return true;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            cargarTextBox();
            btnGuardarCliente.Content = "ACTUALIZAR";
            txtDniCliente.IsEnabled = false;
        }

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

                //Para datatable

                //DataRowView selectedRow = (DataRowView)lvwClientes.SelectedItem;
                //DataRow row = selectedRow.Row;

                //string id = row["Cli_Id"].ToString();
                //string dni = row["Cli_Dni"].ToString();
                //string apellido = row["Cli_Apellido"].ToString();
                //string nombre = row["Cli_Nombre"].ToString();
                //string tel = row["Cli_Telefono"].ToString(); 

                // Luego, puedes asignar el valor a tu TextBox u otro control
                //txtIdCliModificar.Text = id;
                //txtApellido.Text = apellido;
                //txtDniCliente.Text = dni;
                //txtNombre.Text = nombre;
                //txtTelefono.Text = tel;
            }
           
        }

        private void lvwClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Cliente clienteEli = (Cliente)lvwClientes.SelectedItem;
            MessageBoxResult result = MessageBox.Show("¿Desea ELIMINAR al cliente "+clienteEli.Cli_Apellido1+" "+clienteEli.Cli_Nombre1, "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                clienteEli.Cli_Estado1 = "Deshabilitado";
                TrabajarCliente.eliminar_Cliente(clienteEli);
                listaClientes = TrabajarCliente.listarClientes(); // Obtener la colección
                lvwClientes.ItemsSource = listaClientes; // Asignar la colección al ListView
            }

        }
    }
}
