using Deportes.Modelo.ComunModel;
using System.Net;

namespace Deportes.api.Config
{
    public static class MapeadorCodigoStatus
    {
        public static HttpStatusCode ObtenerCodigoPorCategoria(CategoriaError error)
        {
            return error switch
            {
                CategoriaError.EntidadSinEncontrar => HttpStatusCode.NotFound,
                CategoriaError.EntidadEncontrada => HttpStatusCode.BadRequest,
                CategoriaError.NoAutorizado => HttpStatusCode.Forbidden,
                CategoriaError.NoAutenticado => HttpStatusCode.Unauthorized,
                CategoriaError.ReglaNegocioNoCumplida => HttpStatusCode.BadRequest,
                CategoriaError.SinEspecificar => HttpStatusCode.InternalServerError,
                CategoriaError.NoActivoMail => HttpStatusCode.Conflict,
               
                _ => HttpStatusCode.InternalServerError,
            };
        }
    }
}
