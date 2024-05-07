using Deportes.Modelo.CalificacionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.ICalificacion
{
    public interface ICalificacionRepository
    {
        public void Calificar(Calificacion calificacion);
        public IList<Calificacion> CalificacionPorUsuario(int idUsuario);
    }
}
