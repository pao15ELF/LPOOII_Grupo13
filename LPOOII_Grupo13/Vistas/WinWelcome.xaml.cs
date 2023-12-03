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

using ClaseBase;
using Util;
namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinWelcome.xaml
    /// </summary>
    public partial class WinWelcome : Window
    {
        //private Usuario admin;
        //private Usuario operador;
        //private Usuario supervisor;

        private Usuario buscarUsuario= new Usuario();

        private MediaPlayer mediaPlayer = new MediaPlayer();

        public WinWelcome()
        {
            InitializeComponent();
            inicializarUsuarios();
            login.txtUsuario.Focus();
        }

        public void inicializarUsuarios()
        {
            //admin = new Usuario(1, "admin", "admin", "Flores", "Paola", "Administrador");
            //operador = new Usuario(2, "operador", "operador", "Zamudio", "Cintia", "Operador");
            //supervisor = new Usuario(3, "super","super","Flores","Karen", "Supervisor");
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            login.txtUsuario.Focus();
            
        }

        /// <summary>
        /// Metodo para reproducir un sonido al realizar un login correcto.
        /// </summary>
        /// <param name="ruta"></param>
         private void ReproducirSonido(string ruta)
        {
            try
            {
                mediaPlayer.Open(new Uri(ruta, UriKind.RelativeOrAbsolute));
                mediaPlayer.Play();
            }
            catch (Exception exep)
            {
                MessageBox.Show("Error al reproducir el sonido: " + exep.Message);
            }
        }
        
        

        /// <summary>
        /// Botón para realizar la verificación de la contraseña y usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string nomUsuario = login.txtUsuario.Text;
            string contrasenia = new System.Net.NetworkCredential(string.Empty, login.txtPassword.Password).Password;

            buscarUsuario.User_UserName = nomUsuario;
            buscarUsuario.User_Password = new System.Net.NetworkCredential(string.Empty, login.txtPassword.Password).Password;

            Usuario usuarioEncontrado =TrabajarUsuario.buscarUsuario(buscarUsuario);

            if (usuarioEncontrado!=null)
            {

                ReproducirSonido("Sonido/intro.mp3");

                MessageBox.Show("BIENVENIDO: " + nomUsuario);
                WinPrincipal oWinPri = new WinPrincipal();
                Util.Util.setUsuario(usuarioEncontrado);
                oWinPri.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrecta", "AVISO");
            }
        }

        /// <summary>
        /// Botón para cerrar el formulario login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
        /// Metodo para poder arrastrar el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }


    }
}
