using Deportes.Modelo.ComunModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.UsuarioServices.Errores
{
    public  class UsuarioYaExisteException : ErrorDeDominioException
    {
        private const CategoriaError categoria = CategoriaError.EntidadSinEncontrar;
        private const string mensaje = "El email ya existe en la Base de Datos.";

        public UsuarioYaExisteException() : base(categoria, mensaje) { }
    }
}
