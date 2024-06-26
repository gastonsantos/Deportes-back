﻿using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.EventoModel;
using Deportes.Modelo.EventoModel.Dto;
using Deportes.Modelo.ResultadoModel.Dto;
using Deportes.Modelo.UsuarioModel;
using Deportes.Modelo.UsuarioModel.Dto;
using Deportes.Servicio.Interfaces.IEvento;
using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

    public int CantidadDeParticipantesEnUnEvento(int idEvento)
    {
        return _context.Participante.Where(p => p.IdEvento == idEvento && p.Aceptado == true).Count();
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
                DtoUsuarios = _context.Participante
                            .Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true)
                            .Select(u => new DtoUsuario
                            {
                                Id  = u.IdUsuarioParticipante,
                                Nombre = u.IdUsuarioParticipanteNavigation.Nombre,
                                Apellido = u.IdUsuarioParticipanteNavigation.Apellido,
                                Apodo = u.IdUsuarioParticipanteNavigation.Apodo,
                                Email = u.IdUsuarioParticipanteNavigation.Email
                            })
                            .ToList(),
                
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
                DtoUsuarios = _context.Participante
                            .Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true && p.IdUsuarioParticipante != e.IdUsuarioCreador )
                            .Select(u => new DtoUsuario
                            {
                                Id = u.IdUsuarioParticipante ,
                                Nombre = u.IdUsuarioParticipanteNavigation.Nombre ,
                                Apellido = u.IdUsuarioParticipanteNavigation.Apellido,
                                Apodo = u.IdUsuarioParticipanteNavigation.Apodo,
                                Email = u.IdUsuarioParticipanteNavigation.Email,
                                IdParticipante = _context.Participante.Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true).Select(par => par.IdParticipantes).FirstOrDefault(),
    
                            }).Union(_context.Participante
                    .Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true && e.IdUsuarioCreador != p.IdUsuarioCreadorEvento)
                    .Select(u => new DtoUsuario
                    {
                        Id = u.IdUsuarioCreadorEvento,
                        Nombre = u.IdUsuarioCreadorEventoNavigation.Nombre,
                        Apellido = u.IdUsuarioCreadorEventoNavigation.Apellido,
                        Apodo = u.IdUsuarioCreadorEventoNavigation.Apodo,
                        Email = u.IdUsuarioCreadorEventoNavigation.Email,
                        IdParticipante = _context.Participante.Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true).Select(par => par.IdParticipantes).FirstOrDefault(),

                    }))
                            .ToList(),
                IdParticipante = _context.Participante.Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true).Select(par => par.IdParticipantes).FirstOrDefault(),
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
                DtoUsuarios = _context.Participante
                            .Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true
                            )
                            .Select(u => new DtoUsuario
                            {
                                Id = u.IdUsuarioParticipante,
                                Nombre = u.IdUsuarioParticipanteNavigation.Nombre,
                                Apellido = u.IdUsuarioParticipanteNavigation.Apellido,
                                Apodo = u.IdUsuarioParticipanteNavigation.Apodo,
                                Email = u.IdUsuarioParticipanteNavigation.Email

                            }).Union(_context.Participante
                    .Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true)
                    .Select(u => new DtoUsuario
                    {
                        Id = u.IdUsuarioCreadorEvento,
                        Nombre = u.IdUsuarioCreadorEventoNavigation.Nombre,
                        Apellido = u.IdUsuarioCreadorEventoNavigation.Apellido,
                        Apodo = u.IdUsuarioCreadorEventoNavigation.Apodo,
                        Email = u.IdUsuarioCreadorEventoNavigation.Email
                    }))
                            .ToList(),
                IdParticipante = _context.Participante.Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true).Select(par => par.IdParticipantes).FirstOrDefault(),
                Imagen = e.IdDeporteNavigation.Imagen,
                NombreDuenio = e.IdUsuarioCreadorNavigation.Nombre + " " + e.IdUsuarioCreadorNavigation.Apellido
            })
            .ToList();

        return eventos;
    }

    public IList<DtoEventoDeporte> GetEventosCreadosPorUsuarioFinalizado(int idUsuario)
    {

        return _context.Evento.Where(c => c.IdUsuarioCreador == idUsuario && c.Finalizado == true)
            .OrderByDescending(e => e.Fecha)
            .Select
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
                DtoUsuarios = _context.Participante
                            .Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true && p.IdUsuarioParticipante != e.IdUsuarioCreador)
                            .Select(u => new DtoUsuario
                            {
                                Id = u.IdUsuarioParticipante,
                                Nombre = u.IdUsuarioParticipanteNavigation.Nombre,
                                Apellido = u.IdUsuarioParticipanteNavigation.Apellido,
                                Apodo = u.IdUsuarioParticipanteNavigation.Apodo,
                                Email = u.IdUsuarioParticipanteNavigation.Email
                            }).Union(_context.Participante
                    .Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true && e.IdUsuarioCreador != p.IdUsuarioCreadorEvento)
                    .Select(u => new DtoUsuario
                    {
                        Id = u.IdUsuarioCreadorEvento,
                        Nombre = u.IdUsuarioCreadorEventoNavigation.Nombre,
                        Apellido = u.IdUsuarioCreadorEventoNavigation.Apellido,
                        Apodo = u.IdUsuarioCreadorEventoNavigation.Apodo,
                        Email = u.IdUsuarioCreadorEventoNavigation.Email
                    }))
                            .ToList(),
                DtoResultado = _context.Resultados
                .Where(r => r.IdEvento == e.IdEvento)
                .Select(p => new DtoResultado
                {
                    ResultadoLocal=p.ResultadoLocal,
                    ResultadoVisitante=p.ResultadoVisitante
                }).FirstOrDefault(),
                Imagen = e.IdDeporteNavigation.Imagen,
                NombreDuenio = e.IdUsuarioCreadorNavigation.Nombre + " " + e.IdUsuarioCreadorNavigation.Apellido
            })
            .ToList();
    }

    public DtoEventoDeporte GetEventoConDeporte(int idEvento)
    {

        return _context.Evento.Where(c => c.IdEvento == idEvento && c.Finalizado == false).Select
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
                DtoUsuarios = _context.Participante
                            .Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true
                            )
                            .Select(u => new DtoUsuario
                            {
                                Id = u.IdUsuarioParticipante,
                                Nombre = u.IdUsuarioParticipanteNavigation.Nombre,
                                Apellido = u.IdUsuarioParticipanteNavigation.Apellido,
                                Apodo = u.IdUsuarioParticipanteNavigation.Apodo,
                                Email = u.IdUsuarioParticipanteNavigation.Email
                            }).Union(_context.Participante
                    .Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true)
                    .Select(u => new DtoUsuario
                    {
                        Id = u.IdUsuarioCreadorEvento,
                        Nombre = u.IdUsuarioCreadorEventoNavigation.Nombre,
                        Apellido = u.IdUsuarioCreadorEventoNavigation.Apellido,
                        Apodo = u.IdUsuarioCreadorEventoNavigation.Apodo,
                        Email = u.IdUsuarioCreadorEventoNavigation.Email,

                    }))
                            .ToList(),
                Imagen = e.IdDeporteNavigation.Imagen,
                IdUsuarioDuenio =e.IdUsuarioCreadorNavigation.Id,
                NombreDuenio = e.IdUsuarioCreadorNavigation.Nombre + " " + e.IdUsuarioCreadorNavigation.Apellido
            })

            .FirstOrDefault();
    }

    public  IList<DtoEventoDeporte> BuscadorDeEventosConDeporte(string? buscador)
    {

            var eventos =  _context.Evento.Where(c => c.Provincia == buscador && c.Finalizado == false || c.Localidad == buscador  && c.Finalizado== false || c.Nombre == buscador && c.Finalizado == false || c.IdDeporteNavigation.Nombre == buscador && c.Finalizado == false).Select
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
                DtoUsuarios = _context.Participante
                            .Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true && p.IdUsuarioParticipante != e.IdUsuarioCreador)
                            .Select(u => new DtoUsuario
                            {
                                Id = u.IdUsuarioParticipante,
                                Nombre = u.IdUsuarioParticipanteNavigation.Nombre,
                                Apellido = u.IdUsuarioParticipanteNavigation.Apellido,
                                Apodo = u.IdUsuarioParticipanteNavigation.Apodo,
                                Email = u.IdUsuarioParticipanteNavigation.Email,
                                IdParticipante = _context.Participante.Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true).Select(par => par.IdParticipantes).FirstOrDefault(),

                            }).Union(_context.Participante
                    .Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true && e.IdUsuarioCreador != p.IdUsuarioCreadorEvento)
                    .Select(u => new DtoUsuario
                    {
                        Id = u.IdUsuarioCreadorEvento,
                        Nombre = u.IdUsuarioCreadorEventoNavigation.Nombre,
                        Apellido = u.IdUsuarioCreadorEventoNavigation.Apellido,
                        Apodo = u.IdUsuarioCreadorEventoNavigation.Apodo,
                        Email = u.IdUsuarioCreadorEventoNavigation.Email,
                        IdParticipante = _context.Participante.Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true).Select(par => par.IdParticipantes).FirstOrDefault(),

                    }))
                            .ToList(),
                IdParticipante = _context.Participante.Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true).Select(par => par.IdParticipantes).FirstOrDefault(),
                Imagen = e.IdDeporteNavigation.Imagen,
                NombreDuenio = e.IdUsuarioCreadorNavigation.Nombre + " " + e.IdUsuarioCreadorNavigation.Apellido
            })
            .ToList();
        return eventos;
    }



    public int AgregarEvento(Evento evento)
    {
        _context.Evento.Add(evento);
        _context.SaveChanges();
        return evento.IdEvento;

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

    public IList<DtoEventoDeporte> GetEventosEnLosQueParticipoFinalizado(int idUsuario)
    {
        var eventosEnLosQueParticipo = _context.Participante
            .Where(p => p.IdUsuarioParticipante == idUsuario && p.Aceptado == true && p.InvitaEsDuenio == true
            || p.IdUsuarioCreadorEvento == idUsuario && p.Aceptado == true && p.InvitaEsDuenio == false)
            .Select(p => p.IdEvento);

        var eventos = _context.Evento
            .Where(e => eventosEnLosQueParticipo.Contains(e.IdEvento) && e.Finalizado == true)
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
                DtoUsuarios = _context.Participante
                            .Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true
                            )
                            .Select(u => new DtoUsuario
                            {
                                Id = u.IdUsuarioParticipante,
                                Nombre = u.IdUsuarioParticipanteNavigation.Nombre,
                                Apellido = u.IdUsuarioParticipanteNavigation.Apellido,
                                Apodo = u.IdUsuarioParticipanteNavigation.Apodo,
                                Email = u.IdUsuarioParticipanteNavigation.Email

                            }).Union(_context.Participante
                    .Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true)
                    .Select(u => new DtoUsuario
                    {
                        Id = u.IdUsuarioCreadorEvento,
                        Nombre = u.IdUsuarioCreadorEventoNavigation.Nombre,
                        Apellido = u.IdUsuarioCreadorEventoNavigation.Apellido,
                        Apodo = u.IdUsuarioCreadorEventoNavigation.Apodo,
                        Email = u.IdUsuarioCreadorEventoNavigation.Email
                    }))
                            .ToList(),
                DtoResultado = _context.Resultados
                .Where(r => r.IdEvento == e.IdEvento)
                .Select(p => new DtoResultado
                {
                    ResultadoLocal = p.ResultadoLocal,
                    ResultadoVisitante = p.ResultadoVisitante
                }).FirstOrDefault(),
                IdParticipante = _context.Participante.Where(p => p.IdEvento == e.IdEvento && p.Aceptado == true).Select(par => par.IdParticipantes).FirstOrDefault(),
                Imagen = e.IdDeporteNavigation.Imagen,
                NombreDuenio = e.IdUsuarioCreadorNavigation.Nombre + " " + e.IdUsuarioCreadorNavigation.Apellido
            })
            .ToList();

        return eventos;
    }


    
}
