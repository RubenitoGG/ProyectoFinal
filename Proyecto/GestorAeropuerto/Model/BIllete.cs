using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAeropuerto.Model
{
    public class Billete
    {
        public int BilleteId { get; set; }

        [Required(ErrorMessage = "Precio obligatorio")]
        public float Precio { get; set; }

        [Required(ErrorMessage = "Tipo de asiento obligatorio")]
        [RegularExpression("Turista|Ejecutiva|Primera")]
        public string TipoAsiento { get; set; }

        [Required(ErrorMessage = "Número de asiento obligatorio")]
        public int NumAsiento { get; set; }

        [Key, ForeignKey("Pasajero")]
        public int PasajeroId { get; set; }

        public virtual Pasajero Pasajero { get; set; }

        public virtual Vuelo Vuelo { get; set; }
    }
}
