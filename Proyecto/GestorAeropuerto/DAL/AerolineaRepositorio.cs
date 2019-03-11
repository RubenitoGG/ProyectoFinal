using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorAeropuerto.Model;

namespace GestorAeropuerto.DAL
{
    public class AerolineaRepositorio : GenericRepository<Aerolinea>
    {
        public AerolineaRepositorio(GestorAeropuertoContext context) : base(context) { }
    }
}
