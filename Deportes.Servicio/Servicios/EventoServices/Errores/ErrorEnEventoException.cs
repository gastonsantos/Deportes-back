using Deportes.Modelo.ComunModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.EventoServices.Errores
{
    public class ErrorEnEventoException : ErrorDeDominioException
    {
        private const CategoriaError categoria = CategoriaError.ReglaNegocioNoCumplida;
        private const string mensaje = "Error en creacion o modificacion de evento";

        public ErrorEnEventoException() : base(categoria, mensaje) { }
    }
}
