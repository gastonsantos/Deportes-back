using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.UsuarioModel;
using Deportes.Servicio.Interfaces.IDeporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Infra.Database.DeporteRepository;

public class DeporteRepository : IDeporteRepository
{
    private readonly DeportesContext _context;

    public DeporteRepository(DeportesContext context)
    {
        _context = context;
    }

    public IList<Deporte> GetAllDeportes() //Todos los usuarios
    {
        return _context.Deporte.ToList();
    }



}
