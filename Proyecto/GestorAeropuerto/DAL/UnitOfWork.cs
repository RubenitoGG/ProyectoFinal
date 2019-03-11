using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAeropuerto.DAL
{
    class UnitOfWork : IDisposable
    {
        private GestorAeropuertoContext context = new GestorAeropuertoContext();
        private bool disposed = false;

        private AerolineaRepositorio aerolineaRepositorio;
        private AvionRepositorio avionRepositorio;
        private BilleteRepositorio billeteRepositorio;
        private CargoRepositorio cargoRepositorio;
        private EmpleadoRepositorio empleadoRepositorio;
        private MaletaRepositorio maletaRepositorio;
        private PasajeroRepositorio pasajeroRepositorio;
        private VueloRepositorio vueloRepositorio;

        public AerolineaRepositorio AerolineaRepositorio
        {
            get
            {
                if (aerolineaRepositorio == null)
                    aerolineaRepositorio = new AerolineaRepositorio(context);

                return aerolineaRepositorio;
            }
        }

        public AvionRepositorio AvionRepositorio
        {
            get
            {
                if (avionRepositorio == null)
                    avionRepositorio = new AvionRepositorio(context);

                return avionRepositorio;
            }
        }

        public BilleteRepositorio BilleteRepositorio
        {
            get
            {
                if (billeteRepositorio == null)
                    billeteRepositorio = new BilleteRepositorio(context);

                return billeteRepositorio;
            }
        }

        public CargoRepositorio CargoRepositorio
        {
            get
            {
                if (cargoRepositorio == null)
                    cargoRepositorio = new CargoRepositorio(context);

                return cargoRepositorio;
            }
        }

        public EmpleadoRepositorio EmpleadoRepositorio
        {
            get
            {
                if (empleadoRepositorio == null)
                    empleadoRepositorio = new EmpleadoRepositorio(context);

                return empleadoRepositorio;
            }
        }

        public MaletaRepositorio MaletaRepositorio
        {
            get
            {
                if (maletaRepositorio == null)
                    maletaRepositorio = new MaletaRepositorio(context);

                return maletaRepositorio;
            }
        }

        public PasajeroRepositorio PasajeroRepositorio
        {
            get
            {
                if (pasajeroRepositorio == null)
                    pasajeroRepositorio = new PasajeroRepositorio(context);

                return pasajeroRepositorio;
            }
        }

        public VueloRepositorio VueloRepositorio
        {
            get
            {
                if (vueloRepositorio == null)
                    vueloRepositorio = new VueloRepositorio(context);

                return vueloRepositorio;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
