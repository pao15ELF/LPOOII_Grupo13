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
    /// Interaction logic for WinTicketRegistrarSalida.xaml
    /// </summary>
    public partial class WinTicketRegistrarSalida : Window
    {
        public WinTicketRegistrarSalida()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txbNTicket.Text = "Ticket:   #" + Util.Util.getTicketSalida().Tick_TicketNro.ToString();
            txbNombreCliente.Text = "Cliente:   " + nombreCliente(Util.Util.getTicketSalida().Tick_ClienteDni);
            txbPatente.Text = "Patente:   " + Util.Util.getTicketSalida().Tick_Patente;
            txbHoraIngreso.Text = "Fecha Ingreso:   " + Util.Util.getTicketSalida().Tick_FechaHoraEnt.ToString();
            txbTipoVehiculo.Text = "Tipo Vehiculo:   " + tipoVehiculo(Util.Util.getTicketSalida().Tick_TVCodigo);
            txbTarifa.Text = "Tarifa:   $" + Util.Util.getTicketSalida().Tick_Tarifa.ToString();

            txbHoraSalida.Text = "Fecha Salida:  " + Util.Util.getTicketSalida().Tick_FechaHoraSal.ToString();
            txbTiempoTranscurrido.Text = "Tiempo Transcurrido:  " + Util.Util.getTicketSalida().Tick_Duracion;
            txbHsContabilizadas.Text = "Horas Contabilizadas:   " + calcularHoraContabilizadas();
            txbTotal.Text = "Total:  $" + Util.Util.getTicketSalida().Tick_Total.ToString();

            txbUsuario.Text = "Usuario:   " + Util.Util.getUsuario().User_UserName;
        }

        private string tipoVehiculo(int codigoVehiculo)
        {
            string tipoVeh;
            TipoVehiculo vehiculo = new TipoVehiculo();

            vehiculo = TrabajarTipoVehiculo.buscarTipoVehiculoXCodigo(codigoVehiculo);
            tipoVeh = vehiculo.TypV_Descripcion;

            return tipoVeh;
        }

        private string nombreCliente(int dniCliente)
        {
            string nombreCompleto;
            Cliente cliente = new Cliente();

            cliente = TrabajarCliente.buscarCliente(dniCliente);
            nombreCompleto = cliente.Cli_Apellido1 + ", " + cliente.Cli_Nombre1;

            return nombreCompleto;
        }

        private string calcularHoraContabilizadas()
        {
            int cantHoras;
            DateTime entrada = Util.Util.getTicketSalida().Tick_FechaHoraEnt;
            DateTime salida = Util.Util.getTicketSalida().Tick_FechaHoraSal;

            TimeSpan diferencia = salida.Subtract(entrada);

            int horas = diferencia.Hours;
            int minutos = diferencia.Minutes;

            if (horas > 0)
            {
                if (minutos <= 10)
                {
                    cantHoras = horas;
                }
                else
                {
                    cantHoras = horas+1;
                }
            }
            else
            {
                cantHoras = 1;
            }
            return cantHoras.ToString();
        }

    }
}
