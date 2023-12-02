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
    /// Interaction logic for VistaPreviaDeImpresionSecOcupados.xaml
    /// </summary>
    public partial class VistaPreviaDeImpresionSecOcupados : Window
    {
        //public VistaPreviaDeImpresionSecOcupados()
        //{
        //    InitializeComponent();
        //}

        private ObservableCollection<Util.SectoresOcupados> secOcupados;

        public VistaPreviaDeImpresionSecOcupados(ObservableCollection<Util.SectoresOcupados> sectoresOcupados)
        {
            InitializeComponent();
            secOcupados = sectoresOcupados;
            list.ItemsSource = secOcupados;
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
