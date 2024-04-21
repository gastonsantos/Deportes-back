using Deportes.Modelo.EventoModel.Dto;
using Deportes.Modelo.ParticipanteModel;
using Deportes.Modelo.ParticipanteModel.Dto;
using Deportes.Servicio.Interfaces.IParticipantes;
using Microsoft.EntityFrameworkCore;
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
            .Where(c => c.IdUsuarioParticipante == idUsuario)
            .ToList();

    }

    public void EnviarNotificacionParticipante(int idEvento,int idUserPart, int idUsuarioCreador)
    {

        var nuevoParticipante = new Participante
        {
            IdEvento = idEvento,
            IdUsuarioCreadorEvento = idUsuarioCreador,
            IdUsuarioParticipante = idUserPart,
            Aceptado = false,
            NotificacionVista= false
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
            .Where(p => p.IdUsuarioParticipante == idUsuario)
            .Select(p => new DtoNotificacion
            {
                IdParticipantes = p.IdParticipantes,
                IdEvento = p.IdEvento,
                Aceptado = p.Aceptado,
                NombreUsuarioInvito = p.IdUsuarioParticipanteNavigation.Nombre,
                ApellidoUsuarioInvito = p.IdUsuarioParticipanteNavigation.Apellido,
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

}
