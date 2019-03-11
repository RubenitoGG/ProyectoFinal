using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorAeropuerto.Model;

namespace GestorAeropuerto.DAL
{
    public class PasajeroRepositorio : GenericRepository<Pasajero>
    {
        public PasajeroRepositorio(GestorAeropuertoContext context) : base(context) { }
    }
}
