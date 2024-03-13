using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IChathub
{
    public interface IChatHub
    {
        public Task Send(string cuentoId, string user, string message);
        public Task AddToGroup(string cuentoId, string nombre);

        public Task LeaveGroup(string cuentoId, string nombre);

        public Task SendToGroup(string cuentoId, string user, string message);
    }
}
