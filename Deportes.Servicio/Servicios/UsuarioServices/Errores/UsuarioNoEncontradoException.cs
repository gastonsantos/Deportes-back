using Deportes.Modelo.ComunModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.UsuarioServices.Errores
{
    public class UsuarioNoEncontradoException : ErrorDeDominioException
    {
        private const CategoriaError categoria = CategoriaError.EntidadSinEncontrar;
        private const string mensaje = "El usuario no pudo ser encontrado.";

        public UsuarioNoEncontradoException() : base(categoria, mensaje) { }
    }
}
