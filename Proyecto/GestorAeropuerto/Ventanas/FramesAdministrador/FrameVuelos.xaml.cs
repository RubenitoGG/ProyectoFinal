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
using GestorAeropuerto.DAL;
using GestorAeropuerto.Model;

namespace GestorAeropuerto.Ventanas.FramesAdministrador
{
    /// <summary>
    /// Lógica de interacción para FrameVuelos.xaml
    /// </summary>
    public partial class FrameVuelos : Page
    {
        VentanaAdministrador ventana;
        UnitOfWork uow = new UnitOfWork();
        PropertyValidateModel validador = new PropertyValidateModel();

        Vuelo vuelo;
        bool nuevo;

        public FrameVuelos(VentanaAdministrador ventana)
        {
            InitializeComponent();
            this.ventana = ventana;

            ventana.CambiarColor("Vuelo");

            // Rellenamos los combos:
            RellenarCombos();

            nuevo = true;
        }

        /// <summary>
        /// Rellena el ComboBox de Aerolíneas y de los de horas.
        /// </summary>
        private void RellenarCombos()
        {
            // Rellenamos el ComboBox de Aerolíneas:
            foreach (Aerolinea aero in uow.AerolineaRepositorio.Get())
            {
                comboAerolineas.Items.Add(aero.Nombre);
            }

            // Rellenamos los ComboBox de las horas:
            for (int i = 0; i < 24; i++)
            {
                comboBoxSalida.Items.Add(i + ":00");
                comboBoxSalida.Items.Add(i + ":30");

                comboBoxLlegada.Items.Add(i + ":00");
                comboBoxLlegada.Items.Add(i + ":30");
            }
        }

        private void ActualizarListas()
        {
            if (comboAerolineas.SelectedItem == null)
                return;

            this.listaVuelos.Items.Clear();

            foreach (Vuelo vuelo in uow.VueloRepositorio.Get())
            {
                if (vuelo.Aerolinea.Nombre == comboAerolineas.SelectedItem.ToString())
                    this.listaVuelos.Items.Add(vuelo.VueloId + ": " + vuelo.Origen + " - " + vuelo.Destino);
            }
        }

        private void ComboAerolineas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BorrarTodo();

            if (comboAerolineas.SelectedItem == null)
                return;

            ActualizarListas();
        }

        private void BorrarTodo()
        {
            textoId.Text = "";
            textoOrigen.Text = "";
            textoDestino.Text = "";
            comboBoxSalida.Text = "";
            comboBoxLlegada.Text = "";

            listaVuelos.SelectedItem = null;

            nuevo = true;
            BotonAñadir.Content = "Añadir";
        }

        private void BotonNuevo_Click(object sender, RoutedEventArgs e)
        {
            BorrarTodo();
        }

        /// <summary>
        /// Añade nuevos Vuelos a la Base de Datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonAñadir_Click(object sender, RoutedEventArgs e)
        {
            // Comprobamos los ComboBox:
            if (comboBoxSalida.SelectedItem == null || comboBoxLlegada.SelectedItem == null || comboAerolineas.SelectedItem == null)
            {
                MessageBox.Show("Tienes que completar los ComboBox", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (nuevo) // Modo crear:
            {
                // Creamos el vuelo con sus variables:
                vuelo = new Vuelo()
                {
                    Origen = textoOrigen.Text,
                    Destino = textoDestino.Text,
                    Salida = comboBoxSalida.SelectedItem.ToString(),
                    Llegada = comboBoxLlegada.SelectedItem.ToString()
                };

                // Añadimos la Aerolínea:
                foreach (Aerolinea aero in uow.AerolineaRepositorio.Get())
                {
                    if (aero.Nombre == comboAerolineas.SelectedItem.ToString())
                        vuelo.Aerolinea = aero;
                }

                // Comprobamos con el validador:
                if (validador.errores(vuelo) == "")
                {
                    // Agregamos el jugador a la Base de Datos:
                    uow.VueloRepositorio.Añadir(vuelo);
                    MessageBox.Show("Vuelo añadido correctamente", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Datos incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else // Modo actualizar:
            {
                // Cambiamos los valores:
                this.vuelo.Origen = textoOrigen.Text;
                this.vuelo.Destino = textoDestino.Text;
                this.vuelo.Salida = comboBoxSalida.SelectedItem.ToString();
                this.vuelo.Llegada = comboBoxLlegada.SelectedItem.ToString();

                // Comprobamos con el validador:
                if (validador.errores(vuelo) == "")
                {
                    uow.VueloRepositorio.Update(this.vuelo);
                    MessageBox.Show("Vuelo añadido correctamente", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Datos incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ventana.frameVentana.Content = new FrameVuelos(ventana);
                }
            }
            
            ActualizarListas();
        }

        private void ListaVuelos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaVuelos.SelectedItem == null)
                return;

            foreach (Vuelo vuelo in uow.VueloRepositorio.Get())
            {
                if (vuelo.VueloId.ToString() == listaVuelos.SelectedItem.ToString().Split(':')[0])
                    this.vuelo = vuelo;
            }

            textoId.Text = vuelo.VueloId.ToString();
            textoOrigen.Text = vuelo.Origen;
            textoDestino.Text = vuelo.Destino;
            comboBoxSalida.Text = vuelo.Salida;
            comboBoxLlegada.Text = vuelo.Llegada;

            this.nuevo = false;
            BotonAñadir.Content = "Actualizar";
        }

        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (this.listaVuelos.SelectedItem != null)
            {
                uow.VueloRepositorio.Delete(this.vuelo);

                ActualizarListas();
                BorrarTodo();
            }
            else
                MessageBox.Show("No hay ningún Vuelo seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BotonAviones_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aplicación aún en desarrollo, característica aún no disponible.",
                "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BotonBilletes_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aplicación aún en desarrollo, característica aún no disponible.",
                "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BotonAyuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("- Para añadir un nuevo Vuelo: tienes que completar todos los datos y pulsar en el botón 'Añadir'.\n" +
               "- Para actualizar un Vuelo: selecciona uno en la lista de la izquierda, cambia los datos y pulsa el botón 'Actualizar'.\n" +
               "- Para borrar un Vuelo: selecciona uno en la lista de la izquierda y pulsa el botón 'Eliminar'.\n" +
               "- El botón de 'Aviones' te mueve a la pestaña de Aviones dónde puedes añadir y eliminar aviones a ese Vuelo (FUNCIÓN AÚN NO DISPONIBLE).\n" +
               "- El botón de 'Billetes' te mueve a la pestaña de Billetes dónde puedes añadir y eliminar Billetes a ese Vuelo (FUNCIÓN AÚN NO DISPONIBLE).",
               "Ayuda", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}
