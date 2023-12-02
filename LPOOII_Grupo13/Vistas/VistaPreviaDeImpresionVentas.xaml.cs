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
    /// Interaction logic for VistaPreviaDeImpresionVentas.xaml
    /// </summary>
    public partial class VistaPreviaDeImpresionVentas : Window
    {
        //public VistaPreviaDeImpresionVentas()
        //{
        //    InitializeComponent();
        //}
        ObservableCollection<Ventas> listaVentas = new ObservableCollection<Ventas>();

        public VistaPreviaDeImpresionVentas(ObservableCollection<Ventas> listaVentasRealizadas)
        {
            InitializeComponent();
            listaVentas = listaVentasRealizadas;
            list.ItemsSource = listaVentas;
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

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            if (pdlg.ShowDialog() == true)
            {
                //pdlg.PrintDocument(((IDocumentPaginatorSource)DocMain).DocumentPaginator, "Imprimir");
                pdlg.PrintVisual(list, "Imprimir");
            }
        }
    }
}
