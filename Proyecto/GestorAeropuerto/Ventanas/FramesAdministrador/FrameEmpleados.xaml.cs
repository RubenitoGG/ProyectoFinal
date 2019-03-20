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

            nuevo = true;

            // Cambiamos el color del menú de la ventana:
            ventana.CambiarColor("Empleado");

            // Rellenamos los combos:
            ActualizarCombos();
        }

        /// <summary>
        /// Actualiza la ListBox.
        /// </summary>
        /// <param name="aerolinena"></param>
        private void ActualizarLista(string aerolinena)
        {
            // Borramos los campos:
            BorrarCampos();

            // Borramos todos los items de la ListBox:
            this.listaEmpleados.Items.Clear();

            // Mostramos los empleados de esa Aerolínea:
            foreach (Empleado empleado in uow.EmpleadoRepositorio.Get())
            {
                if(empleado.Aerolinea.Nombre == aerolinena)
                    this.listaEmpleados.Items.Add(empleado.Nombre);
            }

            // Pasamos al modo actualizar:
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
            this.comboGenero.Items.Add("Masculino");
            this.comboGenero.Items.Add("Femenino");

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
            // Borramos todo:
            BorrarCampos();

            // Pasamos al modo crear:
            nuevo = true;
            BotonAñadir.Content = "Añadir";
        }

        /// <summary>
        /// Borrar los TextBox, ComboBox y la selección del empleado:
        /// </summary>
        private void BorrarCampos()
        {
            textoId.Text = "";
            textoNombre.Text = "";
            textoApellidos.Text = "";
            textoPais.Text = "";
            comboGenero.Text = "";
            comboCargo.Text = "";

            empleado = null;
            this.listaEmpleados.SelectedItem = null;
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
            foreach (Empleado empl in uow.EmpleadoRepositorio.Get())
            {
                if (empl.Nombre == this.listaEmpleados.SelectedItem.ToString())
                    this.empleado = empl;
            }

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
        
        /// <summary>
        /// Crea o Actualiza el Empleado y lo guarda en la Base de Datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonAñadir_Click(object sender, RoutedEventArgs e)
        {
            // Modo añadir:
            if (nuevo)
            {
                // Creamos el Empleado con los valores:
                this.empleado = new Empleado()
                {
                    Nombre = textoNombre.Text,
                    Apellidos = textoApellidos.Text,
                    Pais = textoPais.Text,
                    Genero = comboGenero.Text
                };

                // Añadimos el Cargo seleccionado:
                foreach (Cargo cargo in uow.CargoRepositorio.Get())
                {
                    if (cargo.Nombre == comboCargo.SelectedItem.ToString())
                        this.empleado.Cargo = cargo;
                }
                
                // Añadimos la Aerolínea seleccionada:
                foreach (Aerolinea aerolinea in uow.AerolineaRepositorio.Get())
                {
                    if (aerolinea.Nombre == comboAerolinea.SelectedItem.ToString())
                        this.empleado.Aerolinea = aerolinea;
                }

                // Lo guardamos en la Base de Datos:
                uow.EmpleadoRepositorio.Añadir(this.empleado);

                // Borramos todos los campos:
                BorrarCampos();
            }
            // Modo actualizar:
            else
            {

            }
        }

        /// <summary>
        /// Al cambiar de Aerolínea se muestran otros los Empleados de la seleccionada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboAerolinea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActualizarLista(this.comboAerolinea.Text);
        }

        /// <summary>
        /// Elima el empleado seleccionado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Vemos si hay un empleado seleccionado:
            if(empleado != null)
            {
                // Borramos el empleado:
                uow.EmpleadoRepositorio.Delete(this.empleado);
            }

            // Borramos los campos:
            BorrarCampos();

            // Pasamos al modo añadir:
            nuevo = true;
            BotonAñadir.Content = "Añadir";
        }
    }
}
