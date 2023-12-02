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

        //private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    //DataTable datosTicket = TrabajarTicket.traerTicket();
        //    //List<Button> botones = new List<Button> { btnE1, btnE2, btnE3, btnE4, btnE5, btnE6, btnE7,
        //    //                                          btnE8, btnE9, btnE10, btnE11, btnE12, btnE13, btnE14,
        //    //                                          btnE15, btnE16, btnE17, btnE18, btnE19, btnE20,
        //    //                                          btnE21, btnE22, btnE23, btnE24, btnE25, btnE26,
        //    //                                          btnE27, btnE28, btnE29, btnE30 };

        //    if (sender is Button)
        //    //foreach (Button boton in botones)
        //    {
        //        Button boton = (Button)sender;

        //        // Verificar el color de fondo
        //        //SolidColorBrush backgroundBrush = botones.Background as SolidColorBrush;

        //        SolidColorBrush backgroundBrush = boton.Background as SolidColorBrush;

        //        TextBlock mensajeTxtBlock = FindResource("mensajeTxtBlock") as TextBlock;
        //        // TextBlock mensajeTxtBlock = (sender as Button).Template.FindName("mensajeTxtBlock", sender as Button) as TextBlock;


        //        if (backgroundBrush != null)
        //        {
        //            Color colorFondo = backgroundBrush.Color;

        //            if (colorFondo == disponible)
        //            {
        //                mensajeTxtBlock.Text = "Sector Libre";
        //                // Mouestra el mensaje cuando el mouse entra en el botón
        //                mensajeTxtBlock.Visibility = Visibility.Visible;

        //            }
        //            else
        //            {
        //                if (colorFondo == ocupado)
        //                {

        //                    mensajeTxtBlock.Text = "Sector Ocupado\n Tiempo ocupado: \nVehiculo:\n Monto a pagar:";
        //                    // Mouestra el mensaje cuando el mouse entra en el botón
        //                    mensajeTxtBlock.Visibility = Visibility.Visible;

        //                }
        //                else
        //                {
        //                    if (colorFondo == deshabilitado)
        //                        mensajeTxtBlock.Text = "Sector NO Disponible";
        //                    // Mouestra el mensaje cuando el mouse entra en el botón
        //                    mensajeTxtBlock.Visibility = Visibility.Visible;

        //                }
        //            }
        //        }
        //    }
        //}  


        //private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    TextBlock mensajeTxtBlock = FindResource("mensajeTxtBlock") as TextBlock;
        //    //TextBlock mensajeTxtBlock = (sender as Button).Template.FindName("mensajeTxtBlock", sender as Button) as TextBlock;

        //    // Oculta el mensaje cuando el mouse sale del botón
        //    mensajeTxtBlock.Visibility = Visibility.Hidden;
        //}


        //private void Button_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    // Cambiar la visibilidad del TextBlock a Visible cuando se pasa el ratón por encima
        //    if (sender is Button)
        //    {
        //        Button button = sender as Button;
        //        ContentControl contentControl = button.Template.FindName("mensajeTxtBlock", button) as ContentControl;
        //        if (contentControl != null)
        //        {
        //            mensajeTxtBlock.Text = "Sector NO Disponible";
        //            contentControl.Visibility = Visibility.Visible;
        //        }
        //    }
        //}

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

                                //string tiempo = string.Format("{0} Días\n Horas: {1}:{2}:{3}",
                                //Math.Abs(diferencia.Days), Math.Abs(diferencia.Hours), Math.Abs(diferencia.Minutes), Math.Abs(diferencia.Seconds));

                                //mensajeTxtBlock.Text = "Sector Libre \nNunca ocupado \n Tiempo: "+ tiempo;
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

                                    // string x = tipoVehiculo(tipoV);
                                    // MessageBox.Show("vehiculo" + tipoV);
                                    // MessageBox.Show("Tiempo" + entrada);

                                    //mensajeTxtBlock.Text = "Ingreso " + numboton;
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

        ///// <summary>
        ///// E. ANDA
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Button_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    if (sender is Button)
        //    {
        //        Button button = sender as Button;
        //        ContentControl contentControl = button.Template.FindName("mensajeContentControl", button) as ContentControl;

        //        if (contentControl != null)
        //        {
        //            TextBlock mensajeTxtBlock = contentControl.Content as TextBlock;

        //            if (mensajeTxtBlock != null)
        //            {
        //                mensajeTxtBlock.Text = "Sector NO Disponible";
        //            }

        //            contentControl.Visibility = Visibility.Visible;
        //        }
        //    }
        //}


        //private void Button_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    // Cambiar la visibilidad del TextBlock a Hidden cuando el ratón sale del botón
        //    if (sender is Button)
        //    {
        //        Button button = sender as Button;
        //        ContentControl contentControl = button.Template.FindName("mensajeTxtBlock", button) as ContentControl;
        //        if (contentControl != null)
        //        {
        //            contentControl.Visibility = Visibility.Hidden;
        //        }
        //    }
        //}


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

        private string tipoVehiculo(int codigo)
        {
            string tipoVeh;
            //TipoVehiculo vehiculo = new TipoVehiculo();

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

        public string calcularDuracionTiempo(DateTime fechaHoraEnt)
        {
            string duracion;
            // DateTime fechaHoraEntrada;
            //if (DateTime.TryParseExact(txtFecYHoraEntrada.Text, "d/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaHoraEntrada))
            //{
            DateTime ahora = DateTime.Now;

            TimeSpan diferencia = ahora.Subtract(fechaHoraEnt);

            int horas = diferencia.Hours;
            int minutos = diferencia.Minutes;

            duracion = horas.ToString() + "Hs - " + minutos.ToString() + "Min";

            return duracion;

            //} 
        }

        public TimeSpan calcularDiferenciaFecha(DateTime fechaHoraPredefinida)
        {
            DateTime ahora = DateTime.Now;
            TimeSpan diferencia = fechaHoraPredefinida.Subtract(ahora);
            return diferencia;
        }

        public int calcularHorasEntreFechas(DateTime fechaHoraPredefinida)
        {
            DateTime ahora = DateTime.Now;
            TimeSpan diferencia = ahora - fechaHoraPredefinida;
            int horasDiferencia = (int)diferencia.TotalHours; // Obtiene la diferencia en horas

            return horasDiferencia;
        }


    }

}

