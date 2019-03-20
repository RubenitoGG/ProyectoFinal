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

namespace GestorAeropuerto.Ventanas.FramesAdministrador
{
    /// <summary>
    /// Lógica de interacción para FrameVuelos.xaml
    /// </summary>
    public partial class FrameVuelos : Page
    {
        VentanaAdministrador ventana;

        public FrameVuelos(VentanaAdministrador ventana)
        {
            InitializeComponent();
            this.ventana = ventana;
        }
    }
}
