using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.EventoModel;
using Deportes.Modelo.EventoModel.Dto;
using Deportes.Servicio.Interfaces.IEvento;
using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Infra.Database.EventoRepository;

public class EventoRepository : IEventoRepository
{
    private readonly DeportesContext _context;

    public EventoRepository(DeportesContext context)
    {
        _context = context;
    }

    public Evento GetEvento(int idEvento)
    {
        return _context.Evento.FirstOrDefault(e => e.IdEvento == idEvento);
    }

    public IList<DtoEventoDeporte> GetAllEventosConDeportes()
    {
       

        return _context.Evento
            .Include(e => e.IdDeporteNavigation) 
            .Include(e => e.IdUsuarioCreadorNavigation)
            .Where(e => e.Finalizado == false)
            .Select(e => new DtoEventoDeporte
            {
                IdEvento = e.IdEvento,
                Nombre = e.Nombre,
                Provincia = e.Provincia,
                Localidad =e.Localidad,
                Direccion =e.Direccion,
                Numero = e.Numero,
                Hora = e.Hora,
                IdDeporte = e.IdDeporte, 
                Fecha= e.Fecha,
                NombreDep = e.IdDeporteNavigation.Nombre,
                CantJugadores =e.IdDeporteNavigation.CantJugadores,
                CantJugadoresAnotados = _context.Participante.Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true).Count()+1,
                Imagen =e.IdDeporteNavigation.Imagen ,
                IdUsuarioDuenio = e.IdUsuarioCreadorNavigation.Id,
                NombreDuenio = e.IdUsuarioCreadorNavigation.Nombre +" "+ e.IdUsuarioCreadorNavigation.Apellido
            })
            .ToList();
    }
    public IList<DtoEventoDeporte> GetEventosCreadosPorUsuario(int idUsuario)
    {

        return _context.Evento.Where(c => c.IdUsuarioCreador == idUsuario && c.Finalizado == false).Select
            (e => new DtoEventoDeporte
            {
                IdEvento = e.IdEvento,
                Nombre = e.Nombre,
                Provincia = e.Provincia,
                Localidad = e.Localidad,
                Direccion = e.Direccion,
                Numero = e.Numero,
                Hora = e.Hora,
                IdDeporte = e.IdDeporte,
                Fecha = e.Fecha,
                NombreDep = e.IdDeporteNavigation.Nombre,
                CantJugadores = e.IdDeporteNavigation.CantJugadores,
                CantJugadoresAnotados = _context.Participante.Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true).Count() + 1,

                Imagen = e.IdDeporteNavigation.Imagen,
                NombreDuenio = e.IdUsuarioCreadorNavigation.Nombre + " " + e.IdUsuarioCreadorNavigation.Apellido
            })
            .ToList();
    }
    public IList<DtoEventoDeporte> GetEventosEnLosQueParticipo(int idUsuario)
    {
        var eventosEnLosQueParticipo = _context.Participante
            .Where(p => p.IdUsuarioParticipante == idUsuario  && p.Aceptado == true && p.InvitaEsDuenio == true 
            || p.IdUsuarioCreadorEvento == idUsuario && p.Aceptado == true && p.InvitaEsDuenio == false )
            .Select(p => p.IdEvento);

      

        var eventos = _context.Evento
            .Where(e => eventosEnLosQueParticipo.Contains(e.IdEvento) && e.Finalizado == false)
            .Select(e => new DtoEventoDeporte
            {
                IdEvento = e.IdEvento,
                Nombre = e.Nombre,
                Provincia = e.Provincia,
                Localidad = e.Localidad,
                Direccion = e.Direccion,
                Numero = e.Numero,
                Hora = e.Hora,
                IdDeporte = e.IdDeporte,
                Fecha = e.Fecha,
                NombreDep = e.IdDeporteNavigation.Nombre,
                CantJugadores = e.IdDeporteNavigation.CantJugadores,
                CantJugadoresAnotados = _context.Participante.Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true).Count() + 1,
                Imagen = e.IdDeporteNavigation.Imagen,
                NombreDuenio = e.IdUsuarioCreadorNavigation.Nombre + " " + e.IdUsuarioCreadorNavigation.Apellido
            })
            .ToList();

        return eventos;
    }

    public DtoEventoDeporte GetEventoConDeporte(int idEvento)
    {

        return _context.Evento.Where(c => c.IdEvento == idEvento && c.Finalizado != true).Select
            (e => new DtoEventoDeporte
            {
                IdEvento = e.IdEvento,
                Nombre = e.Nombre,
                Provincia = e.Provincia,
                Localidad = e.Localidad,
                Direccion = e.Direccion,
                Numero = e.Numero,
                Hora = e.Hora,
                IdDeporte = e.IdDeporte,
                Fecha = e.Fecha,
                NombreDep = e.IdDeporteNavigation.Nombre,
                CantJugadores = e.IdDeporteNavigation.CantJugadores,
                CantJugadoresAnotados = _context.Participante.Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true).Count() + 1,
                Imagen = e.IdDeporteNavigation.Imagen,
                IdUsuarioDuenio =e.IdUsuarioCreadorNavigation.Id,
                NombreDuenio = e.IdUsuarioCreadorNavigation.Nombre + " " + e.IdUsuarioCreadorNavigation.Apellido
            })

            .FirstOrDefault();
    }


    public void AgregarEvento(Evento evento)
    {
        _context.Evento.Add(evento);
        _context.SaveChanges();

    }

    public void ModificarEvento(Evento evento)
    {
        var eventosEntontrado = _context.Evento.FirstOrDefault(c => c.IdEvento == evento.IdEvento);
        if (eventosEntontrado != null)
        {
            eventosEntontrado.Provincia = evento.Provincia;
            eventosEntontrado.Localidad = evento.Localidad;
            eventosEntontrado.Direccion = evento.Direccion;
            eventosEntontrado.Numero= evento.Numero;
            eventosEntontrado.Fecha = evento.Fecha;
            eventosEntontrado.Hora = evento.Hora;
            eventosEntontrado.IdDeporte = evento.IdDeporte;
            eventosEntontrado.Nombre= evento.Nombre;
        }
        _context.SaveChanges();
    }



    public void CambiarEstadoEvento(int idEvento)
    {
        var evento = _context.Evento.FirstOrDefault(c => c.IdEvento == idEvento);
        if (evento != null) {
            evento.Fecha = null;
            evento.Finalizado = true;
        }
       
        //evento.Finalizado = null;
        _context.SaveChanges();
    }


    public int IdUsuarioCreadorPorIdEvento(int idEvento)
    {
        var evento =  _context.Evento.FirstOrDefault(e => e.IdEvento == idEvento);
        return evento.IdUsuarioCreador;
    }






}
