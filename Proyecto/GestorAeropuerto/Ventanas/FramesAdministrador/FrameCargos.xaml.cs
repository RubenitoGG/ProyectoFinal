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
        PropertyValidateModel validador = new PropertyValidateModel();

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
                double money;

                // Comprobamos si los campos son correctos:
                if (!double.TryParse(textoSueldo.Text, out money))
                {
                    MessageBox.Show("Valor de dinero incorrecto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Creamos el nuevo Cargo:
                this.cargo = new Cargo()
                {
                    Nombre = textoNombre.Text,
                    Sueldo = Convert.ToDouble(textoSueldo.Text)
                };

                // Comprobamos con el validador:
                if (validador.errores(cargo) == "")
                {
                    // Lo añadimos a la Base de Datos:
                    uow.CargoRepositorio.Añadir(this.cargo);

                    // Mensaje de Cargo añadido:
                    MessageBox.Show("Cargo añadido correctamente.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Datos incorrectos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // Borramos los campos:
                BorrarCampos();

                // Actualizamos la ListBox:
                ActualizarLista();
            }
            // Modo Actualizar:
            else
            {
                double money;

                // Comprobamos si los campos son correctos:
                if (!double.TryParse(textoSueldo.Text, out money))
                {
                    MessageBox.Show("Valor de dinero incorrecto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Cambiamos los valores del Cargo seleccionado:
                this.cargo.Nombre = textoNombre.Text;
                this.cargo.Sueldo = Convert.ToDouble(textoSueldo.Text);

                // Comprobamos con el validador:
                if (validador.errores(this.cargo) == "")
                {
                    // Actualizamos la Base de Datos:
                    uow.CargoRepositorio.Update(this.cargo);

                    // Mensaje de Cargo actualizado:
                    MessageBox.Show("Cargo actualizado correctamente.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Datos incorrectos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ventana.frameVentana.Content = new FrameCargos(ventana);
                }
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
            if (this.cargo != null)
            {
                // Mensaje para confirmar la eliminación:
                if (MessageBox.Show("¿Estás seguro?\nSi borras este Cargo se borrarán todos los Empleados asignados al mismo.",
                    "Info", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    // Borramos los Empleados que tengan ese Cargo:
                    foreach (Empleado empleado in uow.EmpleadoRepositorio.Get())
                    {
                        if (empleado.Cargo == this.cargo)
                            uow.EmpleadoRepositorio.Delete(empleado);
                    }

                    // Borramos el cargo:
                    uow.CargoRepositorio.Delete(cargo);

                    // Mensaje de Cargo eliminado:
                    MessageBox.Show("Cargo eliminado correctamente.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Actualizamos la ListBox:
                    ActualizarLista();
                }
            }
            else
                MessageBox.Show("No hay ningún Cargo seleccionado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BotonAyuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("- Para añadir un nuevo Cargo: tienes que completar todos los datos y pulsar en el botón 'Añadir'.\n" +
               "- Para actualizar un Cargo: selecciona uno en la lista de la izquierda, cambia los datos y pulsa el botón 'Actualizar'.\n" +
               "- Para borrar un Cargo: selecciona uno en la lista de la izquierda y pulsa el botón 'Eliminar'.\n" +
               "- El botón de 'Nuevo' deselecciona el Cargo seleccionado para poder crear uno nuevo.", "Ayuda", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}
