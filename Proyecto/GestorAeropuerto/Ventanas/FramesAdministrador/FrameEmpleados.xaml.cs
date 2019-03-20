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
    /// Lógica de interacción para FrameEmpleados.xaml
    /// </summary>
    public partial class FrameEmpleados : Page
    {
        VentanaAdministrador ventana; // Referencia a la ventana donde se encuentra el frame.
        UnitOfWork uow = new UnitOfWork();

        public FrameEmpleados(VentanaAdministrador ventana)
        {
            InitializeComponent();
            this.ventana = ventana;

            // Cambiamos el color del menú de la ventana:
            ventana.CambiarColor("Vuelo");
        }

        /// <summary>
        /// Rellena los ComboBox con sus valores.
        /// </summary>
        private void RellenarCombos()
        {
            // Guardamos todas la Aerolíneas:
            List<Aerolinea> aerolineas = uow.AerolineaRepositorio.Get();

            // Ponemos los nombres de las Aerolíneas en el ComboBox:
            foreach (Aerolinea aerolinea in aerolineas)
            {
                this.comboAerolinea.Items.Add(aerolinea.Nombre);
            }

            // Guardamos los géneros en el ComboBox:
            this.comboGenero.Items.Add("Hombre");
            this.comboGenero.Items.Add("Mujer");
            this.comboGenero.Items.Add("");
        }
    }
}
