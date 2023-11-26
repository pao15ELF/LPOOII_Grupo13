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

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinVehiculosPlayas.xaml
    /// </summary>
    public partial class WinVehiculosPlayas : Window
    {
        System.Windows.Media.Color disponible = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF00E100");
        System.Windows.Media.Color ocupado = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFEB0000");
        System.Windows.Media.Color deshabilitado = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF6F6C6C");

        public WinVehiculosPlayas()
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
            //AGREGAR MESSAGE PARA PREGUNTAR SI VOLVER AL MENU PRINCIPAL
            WinPrincipal winPri = new WinPrincipal();
            winPri.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnE7.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEB0000"));
            btnE4.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6F6C6C"));
        }


        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            WinPrincipal winPri = new WinPrincipal();
            winPri.Show();
            this.Close();
        }

        private void comprobarSector(Brush estado)
        {
            SolidColorBrush colorSolido = (SolidColorBrush)estado;
            System.Windows.Media.Color colorBoton = colorSolido.Color;

            if (colorBoton == disponible)
            {
                MessageBox.Show("Sector Disponible. Resgistrar Entrada");
            }
            else
            {
                if (colorBoton == ocupado)
                {
                    MessageBox.Show("Sector Ocupado. Registrar Salida");
                }
                else
                {
                    if (colorBoton == deshabilitado)
                        MessageBox.Show("Sector Deshabilitado");
                }
            }
        }

        private void btnE1_Click(object sender, RoutedEventArgs e)
        {
            comprobarSector(btnE1.Background);
        }

        private void btnE2_Click(object sender, RoutedEventArgs e)
        {
            comprobarSector(btnE2.Background);
        }

        private void btnE3_Click(object sender, RoutedEventArgs e)
        {
            comprobarSector(btnE3.Background);
        }

        private void btnE4_Click(object sender, RoutedEventArgs e)
        {
            comprobarSector(btnE4.Background);
        }

        private void btnE5_Click(object sender, RoutedEventArgs e)
        {
            comprobarSector(btnE5.Background);
        }

        private void btnE6_Click(object sender, RoutedEventArgs e)
        {
            comprobarSector(btnE6.Background);
        }

        private void btnE7_Click(object sender, RoutedEventArgs e)
        {
            comprobarSector(btnE7.Background);
        }

        private void btnE8_Click(object sender, RoutedEventArgs e)
        {
            comprobarSector(btnE8.Background);
        }

        private void btnE9_Click(object sender, RoutedEventArgs e)
        {
            comprobarSector(btnE9.Background);
        }

        private void btnE10_Click(object sender, RoutedEventArgs e)
        {
            comprobarSector(btnE10.Background);
        }

        
    }
}
