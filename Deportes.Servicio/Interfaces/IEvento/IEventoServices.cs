using Deportes.Modelo.EventoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IEvento
{
    public interface IEventoServices
    {
        public void AgregarEvento(string nombre, string direccion, string provincia,string localidad, string numero, string hora, int idUsuarioCreador, int idDeporte, DateTime fecha);
    }
}
