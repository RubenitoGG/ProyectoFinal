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
using GestorAeropuerto.Ventanas;
using GestorAeropuerto.Model;
using GestorAeropuerto.DAL;

namespace GestorAeropuerto.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaLogin.xaml
    /// </summary>
    public partial class VentanaLogin : Window
    {
        UnitOfWork uow =  new UnitOfWork();
        MainWindow main; // Referencia para cerrar la ventana al iniciar sesión correctamente.

        public VentanaLogin(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void BotonEntrar_Click(object sender, RoutedEventArgs e)
        {
            Usuario u = new Usuario
            {
                Nombre = textoUsuario.Text,
                Password = textoContraseña.Password
            };

            // Cogemos la lista de usuarios:
            List<Usuario> usuarios = uow.UsuarioRepositorio.Get();

            bool bien = false; // Variable para comprobar usuarios:

            // Buscamos en todos los usuarios:
            foreach (Usuario usuario in usuarios)
            {
                if (u.Nombre == usuario.Nombre && u.Password == usuario.Password) // Nombre y/o contraseña correctos.
                {
                    bien = true;
                }
            }

            // Comprobamos si entramos o no:
            if (bien)

            {// Abrir ventana Administrador:
                VentanaAdministrador nuevaVentana = new VentanaAdministrador();
                nuevaVentana.Show();

                // Cerrar ventanas:
                main.Close();
                this.Close();
            }
            else
            {
                // Mensaje de usuario/contraseña incorrectos:
                MessageBox.Show("Usuario y/o contraseña incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // Borrar textos:
                textoUsuario.Text = "";
                textoContraseña.Password = "";
            }
        }
    }
}
