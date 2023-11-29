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
    /// Interaction logic for WinAcercaDeNosotros.xaml
    /// </summary>
    public partial class WinAcercaDeNosotros : Window
    {
        public WinAcercaDeNosotros()
        {
            InitializeComponent();
            mediaVideo.Source = new Uri("Video/SistemaEstacionamiento.mp4", UriKind.Relative); 
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

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaVideo.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaVideo.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaVideo.Stop();
            mediaVideo.Play();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
