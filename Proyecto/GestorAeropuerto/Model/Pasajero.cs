using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAeropuerto.Model
{
    [Table(name: "Pasajeros")]
    public class Pasajero : PropertyValidateModel
    {
        public int PasajeroId { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(20, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Número de pasaporte obligatorio")]
        public int NumPasaporte { get; set; }

        public ICollection<Maleta> Maletas { get; set; }

        public virtual Billete Billete { get; set; }
    }
}
