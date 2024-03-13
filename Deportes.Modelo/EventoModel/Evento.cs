using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.ParticipanteModel;
using Deportes.Modelo.UsuarioModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.EventoModel
{
    public  class Evento
    {
        public int IdEvento { get; set; }

        public string Nombre { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public int IdUsuarioCreador { get; set; }

        public int IdDeporte { get; set; }

        public DateTime? Fecha { get; set; }

        public bool? Finalizado { get; set; }

        public virtual Deporte IdDeporteNavigation { get; set; } = null!;

        public virtual Usuario IdUsuarioCreadorNavigation { get; set; } = null!;

        public virtual ICollection<Participante> Participantes { get; set; } = new List<Participante>();
    }
}
