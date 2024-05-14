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
    public ActionResult ObtenerEventos([FromQuery] DtoGetTodosLosEventos dto)
    {
        var eventos = _eventoServices.GetAllEventosConDeportes(dto.Limit, dto.Offset)
            .ToArray();
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

    [HttpPost("ModificarEvento", Name = "ModificarEvento")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite un modificar Evento")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha modificado un Evento")]
    public ActionResult ModificarEvento([FromBody] DtoEventoModificar eventoDto)
    {

        _eventoServices.ModificarEvento(eventoDto.IdEvento, eventoDto.Nombre, eventoDto.Provincia, eventoDto.Localidad, eventoDto.Direccion, eventoDto.Numero, eventoDto.Hora, eventoDto.IdUsuarioCreador, eventoDto.IdDeporte, eventoDto.Fecha);
        return Ok();

    }

    [HttpPost("CancelarEvento", Name = "CancelarEvento")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite cancelar un evento")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se pudo cancelar el evento")]
    public ActionResult CancelarEvento([FromBody] DtoEventoId eventoDto)
    {

         _eventoServices.CambiarEstadoEvento(eventoDto.Id);
        return Ok();

    }


    [HttpPost("ObtenerEventosQueParticipo", Name = "ObtenerEventosQueParticipo")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite obtener los Eventos por Usuario que participo")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se pudo traer los Eventos en los cuales participo")]
    public ActionResult ObtenerEventosQueParticipo([FromBody] DtoEventoId eventoDto)
    {

        var eventos = _eventoServices.GetEventosEnLosQueParticipo(eventoDto.Id);
        return Ok(eventos);

    }
    [HttpPost("ObtenerEventosQueParticipoFinalizado", Name = "ObtenerEventosQueParticipoFinalizado")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite obtener los Eventos por Usuario que participe")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se pudo traer los Eventos en los cuales participe")]
    public ActionResult ObtenerEventosQueParticipoFinalizado([FromBody] DtoEventoId eventoDto)
    {

        var eventos = _eventoServices.GetEventosEnLosQueParticipoFinalizado(eventoDto.Id);
        return Ok(eventos);

    }

    [HttpPost("ObtenerEventosPorUsuarioFinalizado", Name = "ObtenerEventosPorUsuarioFinalizado")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite obtener los Eventos por Usuario Finalizado")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se pudo traer los Eventos Finalizados")]
    public ActionResult TraerEventosPorUsuarioFinalizado([FromBody] DtoEventoId eventoDto)
    {

        var eventos = _eventoServices.GetEventosCreadosPorUsuarioFinalizado(eventoDto.Id,5, 0).ToArray();
        return Ok(eventos);

    }

    [HttpPost("buscarEventoPorDeporteCiudad", Name = "buscarEventoPorDeporteCiudad")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Buscador de evento por nombre, ciudad, deporte o provincia")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se pudo traer los Eventos buscados")]
    public ActionResult BuscarEventoPorDeporteCiudad([FromBody] DtoBuscador buscador)
    {

        var eventos = _eventoServices.BuscadorDeEventosConDeporte(buscador.Buscador);
        return Ok(eventos);

    }

    [HttpPost("agregarResultadoDelEvento", Name = "agragarResultadoDelEvento")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite agregar un resultado")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha agregado el resultado del partido")]
    public ActionResult AgregarResultadoDelEvento([FromBody] DtoAgregarResultado resultadoDto)
    {

        _eventoServices.AgregarResultado(resultadoDto.IdEvento, resultadoDto.ResultadoLocal, resultadoDto.ResultadoVisitante);
        return Ok();

    }
}
