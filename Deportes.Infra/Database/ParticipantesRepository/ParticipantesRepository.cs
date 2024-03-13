using Deportes.Modelo.ParticipanteModel;
using Deportes.Servicio.Interfaces.IParticipantes;
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

    public void EnviarNotificacionParticipante(int idEvento,int idUserPart)
    {

        var nuevoParticipante = new Participante
        {
            IdEvento = idEvento,
            IdUsuarioParticipante = idUserPart,
            Aceptado = false
        };


        _context.Participante.Add(nuevoParticipante);
        _context.SaveChanges();

    }
    public void EliminarParticipante(int idEvento, int idUserPart)
    {


        var participacionExistente = _context.Participante.FirstOrDefault(c =>
            c.IdEvento == idEvento &&
            c.IdUsuarioParticipante == idUserPart);

        if (participacionExistente != null)
        {

            _context.Participante.Remove(participacionExistente);


            _context.SaveChanges();
        }


    }



    public void AceptarParticipante(int idEvento, int idUserPart)
    {


        var participanteExistente = _context.Participante.FirstOrDefault(c =>
            c.IdEvento == idEvento &&
            c.IdUsuarioParticipante == idUserPart);

        if (participanteExistente != null) { 


            participanteExistente.Aceptado = true;


            _context.SaveChanges();
        }

    }


}
