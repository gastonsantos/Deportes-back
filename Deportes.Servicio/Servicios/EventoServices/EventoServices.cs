using Deportes.Modelo.EventoModel;
using Deportes.Modelo.EventoModel.Dto;
using Deportes.Servicio.Interfaces.IEvento;
using Deportes.Servicio.Interfaces.IFichas;
using Deportes.Servicio.Interfaces.IParticipantes;
using Deportes.Servicio.Interfaces.IUsuario;
using Deportes.Servicio.Servicios.EventoServices.Errores;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.EventoServices;

public class EventoServices: IEventoServices
{
    
    private readonly IEventoRepository _eventoRepository;
    private readonly IParticipantesRepository _participantesRepository;
    public EventoServices(IEventoRepository eventoRepository, IParticipantesRepository participantesRepository)
    {
        _eventoRepository = eventoRepository;
        _participantesRepository = participantesRepository;
   
    }

    public void AgregarEvento(string nombre, string provincia, string localidad, string direccion, string numero, string hora, int idUsuarioCreador, int idDeporte, DateTime fecha)
    {
       Evento evento= new Evento();
        evento.Nombre=nombre;
        evento.Direccion=direccion;
        evento.Provincia=provincia;
        evento.Localidad=localidad;
        evento.Numero=numero;
        evento.Hora=hora;
        evento.IdDeporte=idDeporte;
        evento.IdUsuarioCreador=idUsuarioCreador;
        evento.Fecha = new DateTime(fecha.Year, fecha.Month, fecha.Day);
        evento.Finalizado = false;
        _eventoRepository.AgregarEvento(evento);
    }

    public IList<DtoEventoDeporte> GetAllEventosConDeportes(int limit, int offset)
    {
       return _eventoRepository.GetAllEventosConDeportes()
            .Skip(offset)
            .Take(limit)
            .ToList();
    }

    public DtoEventoDeporte GetEventoConDeporte(int idEvento)
    {
        var evento = GetEvento(idEvento);
        if(evento == null)
        {
            throw new EventoNoEncontradoException();
        }
       // DtoEventoDeporte eventoDep = new DtoEventoDeporte();
         var eventoDep = _eventoRepository.GetEventoConDeporte(idEvento);

        eventoDep.CantJugadoresAnotados = _participantesRepository.CantidadDeUsuarioQueEstanAnotadosEnEvento(idEvento);

        return eventoDep;
    }

    public IList<DtoEventoDeporte> GetEventosCreadosPorUsuario(int idUsuario)
    {
        return _eventoRepository.GetEventosCreadosPorUsuario(idUsuario);
    }
    public Evento GetEvento(int idEvento)
    {
        return _eventoRepository.GetEvento(idEvento);
    }

    public void ModificarEvento(int idEvento, string nombre, string provincia, string localidad, string direccion, string numero, string hora, int idUsuarioCreador, int idDeporte, DateTime fecha)
    {
        var eventoEncontrado = _eventoRepository.GetEvento(idEvento);

        if(eventoEncontrado!= null)
        {
            Evento evento = new Evento();
            evento.IdEvento = idEvento;
            evento.Nombre = nombre;
            evento.Direccion = direccion;
            evento.Provincia = provincia;
            evento.Localidad = localidad;
            evento.Numero = numero;
            evento.Hora = hora;
            evento.IdDeporte = idDeporte;
            evento.IdUsuarioCreador = idUsuarioCreador;
            evento.Fecha = new DateTime(fecha.Year, fecha.Month, fecha.Day);
            evento.Finalizado = false;
            _eventoRepository.ModificarEvento(evento);

        }
        else
        {
            throw new EventoNoEncontradoException();
        }


    }

    public void CambiarEstadoEvento(int idEvento)
    {
        var evento = _eventoRepository.GetEvento(idEvento);

        if(evento != null)
        {
           
            _eventoRepository.CambiarEstadoEvento(idEvento);
        }
        else
        {
            throw new EventoNoEncontradoException();
        }
       
    }

    public IList<DtoEventoDeporte> GetEventosEnLosQueParticipo(int idUsuario)
    {
        return _eventoRepository.GetEventosEnLosQueParticipo(idUsuario);
    }

    public IList<DtoEventoDeporte> GetEventosCreadosPorUsuarioFinalizado(int idUsuario)
    {
        return _eventoRepository.GetEventosCreadosPorUsuarioFinalizado(idUsuario);
    }

    public IList<DtoEventoDeporte> GetEventosEnLosQueParticipoFinalizado(int idUsuario)
    {
        return _eventoRepository.GetEventosEnLosQueParticipoFinalizado(idUsuario);
    }

    public IList<DtoEventoDeporte> BuscadorDeEventosConDeporte(string? buscador)

    {
        return _eventoRepository.BuscadorDeEventosConDeporte(buscador);
    }

    public int CantidadDeParticipantesEnUnEvento(int idEvento)
    {
        return _eventoRepository.CantidadDeParticipantesEnUnEvento(idEvento);
    }
}
