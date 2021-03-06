﻿using System;
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

            // Buscamos la Aerolínea:
            foreach (Aerolinea aerolinea in aerolineas)
            {
                if (aerolinea.Nombre == listaAerolineas.SelectedItem.ToString())
                    this.aero = aerolinea;
            }

            // Ponemos el teléfono en el TextBox:
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
            if (aero != null)
            {
                // Mensaje para confirmar la eliminación:
                if (MessageBox.Show("¿Estás seguro?\nSi borras esta Aerolínea se borrarán todos los Empleados y Vuelos asignados a la misma.",
                    "Info", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    // Borramos los Vuelos que pertenecen a esa Aerolínea:
                    foreach (Vuelo vuelo in uow.VueloRepositorio.Get())
                    {
                        if (vuelo.Aerolinea == aero)
                            uow.VueloRepositorio.Delete(vuelo);
                    }

                    // Borramos los Empleados que pertenecen a esa Aerolínea:
                    foreach (Empleado empleado in uow.EmpleadoRepositorio.Get())
                    {
                        if (empleado.Aerolinea == aero)
                            uow.EmpleadoRepositorio.Delete(empleado);
                    }

                    // Borramos la Aerolínea:
                    uow.AerolineaRepositorio.Delete(aero);

                    // Borramos la selección y el TextBox:
                    aero = null;
                    listaAerolineas.SelectedItem = null;
                    this.textoTelefono.Text = "";

                    // Mensaje de Aerolínea borra:
                    MessageBox.Show("Aerolínea eliminada correctamente.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Actualizamos la lista de Aerolíneas:
                    ActualizarAerolineas();
                }
            }
            else // Si no sacamos un mensaje:
            {
                MessageBox.Show("No hay ninguna Aerolínea seleccionada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BotonAyuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("- Para añadir una nueva Aerolínea: pulsa en el botón 'Añadir' y se abrirá una ventana para introducir los datos.\n" +
                           "- Para borrar una Aerolínea: selecciona una en la lista de la izquierda y pulsa el botón 'Eliminar'.\n",
                           "Ayuda", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}
