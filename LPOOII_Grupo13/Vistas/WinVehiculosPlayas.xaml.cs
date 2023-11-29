using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ClaseBase;
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
            comprobarEstadoSectores();
            //suscribirBotones();
        }

        private void comprobarEstadoSectores()
        {
            SolidColorBrush colorDisponible = new SolidColorBrush(disponible);
            SolidColorBrush colorOcupado = new SolidColorBrush(ocupado);
            DataTable sectores = TrabajarSectores.traerSectores();

            for (int i = 1; i <= 30; i++)
            {
                // Obtener el nombre del botón actual
                string nombreBoton = "btnE" + i;

                // Encontrar el botón correspondiente
                Button boton = FindName(nombreBoton) as Button;

                if (boton != null)
                {
                    // Obtener el registro correspondiente al botón actual
                    DataRow[] fila = sectores.Select("Sec_Identificador = 'E" + i + "'");

                    if (fila.Length > 0)
                    {
                        // Obtener el estado del botón
                        string estado = fila[0]["Sec_Habilitado"].ToString();

                        // Cambiar el color dependiendo del estado
                        if (estado == "Disponible")
                        {
                            boton.Background = colorDisponible; // Color verde si está habilitado
                        }
                        else if (estado == "Ocupado")
                        {
                            boton.Background = colorOcupado; // Color rojo si está ocupado
                        }
                    }
                }
            }
        }
        //private void comprobarEstadoSectores()
        //{
        //    SolidColorBrush colorDisponible = new SolidColorBrush(disponible);
        //    SolidColorBrush colorOcupado = new SolidColorBrush(ocupado);
        //    Button[] botones = new Button[] { btnE1, btnE2, btnE3, btnE4, btnE5, btnE6, btnE7, btnE8, btnE9, btnE10 };
        //    DataTable sectores = TrabajarSectores.traerSectores();

        //    foreach (Button boton in botones)
        //    {
        //        // Obtener el número del botón (E1, E2, ..., E10)
        //        string nombreBoton = boton.Name;
        //        string numeroBoton = nombreBoton.Substring(4); // Suponiendo que siempre son 4 caracteres "btnE"

        //        // Obtener el registro correspondiente al botón actual
        //        DataRow[] fila = sectores.Select("Sec_Identificador = 'E" + numeroBoton + "'");

        //        if (fila.Length > 0)
        //        {
        //            // Obtener el estado del botón
        //            string estado = fila[0]["Sec_Habilitado"].ToString();

        //            // Cambiar el color dependiendo del estado
        //            if (estado == "Disponible")
        //            {
        //                boton.Background = colorDisponible; // Color verde si está habilitado
        //            }
        //            else if (estado == "Ocupado")
        //            {
        //                boton.Background = colorOcupado; // Color rojo si está ocupado
        //            }
        //        }
        //    }
        //}

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
            //son los datos cargados en el primer tp
            //btnE7.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEB0000"));
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
                MessageBox.Show("Sector Disponible. Resgistrar Entrada"); //AGREGAR FUNCION PARA MANDAR A REGISTRAR ENTRADA
            }
            else
            {
                if (colorBoton == ocupado)
                {
                    MessageBox.Show("Sector Ocupado. Registrar Salida"); //AGREGAR FUNCION PARA MANDAR A REGISTRAR SALIDA
                }
                else
                {
                    if (colorBoton == deshabilitado)
                        MessageBox.Show("Sector Deshabilitado");
                }
            }
        }

        private void Boton_Click(object sender, RoutedEventArgs e)
        {
            // Determinar qué botón se hizo clic
            Button boton = sender as Button;

            if (boton != null)
            {
                comprobarSector(boton.Background);
            }
        }
    }
}
