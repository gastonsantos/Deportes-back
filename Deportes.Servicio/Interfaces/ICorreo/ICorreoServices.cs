using Deportes.Modelo.CorreoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.ICorreo
{
    public interface ICorreoServices
    {
        public bool Enviar(Correo correodto);
    }
}
