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
using System.Collections.ObjectModel;

using ClaseBase;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinABMVehiculos.xaml
    /// </summary>
    public partial class WinABMVehiculos : Window
    {
        private TipoVehiculo oTipoVehiculo = new TipoVehiculo();
        private ObservableCollection<TipoVehiculo> listaTipoVehiculos;
        //ObservableCollection<TipoVehiculo> listaTipoVehiculos = new ObservableCollection<TipoVehiculo>();

        public WinABMVehiculos()
        {
            InitializeComponent();
            listaTipoVehiculos = TrabajarTipoVehiculo.listarTipoVehiculo(); // Obtener la colección
            lvwTipoVehiculos.ItemsSource = listaTipoVehiculos; // Asignar la colección al ListView
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
            if (txtCodVehiculo.Text == "" && txtDescripcion.Text == "" && txtTarifa.Text == "")
                return true;
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void cargarDatos()
        {
            if (txtIdVehiculoMod.Text != "")
                oTipoVehiculo.TypV_Id = Convert.ToInt32(txtIdVehiculoMod.Text);

            oTipoVehiculo.TypV_TVCodigo = Convert.ToInt32(txtCodVehiculo.Text);
            oTipoVehiculo.TypV_Descripcion = txtDescripcion.Text;
            oTipoVehiculo.TypV_Tarifa = Convert.ToInt32(txtTarifa.Text);
            oTipoVehiculo.TypV_Estado = "Habilitado";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            if (controlarDatos() == true)
            {
                cargarDatos();

                if (btnGuardarVehiculo.Content.ToString() == "GUARDAR")
                {
                    TrabajarTipoVehiculo.insertar_TipoVehiculo(oTipoVehiculo);

                    // Actualizar la lista después de insertar un nuevo elemento
                    listaTipoVehiculos = TrabajarTipoVehiculo.listarTipoVehiculo();
                    lvwTipoVehiculos.ItemsSource = listaTipoVehiculos;

                    MessageBox.Show("El Vehiculo con el codigo " + txtCodVehiculo.Text + " con la descripcion: " + txtDescripcion.Text + " se guardo correctamente");
                }
                else
                {
                    TrabajarTipoVehiculo.modificar_TipoVehiculo(oTipoVehiculo);
                    // Actualizar la lista después de insertar un nuevo elemento
                    listaTipoVehiculos = TrabajarTipoVehiculo.listarTipoVehiculo();
                    lvwTipoVehiculos.ItemsSource = listaTipoVehiculos;

                    MessageBox.Show("El Vehiculo con el codigo " + txtCodVehiculo.Text + " se modificar correctamente");
                    btnGuardarVehiculo.Content = "GUARDAR";
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
            if(txtCodVehiculo.Text=="" || txtDescripcion.Text=="" || txtTarifa.Text=="")
                return false;
            else
                return true;
        }

        private void limpiarForm()
        {
            txtIdVehiculoMod.Text = "";
            txtCodVehiculo.Text = "";
            txtDescripcion.Text = "";
            txtTarifa.Text = "";
            txtCodVehiculo.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DataContext = FindResource("list_TipoVehiculo");
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            cargarTextBox();
            btnGuardarVehiculo.Content = "ACTUALIZAR";
        }

        public void cargarTextBox()
        {
            if (lvwTipoVehiculos.SelectedItem != null)
            {
                TipoVehiculo tipoVehiculoMod = (TipoVehiculo)lvwTipoVehiculos.SelectedItem;

                txtIdVehiculoMod.Text= tipoVehiculoMod.TypV_Id.ToString();
                txtCodVehiculo.Text = tipoVehiculoMod.TypV_TVCodigo.ToString();
                txtDescripcion.Text = tipoVehiculoMod.TypV_Descripcion.ToString();
                txtTarifa.Text = tipoVehiculoMod.TypV_Tarifa.ToString();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            TipoVehiculo tipoVehiculoElim = (TipoVehiculo)lvwTipoVehiculos.SelectedItem;

            MessageBoxResult result = MessageBox.Show("¿Desea ELIMINAR el vehiculo " + tipoVehiculoElim.TypV_Descripcion + " con codigo "+tipoVehiculoElim.TypV_TVCodigo , "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                TrabajarTipoVehiculo.eliminar_TipoVehiculo(tipoVehiculoElim);
                // Actualizar la lista 
                listaTipoVehiculos = TrabajarTipoVehiculo.listarTipoVehiculo();
                lvwTipoVehiculos.ItemsSource = listaTipoVehiculos;

                
            }
        }

    }
}
