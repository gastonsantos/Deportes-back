using Deportes.Modelo.DeporteModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IDeporte
{
    public interface IDeporteService
    {
        public IList<Deporte> GetAllDeportes();
    }
}
