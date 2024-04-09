using Deportes.Servicio.Interfaces.IEvento;
using Deportes.Servicio.Interfaces.IFichas;
using Deportes.Servicio.Servicios.FichaServices.Dto;
using Deportes.Servicio.Servicios.FichaServices;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Deportes.api.Controllers.Dto;

namespace Deportes.api.Controllers;

[ApiController]
[Route("evento")]
public class EventoController : Controller
{
    private readonly IEventoServices _eventoServices;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public IConfiguration _configuration;
    public EventoController(IEventoServices eventoServices, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _eventoServices = eventoServices;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;

    }
    [HttpPost("AgregarEvento", Name = "AgragarEvento")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite un Evento")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha agregado un Evento")]
    public ActionResult AgregarEvento([FromBody] DtoEvento eventoDto)
    {

        _eventoServices.AgregarEvento(eventoDto.Nombre, eventoDto.Provincia, eventoDto.Localidad, eventoDto.Direccion, eventoDto.Numero, eventoDto.Hora, eventoDto.IdUsuarioCreador, eventoDto.IdDeporte, eventoDto.Fecha);
        return Ok();

    }
    [HttpGet("ObtenerEventos", Name = "ObtenerEventos")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Devuelve todos los eventos")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha devuelto todos los eventos")]
    public ActionResult ObtenerEventos()
    {
        var eventos = _eventoServices.GetAllEventosConDeportes().ToArray();
        return Ok(eventos);

    }
    [HttpPost("ObtenerEventosPorUsuario", Name = "ObtenerEventosPorUsuario")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite obtener los Eventos por Usuario")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se pudo traer los Eventos")]
    public ActionResult TraerEventosPorUsuario([FromBody] DtoEventoId eventoDto)
    {

        var eventos = _eventoServices.GetEventosCreadosPorUsuario(eventoDto.Id);
        return Ok(eventos);

    }
    [HttpGet("EventoDetalle/{id}", Name = "EventoDetalle")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite obtener el detalle de Evento")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se pudo traer el Evento")]
    public ActionResult TraerEventoDetalle(int id)
    {

        var eventos = _eventoServices.GetEventoConDeporte(id);
        return Ok(eventos);

    }


}
