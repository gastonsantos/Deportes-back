using Deportes.api.Controllers.Dto;
using Deportes.Modelo.Custom;
using Deportes.Servicio.Interfaces.IDeporte;
using Deportes.Servicio.Interfaces.IParticipantes;
using Deportes.Servicio.Interfaces.IToken;
using Deportes.Servicio.Interfaces.IUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Deportes.api.Controllers;

[ApiController]
[Route("participantes")]
public class ParticipantesController : Controller
{
    private readonly IParticipantesServices _participantesServices;
   
    public IConfiguration _configuration;
    public ParticipantesController( IConfiguration configuration,IParticipantesServices participantesServices )
    {
       
        _configuration = configuration;
        _participantesServices= participantesServices;
    }

    [Authorize]
    [HttpPost("AgregarParticipante", Name = "AgregarParticipante")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite hacer una prueba con datos HardCodeados de envio de notificacion")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se Envio la notificacion de forma correcta")]
    public ActionResult AgregarParticipantes([FromBody] DtoNotificacionesEnviar dto)
    {
        _participantesServices.EnviarNotificacionParticipante(dto.idEvento, dto.idUsuarioQueInvita, dto.idUsuarioInvitado);
        return Ok();
    }
    [Authorize]
    [HttpPost("ObtenerNotificaciones", Name = "ObtenerNotificaciones")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite obtener las notificaciones")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se reciben las Notificaciones correctamente")]
    public ActionResult ObtenerNotificaciones([FromBody] DtoNotificaciones notificacion)
    {

        var notificaciones = _participantesServices.ObtengoNotificacionParticipante(notificacion.idUsuario);
        return Ok(notificaciones);
    }

    [Authorize]
    [HttpPost("ObtenerNotificacionesPorIdUsuario", Name = "ObtenerNotificacionesPorIdUsuario")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite obtener las notificaciones")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se reciben las Notificaciones correctamente")]
    public ActionResult ObtenerNotificacionesPorUsuario([FromBody] DtoNotificaciones notificacion)
    {

        var notificaciones = _participantesServices.ObtenerNotificacionesPorUsuario(notificacion.idUsuario);
        return Ok(notificaciones);
    }
    [Authorize]
    [HttpPost("AceptarInvitacion", Name = "AceptarInvitacion")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite obtener las notificaciones")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se acepto la notificacion satisfactoriamente")]
    public ActionResult AceptoLaInvitacion([FromBody] DtoNotificaciones notificacion)
    {

         _participantesServices.AceptarParticipante(notificacion.idUsuario);
        return Ok();
    }
    [Authorize]
    [HttpPost("RechazarInvitacion", Name = "RechazarInvitacion")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite obtener las notificaciones")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se rechazo la invitacion satisfactoriamente")]
    public ActionResult RechazoLaInvitacion([FromBody] DtoNotificaciones notificacion)
    {

        _participantesServices.EliminarParticipante(notificacion.idUsuario);
        return Ok();
    }
}
