using Deportes.Modelo.UsuarioModel;
using Deportes.Servicio.Interfaces.IToken;
using Deportes.Servicio.Interfaces.IUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.TokenServices;

public class TokenServices : ITokenService
{
    private readonly IUsuarioService _usuarioService;

    public TokenServices(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }
    public string validarToken(ClaimsIdentity identity)
    {
        
            var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;

            Usuario usuario = _usuarioService.ObtenerUsuarioPorId(int.Parse(id));

        if(usuario != null)
        {
            return usuario.Nombre;
        }

        return "No se encotnro Nombre";
    }


}
