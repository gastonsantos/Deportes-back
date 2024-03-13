using Deportes.Modelo.ParticipanteModel;
using Deportes.Servicio.Interfaces.IParticipantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.ParticipantesServices;

public class ParticipantesServices : IParticipantesServices
{
    private readonly IParticipantesRepository _participantesRepository;

    public ParticipantesServices(IParticipantesRepository participantesRepository)
    {
        _participantesRepository= participantesRepository;
    }

    public void AceptarParticipante(int idEvento, int idUserPart)
    {
        _participantesRepository.AceptarParticipante(idEvento, idUserPart);
    }

    public void EliminarParticipante(int idEvento, int idUserPart)
    {
        _participantesRepository.EliminarParticipante(idEvento, idUserPart);
    }

    public void EnviarNotificacionParticipante(int idEvento, int idUserPart)
    {
        _participantesRepository.EnviarNotificacionParticipante(idEvento, idUserPart);
    }

    public IList<Participante> ObtengoNotificacionParticipante(int idUsuario)
    {
        return _participantesRepository.ObtengoNotificacionParticipante(idUsuario);
    }
}
