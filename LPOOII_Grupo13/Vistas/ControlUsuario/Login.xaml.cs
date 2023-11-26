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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClaseBase;

namespace Vistas.ControlUsuario
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private Usuario admin;
        private Usuario operador;


        public Login()
        {
            InitializeComponent();
            inicializarUsuarios();

        }

        //Obtiene el texto del TextBox
        public string Usuario
        {
            get { return txtUsuario.Text; }
        }

        //Obtiene el texto del PasswordBox
        public string Contrasenia
        {
            get { return txtPassword.Password; }
        }

        public void inicializarUsuarios()
        {
            admin = new Usuario(1, "admin", "admin", "Flores", "Paola", "Administrador");
            operador = new Usuario(2, "operador", "operador", "Zamudio", "Cintia", "Operador");
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string nomUsuario = txtUsuario.Text;
            //string contrasenia = new System.Net.NetworkCredential(string.Empty,txtPassword.Text).Password;
            String contrasenia = txtPassword.Password;

            MessageBox.Show("Usuario:" +nomUsuario+" con: "+contrasenia, "AVISO");
            if ((nomUsuario == admin.User_UserName && contrasenia == admin.User_Password) ||
                (nomUsuario == operador.User_UserName && contrasenia == operador.User_Password))
            {
                MessageBox.Show("BIENVENIDO: " + nomUsuario);
                WinPrincipal oWinPri = new WinPrincipal();
                Usuario usu = new Usuario();
                usu.User_UserName = nomUsuario;
                usu.User_Password = contrasenia;
                Util.Util.setUsuario(usu);
                oWinPri.Show();

                cerrarFormulario();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrecta", "AVISO");
            }
        }

        public void cerrarFormulario()
        {
            foreach (Window ventana in Application.Current.Windows)
            {
                if (ventana is WinWelcome)
                {
                    ventana.Close();
                    break;
                }
            }
        }
    }
}
