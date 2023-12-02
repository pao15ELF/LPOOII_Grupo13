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
    /// Interaction logic for WinPrincipal.xaml
    /// </summary>
    public partial class WinPrincipal : Window
    {
        public WinPrincipal()
        {
            InitializeComponent();
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
            //Application.Current.Shutdown();
            WinWelcome login = new WinWelcome();
            login.Show();
            this.Close();
        }

        private void mnuLogout_Click(object sender, RoutedEventArgs e)
        {
            WinWelcome login = new WinWelcome();
            login.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (Util.Util.getUsuario().User_UserName == "admin")
            {
                mnuSectores.Visibility = Visibility.Visible;
                mnuTipoVehiculos.Visibility = Visibility.Visible;
                mnuClientes.Visibility = Visibility.Collapsed;
                mnuUsuarios.Visibility = Visibility.Collapsed;
                mnuEstacionamientos.Visibility = Visibility.Collapsed;
                mnuLogout.Visibility = Visibility.Visible;
            }
            else
            {
                if (Util.Util.getUsuario().User_UserName == "operador")
                {
                    mnuSectores.Visibility = Visibility.Collapsed;
                    mnuTipoVehiculos.Visibility = Visibility.Collapsed;
                    mnuClientes.Visibility = Visibility.Visible;
                    mnuUsuarios.Visibility = Visibility.Collapsed;
                    mnuEstacionamientos.Visibility = Visibility.Visible;
                    mnuLogout.Visibility = Visibility.Visible;
                }
                else
                {
                    mnuSectores.Visibility = Visibility.Visible;
                    mnuTipoVehiculos.Visibility = Visibility.Visible;
                    mnuClientes.Visibility = Visibility.Visible;
                    mnuUsuarios.Visibility = Visibility.Visible;
                    mnuEstacionamientos.Visibility = Visibility.Visible;
                    mnuLogout.Visibility = Visibility.Visible;
                }
            }
        }

        private void mnuGestionClientes_Click(object sender, RoutedEventArgs e)
        {
            WinABMClientes winClientes = new WinABMClientes();
            winClientes.Show();
            this.Close();
        }

        private void mnuGestionVehiculos_Click(object sender, RoutedEventArgs e)
        {
            WinABMVehiculos winVehiculos = new WinABMVehiculos();
            winVehiculos.Show();
            this.Close();
        }

        private void mnuVehiculoPlaya_Click(object sender, RoutedEventArgs e)
        {
            WinVehiculosPlayas winPlaya = new WinVehiculosPlayas();
            winPlaya.Show();
            this.Close();
        }

        private void mnuEstadoSectores_Click(object sender, RoutedEventArgs e)
        {
            WinEstadoSector winEsSectores = new WinEstadoSector();
            winEsSectores.Show();
            this.Close();
        }

        private void mnuGestionUsuario_Click(object sender, RoutedEventArgs e)
        {
            WinABMUsuario winUsuarios = new WinABMUsuario();
            winUsuarios.Show();
            this.Close();
        }

        private void mnuListadoUsuario_Click(object sender, RoutedEventArgs e)
        {
            WinListaUsuariosFiltros winLisUsuario = new WinListaUsuariosFiltros();
            winLisUsuario.Show();
            this.Close();
        }

        private void mnuResgistrarEntrada_Click(object sender, RoutedEventArgs e)
        {
            WinRegistrarEntrada winRegEntrada = new WinRegistrarEntrada();
            winRegEntrada.Show();
            this.Close();
        }

        private void mnuNosotros_Click(object sender, RoutedEventArgs e)
        {
            WinAcercaDeNosotros winNosotros = new WinAcercaDeNosotros();
            winNosotros.Show();
            this.Close();
        }

        private void mnuResgistrarSalida_Click(object sender, RoutedEventArgs e)
        {
            WinRegistrarSalida winRegSalida = new WinRegistrarSalida();
            winRegSalida.Show();
            this.Close();
        }

        private void mnuSectoresOcupados_Click(object sender, RoutedEventArgs e)
        {
            WinListarSectores winListaReg = new WinListarSectores();
            winListaReg.Show();
            this.Close();
        }

        private void mnuVentas_Click(object sender, RoutedEventArgs e)
        {
            WinListarVentas winVentas = new WinListarVentas();
            winVentas.Show();
            this.Close();
        }


    }
}
