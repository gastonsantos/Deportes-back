using Azure;
using Deportes.api.Controllers.Dto;
using Deportes.Modelo.Custom;
using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.UsuarioModel;
using Deportes.Servicio.Interfaces.IDeporte;
using Deportes.Servicio.Interfaces.IToken;
using Deportes.Servicio.Interfaces.IUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Deportes.api.Controllers;

[ApiController]
[Route("usuarios")]
public class UsuarioController : Controller
{
    private readonly IUsuarioService _usuarioSerive;
   
    private readonly IAutorizacionService _autorizacionService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDeporteService _deportesService;
    public IConfiguration _configuration;
    public UsuarioController(IDeporteService deporteService ,IUsuarioService usuarioService, IConfiguration configuration, IAutorizacionService autorizacionService, IHttpContextAccessor httpContextAccessor)
    {
        _usuarioSerive = usuarioService;
        _configuration = configuration;
        
        _autorizacionService = autorizacionService;
        _httpContextAccessor = httpContextAccessor;
        _deportesService = deporteService;
    }

    [Authorize] // Autorize es un decorador que se le ponen a todos los endPoint que necesiten tener un Token Valido
    [HttpGet("AllUsers", Name = "AllUsers")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite devolver todos los usuarios")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se devuelven los usuarios.")]
    public IEnumerable<Usuario> GetAll()
    {
        var usuarios = _usuarioSerive.GetAll()
            .ToArray();

        return usuarios;
    }

    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite Devolver un Usuario Por Email y Contraseña")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se devuelve un Usuario por Email y Contraseña")]
    [HttpPost("Login", Name = "Login")]
    
    public async Task<IActionResult> Login([FromBody] AutorizacionRequest autorizacion)
    {
        var resultado_autorizacion = await _autorizacionService.DevolverToken(autorizacion);
        if(resultado_autorizacion == null)
        {
            return Unauthorized();  
        }
        return Ok(resultado_autorizacion);
    }

    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite Devolver un Usuario Por Email y Contraseña")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se devuelve un Usuario por Email y Contraseña")]
    [HttpPost("LoginAuto", Name = "LoginAuto")]

    public async Task<IActionResult> LoginAuto([FromBody] RefreshTokenRequest ReAutorizacion)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenExpiradoSupuestamente = tokenHandler.ReadJwtToken(ReAutorizacion.TokenExpirado);
        if (tokenExpiradoSupuestamente.ValidTo > DateTime.UtcNow)
            return BadRequest(new AutorizacionResponse
            {
                Resultado = false, 
                Msg = "Token No expirado"

            });

        string idUsuario = tokenExpiradoSupuestamente.Claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value.ToString(); 
        var autorizationResponse = await _autorizacionService.DevolverRefreshToken(ReAutorizacion, int.Parse(idUsuario));
        if (autorizationResponse.Resultado)
        {
            return Ok(autorizationResponse);
        }
        else
        {
            return BadRequest(autorizationResponse);
        }
    }

    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite Devolver un Usuario Por Email y Contraseña")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se devuelve un Usuario por Email y Contraseña")]
    [HttpPost("Login2", Name = "Login2")]

    public async Task<IActionResult> Login2([FromBody] AutorizacionRequest autorizacion)
    {
        var resultado_autorizacion = await _autorizacionService.DevolverToken(autorizacion);
        var usuario = _usuarioSerive.ObtenerUsuarioMailContraseña(autorizacion.Email, autorizacion.Clave);

        if (resultado_autorizacion == null)
        {
            return Unauthorized();
        }
        //creo una cookie y le envio en la cookie 
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddMinutes(60),
            Secure = true, // si usas HTTPS
            SameSite = SameSiteMode.Strict
        };

        _httpContextAccessor.HttpContext.Response.Cookies.Append("token", resultado_autorizacion.Token, cookieOptions);
        _httpContextAccessor.HttpContext.Response.Cookies.Append("email", usuario.Email, cookieOptions);
        _httpContextAccessor.HttpContext.Response.Cookies.Append("id", usuario.Id.ToString(), cookieOptions);
        _httpContextAccessor.HttpContext.Response.Cookies.Append("nombre", usuario.Nombre, cookieOptions);
        return Ok(resultado_autorizacion);
    }

   
  


    [Authorize] 
    [HttpGet("AllDeportes", Name = "AllDeportes")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite devolver todos los Deportes")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se devuelven los deprotes.")]
    public IEnumerable<Deporte> GetAllDeportes()
    {
        var deporte = _deportesService.GetAllDeportes()
            .ToArray();

        return deporte;
    }

    [Authorize]
    [HttpPost("UsuarioPerfil", Name = "UsuarioPerfil")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite devolver los datos de un usuario determinado por id")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se devuelve el usuario de manera satisfactoria")]
    public ActionResult UsuarioPorId([FromBody] DtoUsuarioPerfil usuarioDto)
    {

        var usuario = _usuarioSerive.ObtenerUsuarioPorId(usuarioDto.Id);
        return Ok(usuario);


    }



}
