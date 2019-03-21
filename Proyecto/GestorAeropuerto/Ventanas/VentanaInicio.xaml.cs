using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GestorAeropuerto.Ventanas;

namespace GestorAeropuerto
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BotonAdministrador_Click(object sender, RoutedEventArgs e)
        {
            VentanaLogin nuevaVentana = new VentanaLogin(this);
            nuevaVentana.Show();
        }

        private void BotonComprar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aplicación aún en desarrollo, no disponible la reserva de vuelos.\n¡Disculpe las molestias!",
                "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
