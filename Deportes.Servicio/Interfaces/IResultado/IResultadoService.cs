using Deportes.Modelo.ResultadoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IResultado
{
    public interface IResultadoService
    {
        public void AgregarResultado(Resultado resultado);
        public Resultado ObtenerResultadoPorIdEvento(int idEvento);

        public void ModificarResultado(Resultado resultado);
    }
}
