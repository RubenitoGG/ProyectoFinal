using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAeropuerto.Model
{
    public class Aerolinea
    {
        public int AerolineaId { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Telefono obligatorio")]
        public string Telefono { get; set; }
    }
}
