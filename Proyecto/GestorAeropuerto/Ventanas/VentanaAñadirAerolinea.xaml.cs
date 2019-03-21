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
using GestorAeropuerto.DAL;
using GestorAeropuerto.Model;
using GestorAeropuerto.Ventanas.FramesAdministrador;

namespace GestorAeropuerto.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaAñadirAerolinea.xaml
    /// </summary>
    public partial class VentanaAñadirAerolinea : Window
    {
        UnitOfWork uow = new UnitOfWork();
        PropertyValidateModel validador = new PropertyValidateModel();

        FrameAerolineas frame;

        public VentanaAñadirAerolinea(FrameAerolineas frame)
        {
            InitializeComponent();
            this.frame = frame;
        }

        private void BotonAñadir_Click_1(object sender, RoutedEventArgs e)
        {
            // Comprobamos campos:
            if(string.IsNullOrEmpty(textoNombre.Text.Trim()) || string.IsNullOrEmpty(textoTelefono.Text.Trim()))
            {
                MessageBox.Show("Faltan campos por cubrir", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                textoNombre.Text = "";
                textoTelefono.Text = "";
                return;
            }

            // Comprobamos teléfono:
            int telefono;

            if (!int.TryParse(this.textoTelefono.Text, out telefono) || this.textoTelefono.Text.Length != 9)
            {
                MessageBox.Show("Teléfono incorrecto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Creamos nueva Aerolínea:
            Aerolinea aero = new Aerolinea
            {
                Nombre = this.textoNombre.Text,
                Telefono = this.textoTelefono.Text
            };

            // Comprobamos con el validador:
            if (validador.errores(aero) == "")
            {
                // Añadir Aerolínea:
                uow.AerolineaRepositorio.Añadir(aero);

                // Actualizar en la principal:
                frame.ActualizarAerolineas();

                // Cerrar Ventana:
                this.Close();
            }
            else
                MessageBox.Show("Datos incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BotonCancelar_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar Ventana:
            this.Close();
        }
    }
}
