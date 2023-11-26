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
    /// Interaction logic for VistaPreviaDeImpresion.xaml
    /// </summary>
    public partial class VistaPreviaDeImpresion : Window
    {
        
        private CollectionViewSource filtrada;

       
        //public VistaPreviaDeImpresion()
        //{
        //    InitializeComponent();
        //    vistaColeccionFiltrada = Resources["FILTRO_USERNAME"] as CollectionViewSource;
        //}


        public VistaPreviaDeImpresion(CollectionViewSource vistaColeccionFiltrada)
        {
            InitializeComponent();
            this.filtrada = vistaColeccionFiltrada;
            list.ItemsSource = filtrada.View;

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
                pdlg.PrintDocument(((IDocumentPaginatorSource)DocMain).DocumentPaginator, "Imprimir");
            }
        }
        

       
    }
}
