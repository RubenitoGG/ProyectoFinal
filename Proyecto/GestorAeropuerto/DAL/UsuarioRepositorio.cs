using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorAeropuerto.Model;

namespace GestorAeropuerto.DAL
{
    public class UsuarioRepositorio : GenericRepository<Usuario>
    {
        public UsuarioRepositorio(GestorAeropuertoContext context) : base(context) { }
    }
}
