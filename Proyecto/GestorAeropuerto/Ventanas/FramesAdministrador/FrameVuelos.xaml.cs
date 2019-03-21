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
            if (nuevo)
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

                // Agregamos el jugador a la Base de Datos:
                uow.VueloRepositorio.Añadir(vuelo);
            }
            else
            {
                this.vuelo.Origen = textoOrigen.Text;
                this.vuelo.Destino = textoDestino.Text;
                this.vuelo.Salida = comboBoxSalida.SelectedItem.ToString();
                this.vuelo.Llegada = comboBoxLlegada.SelectedItem.ToString();

                uow.VueloRepositorio.Update(this.vuelo);
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
            if(this.listaVuelos.SelectedItem != null)
                uow.VueloRepositorio.Delete(this.vuelo);

            ActualizarListas();
            BorrarTodo();
        }
    }
}
