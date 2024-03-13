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

public class ParticipantesController : Controller
{
    private readonly IParticipantesServices _participantesServices;
   
    public IConfiguration _configuration;
    public ParticipantesController( IConfiguration configuration,IParticipantesServices participantesServices )
    {
       
        _configuration = configuration;
        _participantesServices= participantesServices;
    }


    [HttpGet("AgregarParticipante", Name = "AgregarParticipante")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite hacer una prueba con datos HardCodeados de envio de notificacion")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se Envio la notificacion de forma correcta")]
    public ActionResult AgregarParticipantes()
    {
        int eventoId2 = 1;
        int userId = 1003;
        _participantesServices.EnviarNotificacionParticipante(eventoId2, userId);
        return Ok();
    }

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



}
