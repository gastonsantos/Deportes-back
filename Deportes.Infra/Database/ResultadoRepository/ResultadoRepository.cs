using Deportes.Modelo.EventoModel;
using Deportes.Modelo.ResultadoModel;
using Deportes.Servicio.Interfaces.IResultado;
using Deportes.Servicio.Interfaces.IUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Infra.Database.ResultadoRepository;

public class ResultadoRepository : IResultadoRepository
{
    private readonly DeportesContext _context;

    public ResultadoRepository(DeportesContext context)
    {
        _context = context;
    }

    public void AgregarResultado(Resultado resultado)
    {
        _context.Add(resultado);
        _context.SaveChanges();
    }

    public void ModificarResultado(Resultado resultado)
    {
        var resultadoEncontrado = _context.Resultados.FirstOrDefault(e => e.IdEvento == resultado.IdEvento);
        if(resultadoEncontrado != null)
        {
            resultadoEncontrado.ResultadoLocal = resultado.ResultadoLocal;
            resultadoEncontrado.ResultadoVisitante = resultado.ResultadoVisitante;
            _context.SaveChanges();
        }

    }

    public Resultado ObtenerResultadoPorIdEvento(int idEvento)
    {
        return _context.Resultados.FirstOrDefault(e => e.IdEvento == idEvento);
    }


}
