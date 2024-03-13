using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.EventoModel;
using Deportes.Servicio.Interfaces.IEvento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Infra.Database.EventoRepository
{
    public class EventoRepository : IEventoRepository
    {
        private readonly DeportesContext _context;

        public EventoRepository(DeportesContext context)
        {
            _context = context;
        }

        public IList<Evento> GetAllEventos()
        {

            return _context.Evento.ToList();

        }
        public IList<Evento> GetEventosCreadosPorUsuario(int idUsuario)
        {
            return _context.Evento.Where(c => c.IdUsuarioCreador == idUsuario && c.Finalizado != true).ToList();
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
}
