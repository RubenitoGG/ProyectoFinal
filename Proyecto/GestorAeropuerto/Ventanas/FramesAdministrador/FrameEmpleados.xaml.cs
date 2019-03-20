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
        Empleado empleado;

        bool nuevo; // Variable para cambiar entre Añadir y Actualizar.

        public FrameEmpleados(VentanaAdministrador ventana)
        {
            InitializeComponent();
            this.ventana = ventana;

            // Cambiamos el color del menú de la ventana:
            ventana.CambiarColor("Empleado");

            // Rellenamos los combos:
            ActualizarCombos();
        }

        /// <summary>
        /// Actualiza los ComboBox con sus valores.
        /// </summary>
        private void ActualizarCombos()
        {
            // Guardamos todas la Aerolíneas:
            List<Aerolinea> aerolineas = uow.AerolineaRepositorio.Get();

            // Guardamos todos los Cargos:
            List<Cargo> cargos = uow.CargoRepositorio.Get();

            // Ponemos los nombres de las Aerolíneas en el ComboBox:
            foreach (Aerolinea aerolinea in aerolineas)
            {
                this.comboAerolinea.Items.Add(aerolinea.Nombre);
            }

            // Guardamos los géneros en el ComboBox:
            this.comboGenero.Items.Add("Hombre");
            this.comboGenero.Items.Add("Mujer");
            this.comboGenero.Items.Add("Indefinido");

            // Ponemos los nombres de los Cargos en el ComboBox:
            foreach (Cargo cargo in cargos)
            {
                this.comboCargo.Items.Add(cargo.Nombre);
            }
        }

        /// <summary>
        /// Borra todos los textos y la selección del empleado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonBorrar_Click(object sender, RoutedEventArgs e)
        {
            // Pasamos al modo crear:
            nuevo = true;
            BotonAñadir.Content = "Añadir";
        }

        /// <summary>
        /// Guardamos el usuario seleccionado y ponemos sus datos en los TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListaUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Si no hay usuario seleccionado salimos:
            if (this.listaEmpleados.SelectedItem == null)
                return;

            // Guardamos el Empleado seleccionado:
            empleado = (Empleado)this.listaEmpleados.SelectedItem;

            // Ponemos en los TextBox y ComboBox los datos del usuario
            textoId.Text = empleado.EmpleadoId.ToString();
            textoNombre.Text = empleado.Nombre;
            textoApellidos.Text = empleado.Apellidos;
            textoPais.Text = empleado.Pais;
            comboCargo.Text = empleado.Cargo.Nombre;
            comboGenero.Text = empleado.Genero;

            // Pasamos al modo de editar:
            nuevo = false;
            BotonAñadir.Content = "Actualizar";
        }

        /// <summary>
        /// Pasamos al Frame de Cargos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonCargo_Click(object sender, RoutedEventArgs e)
        {
            FrameCargos f = new FrameCargos(ventana);
            ventana.frameVentana.Content = f;
        }
    }
}
