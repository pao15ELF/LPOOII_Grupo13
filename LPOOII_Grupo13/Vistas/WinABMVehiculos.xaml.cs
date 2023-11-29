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
using Microsoft.Win32;
using System.IO;

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

            BitmapImage imagen = imgVehiculo.Source as BitmapImage;
            string x = imgBase64(imagen);
            oTipoVehiculo.TypV_Imagen = x;
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
                    BitmapImage imagen = imgVehiculo.Source as BitmapImage;
                    string x = imgBase64(imagen);
                    oTipoVehiculo.TypV_Imagen = x;

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
            imgVehiculo.Source = null;
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
            btnInsertarImg.Content = "Cambiar Foto";
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

                SeleccionarVehiculo();               

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

        private void btnInsertarFoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivo de imagen (.jpg)|*.jpg|All Files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    BitmapImage foto = new BitmapImage();
                    foto.BeginInit();
                    foto.UriSource = new Uri(ofd.FileName);
                    foto.EndInit();
                    foto.Freeze();

                    imgVehiculo.Source = foto;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen: " + ex.Message, "Error");
                }
            }
        }


        private string imgBase64(BitmapImage foto)
        {
                // Convertir la imagen a base64
                byte[] imageData;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(foto));

                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    imageData = ms.ToArray();
                }

                string base64String = Convert.ToBase64String(imageData);
                return base64String;
        }

        //tercero
        private void SeleccionarVehiculo()
        {
            // Obtener la cadena Base64 del vehículo seleccionado desde la base de datos
            string base64Image = ObtenerBase64DeLaBaseDeDatos();

            // Llamar al método para cargar la imagen
            CargarImagenDesdeBase64(base64Image);
        }

        //primero
        private string ObtenerBase64DeLaBaseDeDatos()
        {
            TipoVehiculo tipoVehiculoMod = (TipoVehiculo)lvwTipoVehiculos.SelectedItem;
            string base64String = tipoVehiculoMod.TypV_Imagen.ToString(); // Aquí obtienes tu cadena Base64 desde tu objeto o propiedad       
            return base64String;
         }

        //segundo
        private void CargarImagenDesdeBase64(string base64String)
        {
            //try
            //{
                byte[] imageBytes = Convert.FromBase64String(base64String);

                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    // Asignar la imagen cargada al Image
                    imgVehiculo.Source = bitmapImage;
                }
            //}
            //catch (Exception ex)
            //{
            //    // Manejo de excepciones: muestra información sobre la excepción
            //    Console.WriteLine("Error al inicializar BitmapImage desde Base64: " + ex.Message);
            //}
        }



    }
}
