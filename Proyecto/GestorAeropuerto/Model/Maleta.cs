using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAeropuerto.Model
{
    public class Maleta : PropertyValidateModel
    {
        public int MaletaId { get; set; }

        public float Peso { get; set; }
    }
}
