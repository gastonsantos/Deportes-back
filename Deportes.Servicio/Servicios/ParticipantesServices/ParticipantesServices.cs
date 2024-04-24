using Deportes.Modelo.ParticipanteModel;
using Deportes.Modelo.ParticipanteModel.Dto;
using Deportes.Servicio.Interfaces.IEvento;
using Deportes.Servicio.Interfaces.IParticipantes;
using Deportes.Servicio.Servicios.ParticipantesServices.Errores;
using MimeKit.Tnef;
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

    public void EnviarNotificacionParticipante(int idEvento, int idUsusarioQueInvita, int idUsuarioInvitado)
    {
        bool hayNotif =  YaHayNotificacion(idEvento, idUsusarioQueInvita, idUsuarioInvitado);
        if (hayNotif == true)
        {
            throw new NoSePuedeEnviarMasDeUnaInvitacionException();
        }
        if (idUsusarioQueInvita == idUsuarioInvitado)
        {
            throw new NoSePuedenEnviarInvitacionesAlMismoUsuario();
        }

        var esCreador = ElQueInvitaEsDuenio(idUsusarioQueInvita ,idEvento);
        _participantesRepository.EnviarNotificacionParticipante(idEvento, idUsusarioQueInvita, idUsuarioInvitado, esCreador);
    }

    public List<DtoNotificacion> ObtenerNotificacionesPorUsuario(int idUsuario)
    {

        return _participantesRepository.ObtenerNotificacionesPorUsuario(idUsuario);
    }

    public IList<Participante> ObtengoNotificacionParticipante(int idUsuario)
    {
        return _participantesRepository.ObtengoNotificacionParticipante(idUsuario);
    }

    public bool ElQueInvitaEsDuenio(int idUsuarioInvita, int idEvento)
    {

        var evento = _eventoRepository.GetEvento(idEvento);

        if(evento.IdUsuarioCreador == idUsuarioInvita)
        {
            return true;
        }
        else
        {
            return false;
        }

    }



    public bool YaHayNotificacion(int idEvento, int idUsusarioQueInvita, int idUsuarioInvitado)
    {

        return  _participantesRepository.YaHayNotificacion(idEvento, idUsusarioQueInvita, idUsuarioInvitado);
    
    }

}
