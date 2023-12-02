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
    /// Interaction logic for WinTicketRegistrarEntrada.xaml
    /// </summary>
    public partial class WinTicketRegistrarEntrada : Window
    {
        public WinTicketRegistrarEntrada()
        {
            InitializeComponent();
            //Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txbNTicket.Text= "Ticket:   #"+Util.Util.getTicketEntrada().Tick_TicketNro.ToString();
            txbNombreCliente.Text = "Cliente:   " + nombreCliente(Util.Util.getTicketEntrada().Tick_ClienteDni);
            txbPatente.Text = "Patente:   "+Util.Util.getTicketEntrada().Tick_Patente;
            txbHoraIngreso.Text = "Fecha Ingreso:   " + Util.Util.getTicketEntrada().Tick_FechaHoraEnt.ToString();
            txbTipoVehiculo.Text = "Tipo Vehiculo:   " + tipoVehiculo(Util.Util.getTicketEntrada().Tick_TVCodigo);
            txbTarifa.Text = "Tarifa:   $" + Util.Util.getTicketEntrada().Tick_Tarifa.ToString();

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
    }
}
