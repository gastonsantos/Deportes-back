using Deportes.Modelo.EventoModel;
using Deportes.Modelo.EventoModel.Dto;
using Deportes.Modelo.ResultadoModel;
using Deportes.Modelo.UsuarioModel;
using Deportes.Servicio.Interfaces.IDeporte;
using Deportes.Servicio.Interfaces.IEvento;
using Deportes.Servicio.Interfaces.IFichas;
using Deportes.Servicio.Interfaces.IParticipantes;
using Deportes.Servicio.Interfaces.IResultado;
using Deportes.Servicio.Interfaces.IUsuario;
using Deportes.Servicio.Servicios.EventoServices.Errores;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.EventoServices;

public class EventoServices : IEventoServices
{

    private readonly IEventoRepository _eventoRepository;
    private readonly IParticipantesRepository _participantesRepository;
    private readonly IUsuarioRepository _usuariosRepository;
    private readonly IDeporteRepository _deporteRepository; 
    private readonly IResultadoRepository _resultadoRepository;
    public EventoServices(IEventoRepository eventoRepository, IParticipantesRepository participantesRepository, IUsuarioRepository usuariosRepository, IDeporteRepository deporteRepository, IResultadoRepository resultadoRepository)
    {
        _eventoRepository = eventoRepository;
        _participantesRepository = participantesRepository;
        _usuariosRepository = usuariosRepository;   
        _deporteRepository = deporteRepository;
        _resultadoRepository = resultadoRepository;
    }

    public void AgregarEvento(string nombre, string provincia, string localidad, string direccion, string numero, string hora, int idUsuarioCreador, int idDeporte, DateTime fecha)
    {
        if (!IsNullOVacio(nombre) || !IsNullOVacio(provincia) || !IsNullOVacio(localidad) || !IsNullOVacio(direccion) || !IsNullOVacio(numero) || !IsNullOVacio(hora) || idUsuarioCreador == 0 || idUsuarioCreador == 0)
        {
            throw new ErrorEnEventoException();
        }
        if (!ExisteUsuarioEnBd(idUsuarioCreador))
        {
            throw new ErrorEnEventoException();
        }
        if (!ExisteDeporetEnBd(idDeporte))
        {
            throw new ErrorEnEventoException();
        }
        if (!EsFechaValida(fecha))
        {
            throw new ErrorEnEventoException();
        }


        Evento evento = new Evento();
       
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
         var idEvento = _eventoRepository.AgregarEvento(evento);

         AgregarResultadoPredeterminado(idEvento);

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
            if (!IsNullOVacio(nombre) || !IsNullOVacio(provincia) || !IsNullOVacio(localidad) || !IsNullOVacio(direccion) || !IsNullOVacio(numero) || !IsNullOVacio(hora) || idUsuarioCreador == 0 || idUsuarioCreador == 0)
            {
                throw new ErrorEnEventoException();
            }
            if (!ExisteUsuarioEnBd(idUsuarioCreador))
            {
                throw new ErrorEnEventoException();
            }
            if (!ExisteDeporetEnBd(idDeporte))
            {
                throw new ErrorEnEventoException();
            }
            if (!EsFechaValida(fecha))
            {
                throw new ErrorEnEventoException();
            }

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

    public IList<DtoEventoDeporte> GetEventosCreadosPorUsuarioFinalizado(int idUsuario, int limit, int offset)
    {
        if (!ExisteUsuarioEnBd(idUsuario))
        {
            throw new ErrorEnEventoException();
        }
        return _eventoRepository.GetEventosCreadosPorUsuarioFinalizado(idUsuario)
            .Skip(offset)
            .Take(limit)
            .ToList(); 
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
    public void AgregarResultado(int? idEvento, int? resultadoLocal, int? resultadoVisitante)
    {
        
        var evento = GetEvento((int)idEvento);
        if (evento == null)
        {
            throw new EventoNoEncontradoException();
        }
        Resultado resultado = new Resultado();
        resultado.IdEvento = idEvento;
        resultado.ResultadoLocal = resultadoLocal;
        resultado.ResultadoVisitante = resultadoVisitante;
        _resultadoRepository.AgregarResultado(resultado);

    }

    private bool ExisteUsuarioEnBd(int idUsuario)
    {
        var usuario = _usuariosRepository.ObtenerUsuarioPorId(idUsuario);
        if (usuario == null)
        {
            return false;
        }
        return true;
    }

    private bool ExisteDeporetEnBd(int idDeporte)
    {
        var deporte = _deporteRepository.GetDeportePorId(idDeporte);
        if (deporte == null)
        {
            return false;
        }
        return true;
    }
    private bool IsNullOVacio(string palabra)
    {
        if (String.IsNullOrEmpty(palabra))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool EsFechaValida(DateTime fecha)
    {
        DateTime fechaActual = DateTime.Today;


        return fecha.Date >= fechaActual.Date;
    }

    private void AgregarResultadoPredeterminado(int idEvento)
    {
        Resultado resultado = new Resultado();
        resultado.IdEvento = idEvento;
        resultado.ResultadoLocal = 0;
        resultado.ResultadoVisitante = 0;
        _resultadoRepository.AgregarResultado(resultado);
    }
    
}
