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
    /// Lógica de interacción para FrameUsuarios.xaml
    /// </summary>
    public partial class FrameUsuarios : Page
    {
        VentanaAdministrador ventana; // Referencia a la ventana donde se encuentra el frame.
        List<Usuario> usuarios; // Lista de todos los usuarios.

        PropertyValidateModel validador = new PropertyValidateModel(); // Validador de la Base de Datos.

        UnitOfWork uow = new UnitOfWork();

        bool nuevo; // Variable para saber si se va a modificar o a crear un nuevor el usuario.

        public FrameUsuarios(VentanaAdministrador ventana)
        {
            InitializeComponent();
            this.ventana = ventana;
            nuevo = true;

            // Cambiamos el color del menú de la ventana:
            ventana.CambiarColor("Usuario");

            // Ponemos los usuarios en el ListBox:
            MostrarUsuarios();
        }
        /// <summary>
        /// Coge todos los usuarios de la base de datos y pone su nombre el el ListBox.
        /// </summary>
        private void MostrarUsuarios()
        {
            // Quitamos todos los usuarios de la lista:
            listaUsuarios.Items.Clear();

            // Cogemos la lista de todos los usuarios:
            this.usuarios = uow.UsuarioRepositorio.GetAll();

            // Ponemos todos los usuarios en el ListBox:
            foreach (Usuario usuario in usuarios)
            {
                listaUsuarios.Items.Add(usuario.Nombre);
            }
        }

        /// <summary>
        /// Borrar todos los TextBox y el deselecciona el item de la lista.
        /// </summary>
        private void BorrarCampos()
        {
            txt_id.Text = "";
            txt_usuario.Text = "";
            txt_contraseña.Text = "";

            listaUsuarios.SelectedItem = null;
        }

        /// <summary>
        /// Pone los datos del Usuario seleccionado en los TextBox y pasa al modo Actualizar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListaUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaUsuarios.SelectedItem == null)
                return;

            // Cogemos el usuario seleccionado:
            foreach (Usuario u in usuarios)
            {
                if (u.Nombre == listaUsuarios.SelectedItem.ToString())
                {
                    // Ponemos en los TextBox los valores del usuario:
                    txt_id.Text = u.UsuarioId.ToString();
                    txt_usuario.Text = u.Nombre;
                    txt_contraseña.Text = u.Password;
                }
            }

            // Cambiamos el botón añadir a modificar:
            BotonAñadir.Content = "Modificar";
            nuevo = false;
        }

        /// <summary>
        /// Deselecciona el usuario y cambia el botón a añadir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonNuevo_Click(object sender, RoutedEventArgs e)
        {
            BorrarCampos();
            BotonAñadir.Content = "Añadir";
            nuevo = true;
        }

        /// <summary>
        /// Añade o modifica un usuario a la base de datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonAñadir_Click(object sender, RoutedEventArgs e)
        {
            #region Añadir
            if (nuevo) // Modo crear.
            {
                // Creamos el usuario a añadir:
                Usuario usuario = new Usuario
                {
                    Nombre = txt_usuario.Text,
                    Password = txt_contraseña.Text
                };

                // Comprobamos con el validador:
                if (validador.errores(usuario) == "")
                {
                    // Añadirmo el usuario a la base de datos:
                    uow.UsuarioRepositorio.Añadir(usuario);

                    // Mensaje de usuario añadido:
                    MessageBox.Show("Usuario añadidio correctamente", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Campos incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion

            #region Actualizar
            else // Modo modificar.
            {
                // Buscamos el usuario seleccionado:
                foreach (Usuario usuario in usuarios)
                {
                    if (txt_id.Text == usuario.UsuarioId.ToString())
                    {
                        // Guardamos los nuevos valores:
                        usuario.Nombre = txt_usuario.Text;
                        usuario.Password = txt_contraseña.Text;

                        // Comprobamos con el validador:
                        if (validador.errores(usuario) == "")
                        {
                            // Actualizamos los usuarios:
                            uow.UsuarioRepositorio.Update(usuario);

                            // Mensaje de usuario modificado:
                            MessageBox.Show("Usuario modificado correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Campos incorrectos", "", MessageBoxButton.OK, MessageBoxImage.Error);
                            ventana.frameVentana.Content = new FrameUsuarios(ventana);
                        }
                    }
                }
            }
            #endregion

            // Borramos los campos:
            BorrarCampos();

            // Refrescamos la lista:
            MostrarUsuarios();
        }

        /// <summary>
        /// Elimina un usuario de la base de datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Buscamos el usuario seleccionado:
            foreach (Usuario usuario in usuarios)
            {
                if(usuario.UsuarioId.ToString() == txt_id.Text)
                {
                    // Borramos el usuario:
                    uow.UsuarioRepositorio.Delete(usuario);
                    
                    // Mensaje de usuario añadido:
                    MessageBox.Show("Usuario eliminado correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            
            // Borramos los campos:
            BorrarCampos();

            // Refrescamos la lista:
            MostrarUsuarios();
        }
    }
}
