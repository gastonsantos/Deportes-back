using Deportes.Modelo.ParticipanteModel;
using Deportes.Modelo.ParticipanteModel.Dto;
using Deportes.Modelo.UsuarioModel;
using Deportes.Servicio.Interfaces.IDeporte;
using Deportes.Servicio.Interfaces.IEvento;
using Deportes.Servicio.Interfaces.IParticipantes;
using Deportes.Servicio.Interfaces.IUsuario;
using Deportes.Servicio.Servicios.EventoServices.Errores;
using Deportes.Servicio.Servicios.ParticipantesServices.Errores;
using Deportes.Servicio.Servicios.UsuarioServices.Errores;
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
    private readonly IDeporteRepository _deporteRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    public ParticipantesServices(IParticipantesRepository participantesRepository, IEventoRepository eventoRepository, IDeporteRepository deporteRepository, IUsuarioRepository usuarioRepository)
    {
        _participantesRepository= participantesRepository;
        _eventoRepository= eventoRepository;
        _deporteRepository= deporteRepository;
        _usuarioRepository= usuarioRepository;
    }

    public void AceptarParticipante(int idParticipante)
    {
       

        if (!PuedeAceptarNotificacion(idParticipante))
        {
            EliminarParticipante(idParticipante);
            throw new EventoLlenoException();
        }
        _participantesRepository.AceptarParticipante(idParticipante);
    }

    

    public void EliminarParticipante(int idParticipante)
    {
       
        _participantesRepository.EliminarParticipante(idParticipante);
    }

    public void EnviarNotificacionParticipante(int idEvento, int idUsusarioQueInvita, int idUsuarioInvitado)
    {
        var evento = _eventoRepository.GetEvento(idEvento);
        if (evento == null)
        {
            throw new EventoNoEncontradoException();

        }

        bool hayNotif =  YaHayNotificacion(idEvento, idUsusarioQueInvita, idUsuarioInvitado);
        if (hayNotif)
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
        var usuario = _usuarioRepository.ObtenerUsuarioPorId(idUsuario);
        if (usuario == null)
        {
            throw new UsuarioNoEncontradoException();
        }
        return _participantesRepository.ObtenerNotificacionesPorUsuario(idUsuario);
    }

    public IList<Participante> ObtengoNotificacionParticipante(int idUsuario)
    {
        var usuario = _usuarioRepository.ObtenerUsuarioPorId(idUsuario);
        if (usuario == null)
        {
            throw new UsuarioNoEncontradoException();
        }
        return _participantesRepository.ObtengoNotificacionParticipante(idUsuario);
    }

    public bool ElQueInvitaEsDuenio(int idUsuarioInvita, int idEvento)
    {

        var usuario = _usuarioRepository.ObtenerUsuarioPorId(idUsuarioInvita);
        if (usuario == null)
        {
            throw new UsuarioNoEncontradoException();
        }
        var evento = _eventoRepository.GetEvento(idEvento);
        if (evento == null)
        {
            throw new EventoNoEncontradoException();

        }
        if (evento.IdUsuarioCreador == idUsuarioInvita)
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
        var usuario = _usuarioRepository.ObtenerUsuarioPorId(idUsusarioQueInvita);
        var usuario2 = _usuarioRepository.ObtenerUsuarioPorId(idUsuarioInvitado);
        if (usuario == null || usuario2 == null)
        {
            throw new UsuarioNoEncontradoException();
        }
        
        return  _participantesRepository.YaHayNotificacion(idEvento, idUsusarioQueInvita, idUsuarioInvitado);
    
    }

    public Participante ObtenerParticipantePorIdParticipante(int idParticipante)
    {
        return _participantesRepository.ObtenerParticipantePorIdParticipante(idParticipante);
    }

    public bool PuedeAceptarNotificacion(int idParticipante)
    {
        var participante = _participantesRepository.ObtenerParticipantePorIdParticipante(idParticipante);

        var evento = _eventoRepository.GetEvento(participante.IdEvento);

        var deporte = _deporteRepository.GetDeportePorId(evento.IdDeporte);
       if(_eventoRepository.CantidadDeParticipantesEnUnEvento(evento.IdEvento)+1 >= deporte.CantJugadores)
        {
            return false;
        }
        else
        {
            return true;
        }



    }
}
