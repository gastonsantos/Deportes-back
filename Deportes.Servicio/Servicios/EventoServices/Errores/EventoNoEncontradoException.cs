using Deportes.Modelo.ComunModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.EventoServices.Errores
{
    public class EventoNoEncontradoException : ErrorDeDominioException
    {
        private const CategoriaError categoria = CategoriaError.EntidadSinEncontrar;
        private const string mensaje = "Evento No encontrado.";

        public EventoNoEncontradoException() : base(categoria, mensaje) { }
    }
}
