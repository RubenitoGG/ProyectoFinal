using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAeropuerto.Model
{
    [Table(name: "Vuelos")]
    public class Vuelo
    {
        public int VueloId { get; set; }

        [Required(ErrorMessage = "Origen obligatorio")]
        public string Origen { get; set; }

        [Required(ErrorMessage = "Destino obligatorio")]
        public string Destino { get; set; }

        [Required(ErrorMessage = "Llegada obligatoria")]
        public string Llegada { get; set; }

        [Required(ErrorMessage = "Salida obligatoria")]
        public string Salida { get; set; }

        public virtual Aerolinea Aerolinea { get; set; }

        public ICollection<Billete> Billetes { get; set; }

        public ICollection<Avion> Aviones { get; set; }
    }
}
