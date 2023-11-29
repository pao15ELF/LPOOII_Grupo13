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
    /// Interaction logic for WinTicketRegistrarEntrada.xaml
    /// </summary>
    public partial class WinTicketRegistrarEntrada : Window
    {
        public WinTicketRegistrarEntrada()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txbEmpresa.Text = "Balut Srl";
            txbDireccion.Text = "Leandro Alem";
            txbTelefono.Text = "4515-1515";
            txbEmail.Text = "prueba@gmail.com";
            txbPasajeNumero.Text += "10";
            txbFechaOperacion.Text += "01/10/2016";
            txbFechaHoraPartida.Text += "04/10/2016";
            txbOrigen.Text += "Jujuy";
            txbDestino.Text += "Salta";
            txbTipoServicio.Text += "Semi-cama";
            txbButaca.Text += "20";
            txbPrecio.Text += "100";
            txbCliente.Text += "Ramon Diaz";
            txbUsuario.Text += "Sixto" + " " + "Alberto";
        }
    }
}
