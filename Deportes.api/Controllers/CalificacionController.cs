using Deportes.api.Controllers.Dto;
using Deportes.Modelo.CalificacionModel;
using Deportes.Servicio.Interfaces.ICalificacion;
using Deportes.Servicio.Interfaces.IEvento;
using Deportes.Servicio.Servicios.CalificacionServices.Dto;
using Deportes.Servicio.Servicios.EventoServices;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Deportes.api.Controllers;



[ApiController]
[Route("calificacion")]
public class CalificacionController : Controller
{
    private readonly ICalificacionServices _calificacionServices;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public IConfiguration _configuration;
    public CalificacionController(ICalificacionServices calificacionServices, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _calificacionServices = calificacionServices;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;

    }

    [HttpPost("AgregarCalificacion", Name = "AgregarCalificacion")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite Calificacar ")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha calificado")]
    public ActionResult AgregarCalificacion([FromBody] DtoCalificacion calificacion)
    {

        _calificacionServices.Calificar(calificacion);
        return Ok();

    }

    [HttpPost("CalificacionDeUsuario", Name = "CalificacionDeUsuario")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite obtener la calificacion del usuario ")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha calificado")]
    public ActionResult ObtenerLaCalificacionDelUsuario([FromBody] DtoUsuarioPerfil usuario)
    {

        var calificacion = _calificacionServices.CalificacionPorUsuario(usuario.Id);
        return Ok(calificacion);

    }
}

