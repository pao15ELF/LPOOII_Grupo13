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
    /// Interaction logic for WinABMUsuario.xaml
    /// </summary>
    public partial class WinABMUsuario : Window
    {
        private Usuario oUsuario = new Usuario();

        CollectionView Vista;
        ObservableCollection<Usuario> listaUsuario;

        public WinABMUsuario()
        {
            InitializeComponent();
            InitializeCombo();
        }

        /// <summary>
        /// Botón para cerrar el formulario.
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
        /// Metodo para controlar que el formulario antes de cerrar.
        /// </summary>
        /// <returns></returns>
        private bool controlarCancelar()
        {

            if (txtApellido.Text == "" && txtNombre.Text == "" && txtUsuario.Text == "" && txtPassword.Text == "" && cmbRoles.SelectedValue == null)
                return true;
            else
                return false;
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
        /// Metodo para inicializar el combo box con los tres roles disponibles.
        /// </summary>
        private void InitializeCombo()
        {
            List<string> listaRoles = new List<string>();
            listaRoles.Add("Administrador");
            listaRoles.Add("Operador");
            listaRoles.Add("Supervisor");
            this.cmbRoles.ItemsSource = listaRoles;
        }

        /// <summary>
        /// Metodo para cargar los usuarios del Observable al Collection para mostrar en el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["LIST_USER"];
            listaUsuario = odp.Data as ObservableCollection<Usuario>;

            Vista = (CollectionView)CollectionViewSource.GetDefaultView(canvas_content.DataContext);
        }

        /// <summary>
        /// Botón para mostrar el primer usuario de la lista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToFirst();
        }

        /// <summary>
        /// Botón para mostrar el ultimo usuario de la lista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToLast();
        }

        /// <summary>
        /// Botón para mostrar el usuario anterior de la lista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToPrevious();
            if (Vista.IsCurrentBeforeFirst)
            {
                Vista.MoveCurrentToLast();
            }
        }

        /// <summary>
        /// Botón para mostrar el siguiente usuario de la lista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToNext();
            if (Vista.IsCurrentAfterLast)
            {
                Vista.MoveCurrentToFirst();
            }
        }

        /// <summary>
        /// Metodo para limpiar el formulario.
        /// </summary>
        private void limpiarForm()
        {
            txtApellido.Text = "";
            txtNombre.Text = "";
            txtUsuario.Text = "";
            txtPassword.Text = "";
            cmbRoles.SelectedValue = "";
            txtIdUsuModificar.Text = "";
            txtApellido.Focus();
        }

        /// <summary>
        /// Metodo para controlar los datos del formulario para el alta de un Usuario.
        /// </summary>
        /// <returns></returns>
        private bool controlarDatos()
        {
            if (txtApellido.Text == "" || txtNombre.Text == "" || txtUsuario.Text == "" || txtPassword.Text == "" || cmbRoles.SelectedValue == null)
                return false;
            else
                return true;
        }
        
        /// <summary>
        /// Botón para dar de alta un Usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (controlarDatos() == true)
            {
                MessageBoxResult result = MessageBox.Show("¿Desea guardar los datos?", "MENSAJE DE CONFIRMACIÓN", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    cargarDatos();
                    if (btnGuardarUsuario.Content.ToString() == "GUARDAR")
                    {
                        if (TrabajarUsuario.buscarUsuarioXUserName(txtUsuario.Text) == null)
                        {
                            TrabajarUsuario.insertar_Usuario(oUsuario);
                            MessageBox.Show("El nuevo Usuario " + txtUsuario.Text + " se agrego correctamente");

                            cargarListaUsuario();
                            //listaUsuario.Add(oUsuario);
                            Vista.MoveCurrentToLast();
                        }
                        else
                        {
                            MessageBox.Show("EL USUARIO DE NOMBRE " + txtUsuario.Text + " YA SE ENCUNTRA REGISTRADO. \n USE OTRO NOMBRE DE USUARIO PARA REALIZAR EL ALTA", "Aviso", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        TrabajarUsuario.modificar_Usuario(oUsuario);
                        int posActual = Vista.CurrentPosition;
                        MessageBox.Show("Usuario actualizado correctamente");
                        cargarListaUsuario();
                        Vista.MoveCurrentToPosition(posActual);
                        btnGuardarUsuario.Content = "GUARDAR";
                        txtUsuario.IsEnabled = true;
                    }

                    limpiarForm();
                }
                else
                {
                    txtUsuario.IsEnabled = true;
                    limpiarForm();
                }
            }
            else
            {
                MessageBox.Show("TODOS LOS CAMPOS DEBEN ESTAR CARGADOS", "Aviso", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Metodo para cargar los datos del textbox a un objeto Usuario.
        /// </summary>
        private void cargarDatos()
        {
            if (txtIdUsuModificar.Text != "")
                oUsuario.User_Id = int.Parse(txtIdUsuModificar.Text);

            oUsuario.User_Apellido = txtApellido.Text;
            oUsuario.User_Nombre = txtNombre.Text;
            oUsuario.User_UserName = txtUsuario.Text;
            oUsuario.User_Password = txtPassword.Text;
            oUsuario.User_Rol = cmbRoles.SelectedItem.ToString();
        }

        /// <summary>
        /// Metodo para cargar la lista de Usuarios al formulario.
        /// </summary>
        private void cargarListaUsuario()
        {
            listaUsuario = TrabajarUsuario.TraerUsuarios();
            canvas_content.DataContext = listaUsuario;
            Vista = (CollectionView)CollectionViewSource.GetDefaultView(canvas_content.DataContext);
        }

        /// <summary>
        /// Botón para modificar los datos de un Usuario de la lista de Usuarios.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = (Usuario)Vista.CurrentItem;

            txtIdUsuModificar.Text = usuario.User_Id.ToString();
            txtApellido.Text = usuario.User_Apellido;
            txtNombre.Text = usuario.User_Nombre;
            txtUsuario.Text = usuario.User_UserName;
            txtPassword.Text = usuario.User_Password;
            cmbRoles.SelectedValue = usuario.User_Rol;

            txtUsuario.IsEnabled = false;

            btnGuardarUsuario.Content = "ACTUALIZAR";
        }

        /// <summary>
        /// Botón para eliminar un Usuario de la lista de Usuarios.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result;

            Result = MessageBox.Show("¿Seguro que quiere eliminar el usuario?", "Confirmación ", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                Usuario usuario = (Usuario)Vista.CurrentItem;
                TrabajarUsuario.eliminar_Usuario(usuario.User_Id);
                cargarListaUsuario();
                Vista.MoveCurrentToLast();
            }
        }
    }
}
