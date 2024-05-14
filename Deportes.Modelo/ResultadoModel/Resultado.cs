using Deportes.Modelo.EventoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.ResultadoModel
{
    public class Resultado
    {
        public int Id { get; set; }

        public int? IdEvento { get; set; }

        public int? ResultadoLocal { get; set; }

        public int? ResultadoVisitante { get; set; }

        public virtual Evento? IdEventoNavigation { get; set; }
    }
}
