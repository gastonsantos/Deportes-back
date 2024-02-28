using Deportes.Modelo.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IToken;

public interface IAutorizacionService
{
    Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion);
}
