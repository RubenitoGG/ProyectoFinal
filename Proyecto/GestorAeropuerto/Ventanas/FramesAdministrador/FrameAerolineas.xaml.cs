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

        Aerolinea aero; // Aerolínea seleccionada.

        UnitOfWork uow = new UnitOfWork();

        public FrameAerolineas(VentanaAdministrador ventana)
        {
            InitializeComponent();
            this.ventana = ventana;

            // Cambiamos el color del menú de la ventana:
            ventana.CambiarColor("Aerolinea");

            // Actualizamos las Aerolíneas:
            ActualizarAerolineas();
        }

        /// <summary>
        /// Método que actualiza las aerolíneas del ListBox.
        /// </summary>
        public void ActualizarAerolineas()
        {
            aerolineas = uow.AerolineaRepositorio.GetAll();

            listaAerolineas.Items.Clear();
            foreach (Aerolinea aerolinea in aerolineas)
            {
                listaAerolineas.Items.Add(aerolinea.Nombre);
            }
        }

        /// <summary>
        /// Saca la ventana para añadir una nueva Aerolínea.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonAñadir_Click(object sender, RoutedEventArgs e)
        {
            VentanaAñadirAerolinea nuevaVentana = new VentanaAñadirAerolinea(this);
            nuevaVentana.Show();
        }

        /// <summary>
        /// Muestra el teléfono de la Aerolínea seleccionada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listaAerolineas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Vemos si hay una Aerolínea seleccionada:
            if (listaAerolineas.SelectedItem == null)
                return;

            // Ponemos el teléfono en el TextBox:
            aero = (Aerolinea)listaAerolineas.SelectedItem;
            this.textoTelefono.Text = aero.Telefono;
        }

        /// <summary>
        /// Elimina la Aerolínea seleccionada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Comprobamos si tenemos seleccionada una Aerolínea:
            if(aero == null)
            {
                // Borramos la Aerolínea:
                uow.AerolineaRepositorio.Delete(aero);

                // Borramos la selección y el TextBox:
                aero = null;
                listaAerolineas.SelectedItem = null;
                this.textoTelefono.Text = "";

                // Mensaje de Aerolínea borra:
                MessageBox.Show("Aerolínea eliminada correctamente.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else // Si no sacamos un mensaje:
            {
                MessageBox.Show("No hay ninguna Aerolínea seleccionada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
