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

        public WinABMVehiculos()
        {
            InitializeComponent();
            listaTipoVehiculos = TrabajarTipoVehiculo.listarTipoVehiculo(); 
            lvwTipoVehiculos.ItemsSource = listaTipoVehiculos;
            DataContext = this; 
        }

        /// <summary>
        /// Metodo para poder arrastrar el formulario.
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
        /// Metodo para controlar el cierre del formulario.
        /// </summary>
        /// <returns></returns>
        private bool controlarCancelar()
        {
            if (txtCodVehiculo.Text == "" && txtDescripcion.Text == "" && txtTarifa.Text == "")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Metodo para cargar los datos del textbox al objeto Tipo Vehiculo.
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
        /// Botón para dar de alta un Vehiculo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            if (controlarDatos() == true)
            {
                MessageBoxResult result = MessageBox.Show("¿Desea guardar los datos?", "MENSAJE DE CONFIRMACIÓN", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    cargarDatos();

                    if (btnGuardarVehiculo.Content.ToString() == "GUARDAR")
                    {
                        if (TrabajarTipoVehiculo.buscarTipoVehiculoXCodigo(Convert.ToInt32(txtCodVehiculo.Text)) == null)
                        {
                            TrabajarTipoVehiculo.insertar_TipoVehiculo(oTipoVehiculo);

                            // Actualizar la lista después de insertar un nuevo elemento
                            listaTipoVehiculos = TrabajarTipoVehiculo.listarTipoVehiculo();
                            lvwTipoVehiculos.ItemsSource = listaTipoVehiculos;

                            MessageBox.Show("El Vehiculo con el codigo " + txtCodVehiculo.Text + " con la descripcion: " + txtDescripcion.Text + " se guardo correctamente");
                        }
                        else
                        {
                            MessageBox.Show("EL VEHICULO CON CODIGO " + txtCodVehiculo.Text + " YA SE ENCUENTRA REGISTRADO", "Aviso", MessageBoxButton.OK);
                        }
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

                        txtCodVehiculo.IsEnabled = true;
                    }
                    limpiarForm();
                }
                else
                {
                    txtCodVehiculo.IsEnabled = true;
                    limpiarForm();
                }
            }
            else
            {
                MessageBox.Show("TODOS LOS CAMPOS DEBEN ESTAR CARGADOS", "Aviso", MessageBoxButton.OK);
            }
            
        }

        /// <summary>
        /// Metodo para controlar los datos del formulario para el alta de un Vehiculo.
        /// </summary>
        /// <returns></returns>
        private bool controlarDatos()
        {
            if (txtCodVehiculo.Text == "" || txtDescripcion.Text == "" || txtTarifa.Text == "" || imgVehiculo.Source == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Metodo para limpiar el formulario.
        /// </summary>
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

        /// <summary>
        /// Botón para modificar los datos de un vehiculo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            cargarTextBox();
            btnGuardarVehiculo.Content = "ACTUALIZAR";
            btnInsertarImg.Content = "Cambiar Foto";

            txtCodVehiculo.IsEnabled = false;
        }

        /// <summary>
        /// Metodo para cargar los datos del textbox a un objeto TipoVehiculo.
        /// </summary>
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

        /// <summary>
        /// Botón para eliminar un Vehiculo de la lista de Vehiculos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Botón para insertar una foto para dar de alta un Vehiculo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Metodo para convertir una imagen a base 64.
        /// </summary>
        /// <param name="foto"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Pasar una base 64 a imagen.
        /// </summary>
        /// <param name="base64String"></param>
        private void CargarImagenDesdeBase64(string base64String)
        {
            
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
        }



    }
}
