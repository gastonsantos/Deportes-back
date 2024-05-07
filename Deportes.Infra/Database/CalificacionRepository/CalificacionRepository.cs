using Deportes.Modelo.CalificacionModel;
using Deportes.Servicio.Interfaces.ICalificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Infra.Database.CalificacionRepository
{
    public class CalificacionRepository : ICalificacionRepository
    {

        private readonly DeportesContext _context;

        public CalificacionRepository(DeportesContext context)
        {
            _context = context;
        }


        public void Calificar(Calificacion calificacion)
        {

            _context.Calificacions.Add(calificacion);   
            _context.SaveChanges();
        }
        public IList<Calificacion> CalificacionPorUsuario (int idUsuario)
        {
            return _context.Calificacions.Where(c => c.IdUsuarioCalificado == idUsuario).ToList();
        }

    }
}
