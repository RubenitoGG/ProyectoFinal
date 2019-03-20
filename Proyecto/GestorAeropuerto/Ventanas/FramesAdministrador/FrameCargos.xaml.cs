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

namespace GestorAeropuerto.Ventanas.FramesAdministrador
{
    /// <summary>
    /// Lógica de interacción para FrameCargos.xaml
    /// </summary>
    public partial class FrameCargos : Page
    {
        VentanaAdministrador ventana; // Referencia a la ventana principal.
        Cargo cargo;

        bool nuevo; // Variable para saber si vamos a Añadir o Modificar.

        UnitOfWork uow = new UnitOfWork();

        public FrameCargos(VentanaAdministrador ventana)
        {
            InitializeComponent();
            this.ventana = ventana;

            // Empezamos en el modo Añadir:
            nuevo = true;

            // Actualizamos la ListBox:
            ActualizarLista();
        }

        /// <summary>
        /// Actualiza la ListBox con los Cargos que hay en la Base de Datos.
        /// </summary>
        private void ActualizarLista()
        {
            // Guardamos todos los cargos:
            List<Cargo> cargos = uow.CargoRepositorio.Get();

            // Borramos toda la ListBox:
            this.listaCargos.Items.Clear();

            // Ponemos todos los cargos en la ListBox:
            foreach (Cargo cargo in cargos)
            {
                this.listaCargos.Items.Add(cargo.Nombre);
            }
        }

        /// <summary>
        /// Volvemos al Frame de Empleados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonVolver_Click(object sender, RoutedEventArgs e)
        {
            FrameEmpleados f = new FrameEmpleados(ventana);
            this.ventana.frameVentana.Content = f;
        }

        /// <summary>
        /// Borra todos los TextBox y el Cargo seleccionado.
        /// </summary>
        private void BorrarCampos()
        {

            // Borramos la referencia al cargo:
            cargo = null;

            // Borramos los TextBox:
            textoId.Text = "";
            textoNombre.Text = "";
            textoSueldo.Text = "";

            // Borramos, de la lista, el Cargo seleccionado:
            this.listaCargos.SelectedItem = null;

            // Pasamos al modo añadir:
            nuevo = true;
            BotonAñadir.Content = "Añadir";
        }

        /// <summary>
        /// Borramos todo..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonNuevo_Click(object sender, RoutedEventArgs e)
        {
            BorrarCampos();
        }

        /// <summary>
        /// Pone en los TextBox los datos del Cargo seleccionado y pasa al modo Actualizar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListaCargos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Si no hay ningún cargo seleccionado volvemos:
            if (this.listaCargos.SelectedItem == null)
                return;

            // Guardamos la lista de todos los Cargos:
            List<Cargo> cargos = uow.CargoRepositorio.Get();

            // Buscamos entre todos los cargos el seleccionado:
            foreach (Cargo c in cargos)
            {
                // Guardamos el que coincida con el nombre:
                if (c.Nombre == this.listaCargos.SelectedItem.ToString())
                    this.cargo = c;
            }

            // Ponemos los datos en los TextBox:
            textoId.Text = cargo.CargoId.ToString();
            textoNombre.Text = cargo.Nombre;
            textoSueldo.Text = cargo.Sueldo.ToString();

            // Pasamos al modo Actualizar:
            nuevo = false;
            BotonAñadir.Content = "Actualizar";
        }

        /// <summary>
        /// Crea o Actualiza un Cargo y actualiza la Base de Datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonAñadir_Click(object sender, RoutedEventArgs e)
        {
            // Modo Añadir:
            if (nuevo)
            {
                // Comprobamos si los campos son correctos:

                // Creamos el nuevo Cargo:
                this.cargo = new Cargo()
                {
                    Nombre = textoNombre.Text,
                    Sueldo = Convert.ToDouble(textoSueldo.Text)
                };

                // Lo añadimos a la Base de Datos:
                uow.CargoRepositorio.Añadir(this.cargo);

                // Mensaje de Cargo añadido:
                MessageBox.Show("Cargo añadido correctamente.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                // Borramos los campos:
                BorrarCampos();

                // Actualizamos la ListBox:
                ActualizarLista();
            }
            // Modo Actualizar:
            else
            {
                // Cambiamos los valores del Cargo seleccionado:
                this.cargo.Nombre = textoNombre.Text;
                this.cargo.Sueldo = Convert.ToDouble(textoSueldo.Text);

                // Actualizamos la Base de Datos:
                uow.CargoRepositorio.Update(this.cargo);

                // Mensaje de Cargo actualizado:
                MessageBox.Show("Cargo actualizado correctamente.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                // Actualizamos la ListBox:
                ActualizarLista();
            }

        }

        /// <summary>
        /// Elimina el Cargo seleccionado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Si tenemos un cargo seleccionado:
            if(this.cargo != null)
            {
                // Borramos el cargo:
                uow.CargoRepositorio.Delete(cargo);

                // Mensaje de Cargo eliminado:
                MessageBox.Show("Cargo eliminado correctamente.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                // Actualizamos la ListBox:
                ActualizarLista();
            }
        }
    }
}
