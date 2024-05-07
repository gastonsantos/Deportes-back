using Deportes.Modelo.CalificacionModel;
using Deportes.Servicio.Servicios.CalificacionServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.ICalificacion
{
    public interface ICalificacionServices
    {
        public void Calificar(DtoCalificacion calificacionDto);
        public int? CalificacionPorUsuario(int idUsuario);
    }
   
}
