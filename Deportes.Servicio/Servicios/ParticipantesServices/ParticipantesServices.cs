using Deportes.Modelo.ParticipanteModel;
using Deportes.Modelo.ParticipanteModel.Dto;
using Deportes.Servicio.Interfaces.IEvento;
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
    private readonly IEventoRepository _eventoRepository;
    public ParticipantesServices(IParticipantesRepository participantesRepository, IEventoRepository eventoRepository)
    {
        _participantesRepository= participantesRepository;
        _eventoRepository= eventoRepository;
    }

    public void AceptarParticipante(int idParticipante)
    {
        _participantesRepository.AceptarParticipante(idParticipante);
    }

    public void EliminarParticipante(int idParticipante)
    {
        _participantesRepository.EliminarParticipante(idParticipante);
    }

    public void EnviarNotificacionParticipante(int idEvento, int idUserPart)
    {
        int idUsuarioCreadorEvento = _eventoRepository.IdUsuarioCreadorPorIdEvento(idEvento);

        _participantesRepository.EnviarNotificacionParticipante(idEvento, idUserPart, idUsuarioCreadorEvento);
    }

    public List<DtoNotificacion> ObtenerNotificacionesPorUsuario(int idUsuario)
    {
        return _participantesRepository.ObtenerNotificacionesPorUsuario(idUsuario);
    }

    public IList<Participante> ObtengoNotificacionParticipante(int idUsuario)
    {
        return _participantesRepository.ObtengoNotificacionParticipante(idUsuario);
    }
}
