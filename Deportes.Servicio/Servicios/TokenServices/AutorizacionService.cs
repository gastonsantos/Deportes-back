using Deportes.Modelo.Custom;
using Deportes.Modelo.HistorialRefreshModel;
using Deportes.Modelo.UsuarioModel;
using Deportes.Servicio.Interfaces.IToken;
using Deportes.Servicio.Interfaces.IUsuario;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;




namespace Deportes.Servicio.Servicios.TokenServices
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;
        
        public AutorizacionService (IConfiguration configuration, IUsuarioRepository usuarioRepository )
        {
            _configuration = configuration;
            _usuarioRepository  = usuarioRepository;
            
        }

        private string GenerarToken (Usuario usuario)
        {
           
            var key = _configuration.GetValue<string>("Jwt:key");

            var keyBytes = Encoding.ASCII.GetBytes (key); // convierte la clave secreta de texto a bytes usando ASCII

            var claims = new ClaimsIdentity(); // crea un new Claims para almacenar informacion del usuario al token

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));// le agregar informacion del usuario, Id, Nombre, email
            claims.AddClaim(new Claim(ClaimTypes.Surname, usuario.Nombre));
            claims.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));

            var credencialesToeken = new SigningCredentials( // crea las credenciales de firma con el algoritmo que es HmaSha246 y la clave secreta que es keyBytes
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescription = new SecurityTokenDescriptor // define propiedades al Token 
            { 
                Subject = claims, //son las Claims que contienen la info del usuario
                Expires = DateTime.UtcNow.AddMinutes(30), // fecha y hora de expiracion del token
                SigningCredentials = credencialesToeken // las credenciales de firma para asegurar el token
            };
            var tokenHandler = new JwtSecurityTokenHandler(); // se encarga el TokenHandler de la creacion y analiss de tokens JWT
            var tokenConfig = tokenHandler.CreateToken(tokenDescription); // lo crea apartir del Description definido anteriormente
            string tokenCreado = tokenHandler.WriteToken(tokenConfig); // lo convierte en String

            return tokenCreado;

        }

        private string GenerarRefreshToken()
        {

            var byteArray = new byte[64];
            var refreshToken = "";
            using (var rng= RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }
            return refreshToken;
        }

        private async Task<AutorizacionResponse> GuardarHistorialRefreshToken(int idUsuario, string token, string refreshToken)
        {
            var historialRefreshToken = new HistorialRefreshToken
            {
                IdUsuario = idUsuario,
                Token = token,
                RefreshToken = refreshToken,
                FechaCreacion = DateTime.UtcNow,
                FechaExpiracion = DateTime.UtcNow.AddMinutes(60)
            };

             _usuarioRepository.GuardarHistorialRefreshToken(historialRefreshToken);

            return new AutorizacionResponse
            {
                Token= token,
                RefreshToken= refreshToken,
                Resultado = true,
                Msg = "Ok"
            };
        }


        public async Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion)
        {
            var usuario_encontrado = _usuarioRepository.ObtenerUsuarioMailContraseña(autorizacion.Email, autorizacion.Clave);

            if(usuario_encontrado == null)
            {
                return await Task.FromResult<AutorizacionResponse>(null);
            }

            string tokenCreado = GenerarToken(usuario_encontrado);

            string refreshToken = GenerarRefreshToken();

            return await GuardarHistorialRefreshToken(usuario_encontrado.Id, tokenCreado, refreshToken);
        }

        public async Task<AutorizacionResponse> DevolverRefreshToken(RefreshTokenRequest refreshToken, int idUsuario)
        {
            var refreshTokenEncontrado = _usuarioRepository.DevolverRefreshToken(refreshToken, idUsuario);
            var usuario_encontado = _usuarioRepository.ObtenerUsuarioPorId(idUsuario);

            if(refreshTokenEncontrado == null)
            {
                return new AutorizacionResponse { Resultado = false, Msg = "No existe Token o Refresh Token" };
            }
            var refreshTokenCreado = GenerarRefreshToken();
            var tokenCreado = GenerarToken(usuario_encontado);

            return await GuardarHistorialRefreshToken(usuario_encontado.Id, tokenCreado, refreshTokenCreado);
        }
    }
}
