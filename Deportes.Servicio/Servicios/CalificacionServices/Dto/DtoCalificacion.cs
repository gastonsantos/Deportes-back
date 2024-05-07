using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.CalificacionServices.Dto;

public class DtoCalificacion
{

    public int IdUsuarioCalificador { get; set; }
    public int IdUsuarioCalificado { get; set; }
    public int IdEvento { get; set; }
    public int Calificacion { get; set; }


}
