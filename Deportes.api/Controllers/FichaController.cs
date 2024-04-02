using Deportes.api.Controllers.Dto;
using Deportes.Servicio.Servicios.FichaServices.Dto;
using Deportes.Servicio.Interfaces.IDeporte;
using Deportes.Servicio.Interfaces.IFichas;
using Deportes.Servicio.Interfaces.IToken;
using Deportes.Servicio.Interfaces.IUsuario;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Deportes.api.Controllers;

[ApiController]
[Route("fichas")]
public class FichaController : Controller
{

    private readonly IFichaServices _fichaServices;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public IConfiguration _configuration;
    public FichaController(IFichaServices fichaServices ,IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _fichaServices= fichaServices;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
      
    }

    [HttpPost("AgregarFichaDeportista", Name = "AgragarFichaDeportista")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite agregar la ficha de deportista basica")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha agregado la ficha de deportista clasica")]
    public ActionResult AgregarFichaDeportista([FromBody] DtoFichaDeportista fichaDeportista)
    {

        _fichaServices.AgregarFichaDeportista(fichaDeportista);
        return Ok();

    }


    [HttpPost("ActualizaFichaDeportista", Name = "ActualizaFichaDeportista")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite actualizar la ficha de deportista basica")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha actualizado la ficha de deportista basica")]
    public ActionResult ActualizaFichaDeportista([FromBody] DtoFichaDeportista fichaDeportista)
    {

        _fichaServices.ActualizarFichaDeportista(fichaDeportista);
        return Ok();

    }

    [HttpPost("ObtieneFichaDeportista", Name = "ObtieneFichaDeportista")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite obtener la ficha de deportista basica")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha obtenido la ficha de deportista basica")]
    public ActionResult ObtieneFichaDeportista([FromBody] DtoUsuarioPerfil fichaDeportista)
    {
        var respuesta = _fichaServices.DevolverFichaDeportistaPorId(fichaDeportista.Id);
         return Ok(respuesta);
        

    }


    /*Agregar eliminar leer y buscar una ficha de estadisticas de futbol*/
    [HttpPost("AgregarFichaFutbol", Name = "AgregarFichaFutbol")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite agregar la ficha de futbol basica")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha agregado la ficha de futbol clasica")]
    public ActionResult AgregarFichaFutbol([FromBody] DtoFutbolServices dtoFutbol)
    {

        _fichaServices.AgregarFichaFutbol(dtoFutbol);
        return Ok();

    }

        [HttpDelete("EliminarFichaFutbol/{id}", Name = "EliminarFichaFutbol")]
       [Produces("application/json")]
       [SwaggerOperation(Summary = "Permite eliminar la ficha de futbol basica")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha eliminado la ficha de futbol clasica")]
        public ActionResult EliminarFichaFutbol(int id)
        {
            

            _fichaServices.EliminarFichaFutbol(id);
            return Ok();
        }

        [HttpGet("ObtenerFichaFutbol/{id}", Name = "ObtenerFichaFutbol")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Permite buscar por id la ficha de futbol basica")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha encontrado la ficha de futbol clasica")]
        public ActionResult ObtenerFichaFutbol(int id)
        {
            var fichaFutbol = _fichaServices.DevolverFichaFutbol(id);
           

            return Ok(fichaFutbol);
        }

        [HttpGet("BuscarFichaFutbol", Name = "BuscarFichaFutbol")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Permite buscar todas  las ficha de futbol basica")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se han traido todas las  fichas de futbol clasica")]
        public ActionResult BuscarFichaFutbol()
        {
            var fichasFutbol = _fichaServices.DevolverFichasDeFutbol();
            return Ok(fichasFutbol);
        }

    /*Agregar, buscar, eliminar y actualizar fichas de tenis*/ 
[HttpPost("AgregarFichaTenis", Name = "AgregarFichaTenis")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite agregar la ficha de tenis basica")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha agregado la ficha de tenis clasica")]
    public ActionResult AgregarFichaTenis([FromBody] DtoFichaTenis dtoTenis)
    {

        _fichaServices.AgregarFichaTenis(dtoTenis);
        return Ok();

    }

        [HttpDelete("EliminarFichaFutbol/{id}", Name = "EliminarFichaFutbol")]
       [Produces("application/json")]
       [SwaggerOperation(Summary = "Permite eliminar la ficha de futbol basica")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha eliminado la ficha de futbol clasica")]
        public ActionResult EliminarFichaFutbol(int id)
        {
            

            _fichaServices.EliminarFichaFutbol(id);
            return Ok();
        }

        [HttpGet("ObtenerFichaFutbol/{id}", Name = "ObtenerFichaFutbol")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Permite buscar por id la ficha de futbol basica")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se ha encontrado la ficha de futbol clasica")]
        public ActionResult ObtenerFichaFutbol(int id)
        {
            var fichaFutbol = _fichaServices.DevolverFichaFutbol(id);
           

            return Ok(fichaFutbol);
        }

        [HttpGet("BuscarFichaFutbol", Name = "BuscarFichaFutbol")]
        [Produces("application/json")]
        [SwaggerOperation(Summary = "Permite buscar todas  las ficha de futbol basica")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se han traido todas las  fichas de futbol clasica")]
        public ActionResult BuscarFichaFutbol()
        {
            var fichasFutbol = _fichaServices.DevolverFichasDeFutbol();
            return Ok(fichasFutbol);
        }


}

