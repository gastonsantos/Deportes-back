using Deportes.Modelo.ParticipanteModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IParticipantes
{
    public interface IParticipantesServices
    {
        public IList<Participante> ObtengoNotificacionParticipante(int idUsuario);
        public void EnviarNotificacionParticipante(int idEvento, int idUserPart);

        public void EliminarParticipante(int idEvento, int idUserPart);

        public void AceptarParticipante(int idEvento, int idUserPart);


    }
}
