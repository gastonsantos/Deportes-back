using Azure;
using Deportes.api.Controllers.Dto;
using Deportes.Modelo.Custom;
using Deportes.Modelo.JwtModel;
using Deportes.Modelo.UsuarioModel;
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
    private readonly ITokenService _tokenService;
    private readonly IAutorizacionService _autorizacionService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public IConfiguration _configuration;
    public UsuarioController(IUsuarioService usuarioService, IConfiguration configuration, ITokenService tokenService, IAutorizacionService autorizacionService, IHttpContextAccessor httpContextAccessor)
    {
        _usuarioSerive = usuarioService;
        _configuration = configuration;
        _tokenService = tokenService;
        _autorizacionService = autorizacionService;
        _httpContextAccessor = httpContextAccessor;
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

    /*
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Permite Devolver un Usuario Por Email y Contraseña")]
    [SwaggerResponse(400, "El objeto request es invalido.")]
    [SwaggerResponse(200, "Se devuelve un Usuario por Email y Contraseña")]
    [HttpPost("Login", Name = "Login")]

    public ActionResult Login([FromBody] DtoUserLogin userDto)

    {

        Usuario usuario = _usuarioSerive.ObtenerUsuarioMailContraseña(userDto.Email, userDto.Contrasenia);
        if (usuario == null)
        {
            return Ok("No se encontro ningun Usuario con ese Email y Contraseña");
        }

        var jwt = _configuration.GetSection("Jwt").Get<Jwt>(); // aca lo que hago es traer el Jwt del appSetting y lo puedo usar como un objeto de Modelo que hice 
                                                               //estos son los datos que se van a guardar en el JWT
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject), //datos de configuracion
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//datos de configuracion
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),//datos de configuracion
            new Claim ("usuario", usuario.Nombre), // datos extras que se los agrego yo
            new Claim("id", usuario.Id.ToString())  // estos datos siempre tienen que ser string

        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            jwt.Issuer,
            jwt.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: signIn
            );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });


    }

    */
    [HttpPost("Eliminar", Name = "Eliminar")]

    public ActionResult EliminarUsuario([FromBody] DtoUserBorrar userDto)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if(identity == null)
        {
            return BadRequest("sin Token");
        }
        string result = _tokenService.validarToken(identity);
       
        return Ok(result);

    }

}
