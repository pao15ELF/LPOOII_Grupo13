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
using System.Diagnostics;
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

        /// <summary>
        /// Metodo para comprobar el estado de los sectores y darle el color que correspone
        /// Rojo: Ocupado.
        /// Verde: Disponible.
        /// Gris: Deshabilitado.
        /// </summary>
        private void comprobarEstadoSectores()
        {
            SolidColorBrush colorDisponible = new SolidColorBrush(disponible);
            SolidColorBrush colorOcupado = new SolidColorBrush(ocupado);
            SolidColorBrush colorDeshabilitado = new SolidColorBrush(deshabilitado);
            DataTable sectores = TrabajarSectores.traerSectores();

            for (int i = 1; i <= 30; i++)
            {
                string nombreBoton = "btnE" + i;

                Button boton = FindName(nombreBoton) as Button;

                if (boton != null)
                {
                    DataRow[] fila = sectores.Select("Sec_Identificador = 'E" + i + "'");

                    if (fila.Length > 0)
                    {
                        string estado = fila[0]["Sec_Habilitado"].ToString();

                        if (estado == "Disponible")
                        {
                            boton.Background = colorDisponible; // Color verde si está habilitado
                        }
                        else 
                        {
                            if (estado == "Ocupado")
                            {
                                boton.Background = colorOcupado; // Color rojo si está ocupado
                            }
                            else 
                            {
                                boton.Background = colorDeshabilitado; //Color gris se esta deshabilitado.
                            }
                        }
                    }
                }
            }
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
            //AGREGAR MESSAGE PARA PREGUNTAR SI VOLVER AL MENU PRINCIPAL
            WinPrincipal winPri = new WinPrincipal();
            winPri.Show();
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnE4.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6F6C6C")); //
        }

        /// <summary>
        /// Botón para salir del formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            WinPrincipal winPri = new WinPrincipal();
            winPri.Show();
            this.Close();
        }

        /// <summary>
        /// Metodo para comprobar el estado del sector.
        /// </summary>
        /// <param name="estado"></param>
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

        /// <summary>
        /// Metodo para cuando se haga click en algun sector.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Boton_Click(object sender, RoutedEventArgs e)
        {
            // Determinar qué botón se hizo clic
            Button boton = sender as Button;

            if (boton != null)
            {
                comprobarSector(boton.Background);
            }
        }

        /// <summary>
        /// Metodo para cuando el mouse este sobre un sector.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;

                SolidColorBrush backgroundBrush = button.Background as SolidColorBrush;

                ContentControl contentControl = button.Template.FindName("mensajeContentControl", button) as ContentControl;

                string numboton = button.Content.ToString();
                //MessageBox.Show("boton"+numboton);

                if (contentControl != null && backgroundBrush != null)
                {
                    Color colorFondo = backgroundBrush.Color;
                    TextBlock mensajeTxtBlock = contentControl.Content as TextBlock;

                    if (mensajeTxtBlock != null && colorFondo == disponible)
                    {
                        DataTable dt = TrabajarTicket.traerTicketRegEntrada();

                        foreach (DataRow row in dt.Rows)
                        {
                            string tic_SecCodigo = row["Tic_SecCodigo"].ToString();
                            tic_SecCodigo = "E" + tic_SecCodigo;

                            if (tic_SecCodigo.Equals(numboton))
                            {
                                DateTime salida = (DateTime)row["Tic_FecHoraSal"];

                                // MessageBox.Show("Tiempo" + entrada);
                                mensajeTxtBlock.Text = "Sector Libre \nTiempo: " + calcularDuracionTiempo(salida);

                                break;
                            }
                            else
                            {
                                DateTime fechaHoraPredefinida = new DateTime(2023, 11, 29, 00, 15, 22); // Año, Mes, Día, Hora, Minuto, Segundo
                                TimeSpan diferencia = calcularDiferenciaFecha(fechaHoraPredefinida);

                                mensajeTxtBlock.Text = "Sector Libre \nNunca ocupado \n Tiempo: " + string.Format("{0} Días - ", Math.Abs(diferencia.Days)) + calcularHorasEntreFechas(fechaHoraPredefinida) + " Hs";
                            }
                        }
                        // Mouestra el mensaje cuando el mouse entra en el botón
                        contentControl.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        if (mensajeTxtBlock != null && colorFondo == ocupado)
                        {
                            DataTable dt = TrabajarTicket.traerTicketRegEntrada();

                            foreach (DataRow row in dt.Rows)
                            {
                                string tic_SecCodigo = row["Tic_SecCodigo"].ToString();
                                tic_SecCodigo = "E" + tic_SecCodigo;
                                //MessageBox.Show("sector"+tic_SecCodigo);

                                if (tic_SecCodigo.Equals(numboton))
                                {
                                    int tipoV = Convert.ToInt32(row["Tic_TVCodigo"]);
                                    DateTime entrada = (DateTime)row["Tic_FecHoraEnt"];

                                    mensajeTxtBlock.Text = "Sector Ocupado\n Monto:" + row["Tic_Tarifa"].ToString()
                                        + "\nVehiculo: " + tipoVehiculo(tipoV) + "\nTiempo: " + calcularDuracionTiempo(entrada);

                                    break; // Detiene el bucle una vez que encuentre la coincidencia
                                }
                            }
                            // Mouestra el mensaje cuando el mouse entra en el botón
                            contentControl.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            if (mensajeTxtBlock != null && colorFondo == deshabilitado)
                            {
                                mensajeTxtBlock.Text = "Sector NO Disponible";
                                // Mouestra el mensaje cuando el mouse entra en el botón
                                contentControl.Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Metodo para cuando el mouse no este sobre un sector.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                ContentControl contentControl = button.Template.FindName("mensajeContentControl", button) as ContentControl;

                if (contentControl != null)
                {
                    TextBlock mensajeTxtBlock = contentControl.Content as TextBlock;

                    if (mensajeTxtBlock != null)
                    {
                        mensajeTxtBlock.Text = " ";
                    }

                    contentControl.Visibility = Visibility.Hidden;
                }
            }
        }

        /// <summary>
        /// Función para mostrar el tipo de vehiculo que esta estacionado.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        private string tipoVehiculo(int codigo)
        {
            string tipoVeh;

            TipoVehiculo vehiculo = TrabajarTipoVehiculo.buscarTipoVehiculoXCodigo(codigo);

            if (vehiculo != null)
            {
                tipoVeh = vehiculo.TypV_Descripcion;
            }
            else
            {
                // En caso que vehiculo sea null
                tipoVeh = " ";
            }
            return tipoVeh;
        }

        /// <summary>
        /// Función para calcular la duracion de tiempo entre dos fechas.
        /// </summary>
        /// <param name="fechaHoraEnt"></param>
        /// <returns></returns>
        public string calcularDuracionTiempo(DateTime fechaHoraEnt)
        {
            string duracion;
            
            DateTime ahora = DateTime.Now;

            TimeSpan diferencia = ahora.Subtract(fechaHoraEnt);

            int horas = diferencia.Hours;
            int minutos = diferencia.Minutes;

            duracion = horas.ToString() + "Hs - " + minutos.ToString() + "Min";

            return duracion;

        }

        /// <summary>
        /// Funcion para calcular la diferencia entre dos fechas.
        /// </summary>
        /// <param name="fechaHoraPredefinida"></param>
        /// <returns></returns>
        public TimeSpan calcularDiferenciaFecha(DateTime fechaHoraPredefinida)
        {
            DateTime ahora = DateTime.Now;
            TimeSpan diferencia = fechaHoraPredefinida.Subtract(ahora);
            return diferencia;
        }

        /// <summary>
        /// Función para calcular las horas entre una fecha.
        /// </summary>
        /// <param name="fechaHoraPredefinida"></param>
        /// <returns></returns>
        public int calcularHorasEntreFechas(DateTime fechaHoraPredefinida)
        {
            DateTime ahora = DateTime.Now;
            TimeSpan diferencia = ahora - fechaHoraPredefinida;
            int horasDiferencia = (int)diferencia.TotalHours; // Obtiene la diferencia en horas

            return horasDiferencia;
        }


    }

}

