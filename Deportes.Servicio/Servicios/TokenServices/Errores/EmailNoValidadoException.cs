using Deportes.Modelo.ComunModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.TokenServices.Errores
{
    public class EmailNoValidadoException : ErrorDeDominioException
    {
        private const CategoriaError categoria = CategoriaError.NoActivoMail;
        private const string mensaje = "El usuario no ha verificado el email!.";

        public EmailNoValidadoException() : base(categoria, mensaje) { }
    }
}
