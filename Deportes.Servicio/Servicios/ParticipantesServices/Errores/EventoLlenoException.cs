using Deportes.Modelo.ComunModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.ParticipantesServices.Errores
{
    public class EventoLlenoException : ErrorDeDominioException
    {
        private const CategoriaError categoria = CategoriaError.ReglaNegocioNoCumplida;
        private const string mensaje = "Evento lleno.";

        public EventoLlenoException() : base(categoria, mensaje) { }
    }
}
