
using Deportes.Modelo.UsuarioModel;
using Deportes.Modelo.HistorialRefreshModel;
using Deportes.Servicio.Interfaces.IUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Deportes.Modelo.Custom;
using Deportes.Servicio.Servicios.UsuarioServices.Dto;
using Deportes.Modelo.EventoModel.Dto;

namespace Deportes.Infra.Database.UsuarioRepository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DeportesContext _context;

    public UsuarioRepository(DeportesContext context)
    {
        _context = context;
    }

    public IList<DtoUsuario> GetAll() //Todos los usuarios
    {
        return _context.Usuario
          .Where(e => e.Activo == true)
          .Select(e => new DtoUsuario
          {
              Id =e.Id,
              Nombre = e.Nombre,
              Apellido = e.Apellido,
              Apodo = e.Apodo,
              Email= e.Email,

          })
          .ToList();
       
    }

    
    public Usuario ObtenerUsuarioMailContraseña(string email, string contra)
    {
        return _context.Usuario.FirstOrDefault(u => u.Email == email && u.Contrasenia == contra);
    }

    public Usuario ObtenerUsuarioPorEmail(string email)
    {
        return _context.Usuario.FirstOrDefault(u => u.Email == email && u.Activo == true);

    }
    public int GuardarUsuarioEnBd(Usuario usuario)
    {
        _context.Usuario.Add(usuario);
        _context.SaveChanges();
        return usuario.Id;
    }

    public Usuario? ObtenerUsuarioPorId(int id)
    {
        return _context.Usuario.FirstOrDefault(u => u.Id == id);
    }

    public  void GuardarHistorialRefreshToken(HistorialRefreshToken historial)
    {
        _context.HistorialRefreshTokens.Add(historial); 
        _context.SaveChanges();

    }

    public HistorialRefreshToken? DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest, int idUsuario)
    {
        return _context.HistorialRefreshTokens.FirstOrDefault(x => x.Token == refreshTokenRequest.TokenExpirado &&
        x.RefreshToken == refreshTokenRequest.RefreshToken &&
        x.IdUsuario == idUsuario);
    }

    public Usuario ObtenerUsuarioPorToken(string token)
    {
        return _context.Usuario.FirstOrDefault(u => u.TokenConfirmacion == token);
    }

    public bool ConfirmarEmailUsuarioYNullearToken(string token)
    {
        var usuario = _context.Usuario.FirstOrDefault(u => u.TokenConfirmacion == token);
        if (usuario == null) {
            return false;
        }
        else
        {
            usuario.VerifyEmail = true;
            usuario.TokenConfirmacion = null;
            _context.SaveChanges();
            return true;
        }

    }

    public void GuardoTokenCambioContraseniaPorEmailUsuario(string email, string tokenCambio)
    {
        var usuario = _context.Usuario.FirstOrDefault(u => u.Email == email);
        usuario.TokenCambioContrasenia = tokenCambio;
        _context.SaveChanges(); 

    }

    public void CambioContraseniaPorToken(string contraseniaNueva, string tokenCambio)
    {
        var usuario = _context.Usuario.FirstOrDefault(u => u.TokenCambioContrasenia == tokenCambio);
        usuario.Contrasenia = contraseniaNueva;
        usuario.TokenCambioContrasenia = null;
        _context.SaveChanges();
    }

    public Usuario ObtenerUsuarioPorTokenCambioContrasenia(string token)
    {
        return _context.Usuario.FirstOrDefault(u => u.TokenCambioContrasenia == token);
    }

}
