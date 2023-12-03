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
        /// Botón para minimizar el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Botón para cerrar el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            WinWelcome login = new WinWelcome();
            login.Show();
            this.Close();
        }

        /// <summary>
        /// Menu Logout click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuLogout_Click(object sender, RoutedEventArgs e)
        {
            WinWelcome login = new WinWelcome();
            login.Show();
            this.Close();
        }

        /// <summary>
        /// Metodo para ocultar pestañas dependiendo del tipo de rol.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Util.Util.getUsuario().User_Rol == "Administrador")
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
                if (Util.Util.getUsuario().User_Rol == "Operador")
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

        /// <summary>
        /// Menu Gestion Clientes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuGestionClientes_Click(object sender, RoutedEventArgs e)
        {
            WinABMClientes winClientes = new WinABMClientes();
            winClientes.Show();
            this.Close();
        }

        /// <summary>
        /// Menu Gestion Vehiculos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuGestionVehiculos_Click(object sender, RoutedEventArgs e)
        {
            WinABMVehiculos winVehiculos = new WinABMVehiculos();
            winVehiculos.Show();
            this.Close();
        }

        /// <summary>
        /// Menu Vehiculo en Playa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuVehiculoPlaya_Click(object sender, RoutedEventArgs e)
        {
            WinVehiculosPlayas winPlaya = new WinVehiculosPlayas();
            winPlaya.Show();
            this.Close();
        }

        /// <summary>
        /// Menu Estado Sectores.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuEstadoSectores_Click(object sender, RoutedEventArgs e)
        {
            WinEstadoSector winEsSectores = new WinEstadoSector();
            winEsSectores.Show();
            this.Close();
        }

        /// <summary>
        /// Menu Gestion Usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuGestionUsuario_Click(object sender, RoutedEventArgs e)
        {
            WinABMUsuario winUsuarios = new WinABMUsuario();
            winUsuarios.Show();
            this.Close();
        }

        /// <summary>
        /// Menu Listado de Usuarios.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuListadoUsuario_Click(object sender, RoutedEventArgs e)
        {
            WinListaUsuariosFiltros winLisUsuario = new WinListaUsuariosFiltros();
            winLisUsuario.Show();
            this.Close();
        }

        /// <summary>
        /// Menu Registrar Entrada al Estacionamiento.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuResgistrarEntrada_Click(object sender, RoutedEventArgs e)
        {
            WinRegistrarEntrada winRegEntrada = new WinRegistrarEntrada();
            winRegEntrada.Show();
            this.Close();
        }

        /// <summary>
        /// Menu Nosotros.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuNosotros_Click(object sender, RoutedEventArgs e)
        {
            WinAcercaDeNosotros winNosotros = new WinAcercaDeNosotros();
            winNosotros.Show();
            this.Close();
        }

        /// <summary>
        /// Menu Registrar Salida del Estacionamiento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuResgistrarSalida_Click(object sender, RoutedEventArgs e)
        {
            WinRegistrarSalida winRegSalida = new WinRegistrarSalida();
            winRegSalida.Show();
            this.Close();
        }

        /// <summary>
        /// Menu Sectores Ocupados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSectoresOcupados_Click(object sender, RoutedEventArgs e)
        {
            WinListarSectores winListaReg = new WinListarSectores();
            winListaReg.Show();
            this.Close();
        }

        /// <summary>
        /// Menu Ventas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuVentas_Click(object sender, RoutedEventArgs e)
        {
            WinListarVentas winVentas = new WinListarVentas();
            winVentas.Show();
            this.Close();
        }


    }
}
