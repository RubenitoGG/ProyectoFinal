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
using GestorAeropuerto.Model;
using GestorAeropuerto.DAL;
using GestorAeropuerto.Ventanas;

namespace GestorAeropuerto.Ventanas.FramesAdministrador
{
    /// <summary>
    /// Lógica de interacción para FrameAerolineas.xaml
    /// </summary>
    public partial class FrameAerolineas : Page
    {
        VentanaAdministrador ventana; // Referencia a la ventana dónde está el frame.
        List<Aerolinea> aerolineas; // Lista de todas las aerolíneas.

        UnitOfWork uow = new UnitOfWork();

        public FrameAerolineas(VentanaAdministrador ventana)
        {
            InitializeComponent();
            this.ventana = ventana;

            // Cambiamos el color del menú de la ventana:
            ventana.CambiarColor("Aerolinea");
            MostrarAerolineas();
        }

        private void MostrarAerolineas()
        {
            aerolineas = uow.AerolineaRepositorio.GetAll();

            listaAerolineas.Items.Clear();
            foreach (Aerolinea aerolinea in aerolineas)
            {
                listaAerolineas.Items.Add(aerolinea.Nombre);
            }
        }

        private void BotonAñadir_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
