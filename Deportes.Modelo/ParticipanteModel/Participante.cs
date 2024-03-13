using Deportes.Modelo.EventoModel;
using Deportes.Modelo.UsuarioModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Modelo.ParticipanteModel;

public class Participante
{
    public int IdParticipantes { get; set; }

    public int IdEvento { get; set; }

    public int IdUsuarioParticipante { get; set; }

    public int IdUsuarioCreadorEvento { get; set; }

    public bool? Aceptado { get; set; }

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioCreadorEventoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioParticipanteNavigation { get; set; } = null!;
}
