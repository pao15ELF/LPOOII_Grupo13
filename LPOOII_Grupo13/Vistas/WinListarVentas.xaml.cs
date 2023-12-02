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
using Util;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinListarVentas.xaml
    /// </summary>
    public partial class WinListarVentas : Window
    {
        ObservableCollection<Ticket> listarVentas = new ObservableCollection<Ticket>();
        ObservableCollection<Ventas> listarVentasGrilla = new ObservableCollection<Ventas>();

        Decimal totalVentas;

        public WinListarVentas()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
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

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            DateTime? fechaIni = dpFechaInicio.SelectedDate;
            DateTime? fechaFi = dpFechaFinal.SelectedDate;
            if (fechaIni.HasValue && fechaFi.HasValue)
            {
                DateTime fechaInicio = fechaIni.Value;
                DateTime fechaFinal = fechaFi.Value;
                listarVentas = TrabajarTicket.traerTicketVentas(fechaInicio, fechaFinal);
                cargarListview();
                lblTotal.Content = "Total de Ventas en el Rango de Fechas:  " + totalVentas.ToString();
                lblTotal.UpdateLayout();
                
            }
            else { MessageBox.Show("No se selecciono fechas"); }
        }

        private void cargarListview(){
            foreach(var item in listarVentas){
                Ventas venta = new Ventas();

                venta.NTicket = item.Tick_TicketNro;
                venta.Sector = item.Tick_SectorCodigo;
                venta.Zona = calcularZona(item.Tick_SectorCodigo);
                venta.FecHoraEntrada = item.Tick_FechaHoraEnt;
                venta.FechaHoraSalida = item.Tick_FechaHoraSal;
                venta.Cliente = nombreCliente(item.Tick_ClienteDni);
                venta.Patente = item.Tick_Patente;
                venta.TipoVehiculo = tipoVehiculo(item.Tick_TVCodigo);
                venta.Duracion = item.Tick_Duracion;
                venta.Tarifa = item.Tick_Tarifa;
                venta.Total = item.Tick_Total;

                totalVentas = totalVentas+ item.Tick_Total;
                listarVentasGrilla.Add(venta);
            }
            lvwListaVentas.ItemsSource = listarVentasGrilla;
            DataContext = this;
        }



        private string tipoVehiculo(int codigo)
        {
            TipoVehiculo tipoVehiculo = TrabajarTipoVehiculo.buscarTipoVehiculoXCodigo(codigo);
            return tipoVehiculo.TypV_Descripcion;
        }

        private string nombreCliente(int dni)
        {
            Cliente cliente = TrabajarCliente.buscarCliente(dni);

            return cliente.Cli_Apellido1 + ", " + cliente.Cli_Nombre1;
        }

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

        private void btnVistaPrevia_Click(object sender, RoutedEventArgs e)
        {
            VistaPreviaDeImpresionVentas vPrevia = new VistaPreviaDeImpresionVentas(listarVentasGrilla);
            vPrevia.Show();
            this.Close();
        }

    }
}
