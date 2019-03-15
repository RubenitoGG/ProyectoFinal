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
using System.Windows.Shapes;
using GestorAeropuerto.DAL;
using GestorAeropuerto.Model;

namespace GestorAeropuerto.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaAñadirAerolinea.xaml
    /// </summary>
    public partial class VentanaAñadirAerolinea : Window
    {
        UnitOfWork uow = new UnitOfWork();

        public VentanaAñadirAerolinea()
        {
            InitializeComponent();
        }

        private void BotonAñadir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
