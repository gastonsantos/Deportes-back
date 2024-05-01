using Deportes.Modelo.ParticipanteModel;
using Deportes.Modelo.ParticipanteModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IParticipantes
{
    public interface IParticipantesRepository
    {
        public IList<Participante> ObtengoNotificacionParticipante(int idUsuario);
        public void EnviarNotificacionParticipante(int idEvento, int idUsuarioQueInvita, int idUsuarioInvitado, bool esCreador);

        public void EliminarParticipante(int idParticipante);

        public void AceptarParticipante(int idParticipante);
        public List<DtoNotificacion> ObtenerNotificacionesPorUsuario(int idUsuario);
        public bool YaHayNotificacion(int idEvento, int idUsusarioQueInvita, int idUsuarioInvitado);
        public int CantidadDeUsuarioQueEstanAnotadosEnEvento(int idEvento);

    }
}
