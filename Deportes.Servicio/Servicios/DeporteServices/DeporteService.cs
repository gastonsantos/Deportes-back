using Deportes.Modelo.DeporteModel;
using Deportes.Servicio.Interfaces.IDeporte;
using Deportes.Servicio.Interfaces.IUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.DeporteServices
{
    public class DeporteService : IDeporteService
    {
        private readonly IDeporteRepository _deporteRepository;

        public DeporteService(IDeporteRepository deporteRepository)
        {
            _deporteRepository = deporteRepository;
        }

        public IList<Deporte> GetAllDeportes()
        {
            return _deporteRepository.GetAllDeportes();
        }

        public Deporte GetDeportePorId(int idDeporte)
        {
            return _deporteRepository.GetDeportePorId(idDeporte);
        }
    }
}
