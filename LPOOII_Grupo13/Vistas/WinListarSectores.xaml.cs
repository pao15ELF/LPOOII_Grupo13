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
using System.Globalization;
using System.Collections.ObjectModel;

using ClaseBase;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinListarSectores.xaml
    /// </summary>
    public partial class WinListarSectores : Window
    {

        ObservableCollection<Ticket> listarTickets = TrabajarTicket.traerTicketConHoraSalNull();
        ObservableCollection<Util.SectoresOcupados> listaSectoresOcupados = new ObservableCollection<Util.SectoresOcupados>();


        public WinListarSectores()
        {
            InitializeComponent();
            cargarListView();
            lvwListaSectores.ItemsSource = listaSectoresOcupados;
            DataContext = this;
        }

        /// <summary>
        /// Botón para cerrar el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            WinPrincipal winPri = new WinPrincipal();
            winPri.Show();
            this.Close();
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

        /// <summary>
        /// Metodo para cargar los datos a la lista de Sectores Ocupados.
        /// </summary>
        private void cargarListView()
        {
            foreach(var item in listarTickets){
                Util.SectoresOcupados sectorOcupado= new Util.SectoresOcupados();

                sectorOcupado.Sector = item.Tick_SectorCodigo;
                sectorOcupado.Zona = calcularZona(item.Tick_SectorCodigo);
                sectorOcupado.FecHoraEntrada = item.Tick_FechaHoraEnt;
                sectorOcupado.Patente = item.Tick_Patente;
                sectorOcupado.Cliente = nombreCliente(item.Tick_ClienteDni);
                sectorOcupado.TipoVehiculo = tipoVehiculo(item.Tick_TVCodigo);
                sectorOcupado.TiempoTranscurrido = tiempoTranscurrido(item.Tick_FechaHoraEnt);

                listaSectoresOcupados.Add(sectorOcupado);
            }
        }

        /// <summary>
        /// Función para calcular el tiempo transcurrido desde la entrada hasta el dia actual.
        /// </summary>
        /// <param name="fechaHoraEnt"></param>
        /// <returns>string</returns>
        private string tiempoTranscurrido(DateTime fechaHoraEnt)
        {
            DateTime ahora = DateTime.Now;
            TimeSpan diferencia = ahora.Subtract(fechaHoraEnt);

            int horas = diferencia.Hours;
            int minutos = diferencia.Minutes;

            return horas + "Hs - " + minutos + "min";
        }

        /// <summary>
        /// Función para buscar la descripcion de un Vehiculo por su codigo.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns>string</returns>
        private string tipoVehiculo(int codigo)
        {
            TipoVehiculo tipoVehiculo = TrabajarTipoVehiculo.buscarTipoVehiculoXCodigo(codigo);
            return tipoVehiculo.TypV_Descripcion;
        }

        /// <summary>
        /// Función para buscar el Nombre y Apellido de un Cliente por su DNI.
        /// </summary>
        /// <param name="dni"></param>
        /// <returns>string</returns>
        private string nombreCliente(int dni)
        {
            Cliente cliente = TrabajarCliente.buscarCliente(dni);

            return cliente.Cli_Apellido1 + ", " + cliente.Cli_Nombre1;
        }

        /// <summary>
        /// Función para calcular a la zona que pertenece.
        /// </summary>
        /// <param name="sector"></param>
        /// <returns></returns>
        private int calcularZona(int sector)
        {
            if (sector <= 10)
            {
                return 1;
            }
            else
            {
                if (sector >= 11 && sector <= 20)
                {
                    return 2;
                }
                else
                    return 3;
            }
        }

        /// <summary>
        /// Botón para ver la vista previa para imprimir la lista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVistaPrevia_Click(object sender, RoutedEventArgs e)
        {
            VistaPreviaDeImpresionSecOcupados vPrevia = new VistaPreviaDeImpresionSecOcupados(listaSectoresOcupados);
            vPrevia.Show();
            this.Close();
        }
    }
}
