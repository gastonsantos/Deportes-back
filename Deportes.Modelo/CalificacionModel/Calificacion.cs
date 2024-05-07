using Deportes.Modelo.EventoModel;
using Deportes.Modelo.UsuarioModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.CalificacionModel
{
    public class Calificacion
    {
        public int Id { get; set; }

        public int? IdUsuarioCalificador { get; set; }

        public int? IdUsuarioCalificado { get; set; }

        public int? IdEventoParticipo { get; set; }

        public int? Calificacion1 { get; set; }

        public virtual Evento? IdEventoParticipoNavigation { get; set; }

        public virtual Usuario? IdUsuarioCalificadoNavigation { get; set; }

        public virtual Usuario? IdUsuarioCalificadorNavigation { get; set; }
    }
}
