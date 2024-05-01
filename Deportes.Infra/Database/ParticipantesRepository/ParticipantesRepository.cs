using Deportes.Modelo.EventoModel.Dto;
using Deportes.Modelo.ParticipanteModel;
using Deportes.Modelo.ParticipanteModel.Dto;
using Deportes.Servicio.Interfaces.IParticipantes;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Infra.Database.ParticipantesRepository;

public class ParticipantesRepository: IParticipantesRepository
{
    private readonly DeportesContext _context;

    public ParticipantesRepository(DeportesContext context)
    {
        _context = context;
    }
    public IList<Participante> ObtengoNotificacionParticipante(int idUsuario)
    {

        return _context.Participante
            .Where(c => c.IdUsuarioParticipante == idUsuario && c.NotificacionVista == false)
            .ToList();

    }


    public void EnviarNotificacionParticipante(int idEvento,int idUsusarioQueInvita, int idUsuarioInvitado, bool esCreador)
    {

        var nuevoParticipante = new Participante
        {
            IdEvento = idEvento,
            IdUsuarioCreadorEvento = idUsusarioQueInvita,
            IdUsuarioParticipante = idUsuarioInvitado,
            Aceptado = false,
            NotificacionVista= false,
            InvitaEsDuenio = esCreador
        };
        _context.Participante.Add(nuevoParticipante);
        _context.SaveChanges();

    }
    public void EliminarParticipante(int idParticipante)
    {
        var participacionExistente = _context.Participante.FirstOrDefault(c =>
            c.IdParticipantes == idParticipante);

        if (participacionExistente != null)
        {

            _context.Participante.Remove(participacionExistente);


            _context.SaveChanges();
        }


    }



    public void AceptarParticipante(int idParticipante)
    {
        var participanteExistente = _context.Participante.FirstOrDefault(c =>
            c.IdParticipantes == idParticipante);

        if (participanteExistente != null) { 


            participanteExistente.Aceptado = true;


            _context.SaveChanges();
        }

    }

    public List<DtoNotificacion> ObtenerNotificacionesPorUsuario(int idUsuario)
    {
        var notificaciones = _context.Participante
            .Where(p => p.IdUsuarioParticipante == idUsuario && p.Aceptado == false)
            .Select(p => new DtoNotificacion
            {
                IdParticipantes = p.IdParticipantes,
                IdEvento = p.IdEvento,
                IdElQueInvita = p.IdUsuarioCreadorEvento,
                Aceptado = p.Aceptado,
                InvitaEsDuenio = p.InvitaEsDuenio,
                NombreUsuarioInvito = p.IdUsuarioCreadorEventoNavigation.Nombre,
                ApellidoUsuarioInvito = p.IdUsuarioCreadorEventoNavigation.Apellido,
                NombreDeporte = p.IdEventoNavigation.IdDeporteNavigation.Nombre,
                Fecha = p.IdEventoNavigation.Fecha,
                Hora = p.IdEventoNavigation.Hora,
                Provincia = p.IdEventoNavigation.Provincia,
                Localidad = p.IdEventoNavigation.Localidad,
                Direccion = p.IdEventoNavigation.Direccion,
                Numero = p.IdEventoNavigation.Numero,
            })
            .ToList();

        return notificaciones;
    }

    public bool YaHayNotificacion(int idEvento, int idUsusarioQueInvita, int idUsuarioInvitado)
    {
        var participante = _context.Participante.FirstOrDefault(p =>
           p.IdEvento == idEvento &&
           p.IdUsuarioParticipante == idUsuarioInvitado &&
           p.IdUsuarioCreadorEvento == idUsusarioQueInvita ||

           p.IdEvento == idEvento &&
           p.IdUsuarioParticipante == idUsusarioQueInvita && 
           p.IdUsuarioCreadorEvento == idUsuarioInvitado);

        return participante != null;
    }
    public int CantidadDeUsuarioQueEstanAnotadosEnEvento(int idEvento)
    {
        var cantidad = _context.Participante.Where(e => e.IdEvento == idEvento && e.Aceptado==true).Count();
        return cantidad + 1;
    }
}
