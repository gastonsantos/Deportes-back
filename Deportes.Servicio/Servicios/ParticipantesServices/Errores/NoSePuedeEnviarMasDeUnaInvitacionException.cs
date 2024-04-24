using Deportes.Modelo.ComunModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.ParticipantesServices.Errores
{
    public class NoSePuedeEnviarMasDeUnaInvitacionException: ErrorDeDominioException
    {
        private const CategoriaError categoria = CategoriaError.NoSpamNotificacion;
        private const string mensaje = "No se puede enviar mas de una invitación a la vez.";

        public NoSePuedeEnviarMasDeUnaInvitacionException() : base(categoria, mensaje) { }
    }
}
