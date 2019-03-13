using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAeropuerto.Model
{
    public class Usuario : PropertyValidateModel
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(20, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(20, MinimumLength = 2)]
        public string Password { get; set; }
    }
}
