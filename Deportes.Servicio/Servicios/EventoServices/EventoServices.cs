using Deportes.Modelo.EventoModel;
using Deportes.Modelo.EventoModel.Dto;
using Deportes.Servicio.Interfaces.IEvento;
using Deportes.Servicio.Interfaces.IFichas;
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
    public EventoServices(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
   
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

    public IList<DtoEventoDeporte> GetAllEventosConDeportes()
    {
       return _eventoRepository.GetAllEventosConDeportes();
    }

    public DtoEventoDeporte GetEventoConDeporte(int idEvento)
    {
        var evento = GetEvento(idEvento);
        if(evento == null)
        {
            throw new EventoNoEncontradoException();
        }
        return _eventoRepository.GetEventoConDeporte(idEvento);
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
}
