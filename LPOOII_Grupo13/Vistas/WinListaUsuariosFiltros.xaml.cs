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

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinListaUsuariosFiltros.xaml
    /// </summary>
    public partial class WinListaUsuariosFiltros : Window
    {
        private CollectionViewSource vistaColeccionFiltrada;

        public WinListaUsuariosFiltros()
        {
            InitializeComponent();
            vistaColeccionFiltrada = Resources["FILTRO_USERNAME"] as CollectionViewSource;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            //AGREGAR MESSAGE PARA PREGUNTAR SI VOLVER AL MENU PRINCIPAL
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

        private void eventVistaUsuario_Filter(object sender, FilterEventArgs e)
        {
            Usuario aUser = e.Item as Usuario;

            //Se realiza la busqueda por username
            if (aUser.User_UserName.StartsWith(txtFiltro.Text, StringComparison.CurrentCultureIgnoreCase))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }


        }

        private void txtFiltro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vistaColeccionFiltrada != null)
            {
                vistaColeccionFiltrada.Filter += eventVistaUsuario_Filter;
            }
        }

        private void btnVistaPrevia_Click(object sender, RoutedEventArgs e)
        {        
            VistaPreviaDeImpresion vistaP = new VistaPreviaDeImpresion(vistaColeccionFiltrada);
            vistaP.Show();
            this.Close();
        }


    }
}
