using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.EventoModel;
using Deportes.Modelo.EventoModel.Dto;
using Deportes.Servicio.Interfaces.IEvento;
using Microsoft.EntityFrameworkCore;
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
                Imagen =e.IdDeporteNavigation.Imagen 

})
            .ToList();
    }
    public IList<DtoEventoDeporte> GetEventosCreadosPorUsuario(int idUsuario)
    {

        return _context.Evento.Where(c => c.IdUsuarioCreador == idUsuario && c.Finalizado != true).Select
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
                Imagen = e.IdDeporteNavigation.Imagen
            })
            .ToList();
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
                Imagen = e.IdDeporteNavigation.Imagen
            })
            .FirstOrDefault();
    }


    public void AgregarEvento(Evento evento)
    {
        _context.Evento.Add(evento);
        _context.SaveChanges();

    }

    public void CambiarEstadoEvento(int idEvento)
    {
        var evento = _context.Evento.FirstOrDefault(c => c.IdEvento == idEvento);
        evento.Finalizado = true;
        _context.SaveChanges();
    }










}
