using Deportes.Modelo.EventoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IEvento
{
    public interface IEventoRepository
    {
        public void AgregarEvento(Evento evento);
    }
}
