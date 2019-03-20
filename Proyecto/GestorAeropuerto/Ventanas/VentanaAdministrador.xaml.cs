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
using System.Windows.Shapes;
using GestorAeropuerto.Ventanas.FramesAdministrador;

namespace GestorAeropuerto.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaAdministrador.xaml
    /// </summary>
    public partial class VentanaAdministrador : Window
    {
        FrameUsuarios frameUsuarios;
        FrameAerolineas frameAerolineas;
        FrameVuelos frameVuelos;
        FrameEmpleados frameEmpleados;

        Color normal = new Color();
        Color seleccionado = new Color();

        public VentanaAdministrador()
        {
            InitializeComponent();

            // Guardamos los colores:
            GuardarColores();

            // Empezamos en la ventana de usuario:
            frameUsuarios = new FrameUsuarios(this);
            frameVentana.Content = frameUsuarios;
        }

        /// <summary>
        /// Guarda los colores de seleccionado y no seleccionado.
        /// </summary>
        private void GuardarColores()
        {
            normal.R = 0;
            normal.G = 0;
            normal.B = 0;
            normal.A = 0;

            seleccionado.R = 96;
            seleccionado.G = 168;
            seleccionado.B = 218;
            seleccionado.A = 100;
        }

        /// <summary>
        /// Borrar los colores de todo el menú.
        /// </summary>
        private void BorrarSeleccionado()
        {
            menuUsuarios.Background = new SolidColorBrush(normal);
            menuAerolineas.Background = new SolidColorBrush(normal);
            menuEmpleados.Background = new SolidColorBrush(normal);
            menuVuelos.Background = new SolidColorBrush(normal);
        }

        /// <summary>
        /// Pone el color fuerte en el menu dependiendo de donde nos encontramos.
        /// </summary>
        /// <param name="menu">Valores: Usuario, Aerolinea, Vuelo, Empleado</param>
        public void CambiarColor(string menu)
        {
            // Borrar los colores del menú:
            BorrarSeleccionado();

            // Ponemos el color dependiendo de donde estemos:
            if (menu == "Usuario")
                menuUsuarios.Background = new SolidColorBrush(seleccionado);
            else if (menu == "Aerolinea")
                menuAerolineas.Background = new SolidColorBrush(seleccionado);
            else if (menu == "Vuelo")
                menuVuelos.Background = new SolidColorBrush(seleccionado);
            else if (menu == "Empleado")
                menuEmpleados.Background = new SolidColorBrush(seleccionado);
        }

        /// <summary>
        /// Nos movemos al Frame de Usuarios.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuUsuarios_Click(object sender, RoutedEventArgs e)
        {
            frameUsuarios = new FrameUsuarios(this);
            frameVentana.Content = frameUsuarios;
        }

        /// <summary>
        /// Nos movemos al Frame de Aerolíneas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuAerolineas_Click(object sender, RoutedEventArgs e)
        {
            frameAerolineas = new FrameAerolineas(this);
            frameVentana.Content = frameAerolineas;
        }

        /// <summary>
        /// Nos movemos al Frame de Vuelos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuVuelos_Click(object sender, RoutedEventArgs e)
        {
            frameVuelos = new FrameVuelos(this);
            frameVentana.Content = frameVuelos;
        }

        /// <summary>
        /// Nos movemos al Frame de Empleados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuEmpleados_Click(object sender, RoutedEventArgs e)
        {
            frameEmpleados = new FrameEmpleados(this);
            frameVentana.Content = frameEmpleados;
        }

        
    }
}
