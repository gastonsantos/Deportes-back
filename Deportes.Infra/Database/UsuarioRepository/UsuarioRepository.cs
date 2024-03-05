
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

namespace Deportes.Infra.Database.UsuarioRepository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DeportesContext _context;

    public UsuarioRepository(DeportesContext context)
    {
        _context = context;
    }

    public IList<Usuario> GetAll() //Todos los usuarios
    {
        return _context.Usuario.ToList();
    }

    public Usuario? ObtenerUsuarioMailContraseña(string email, string contra)
    {
        return _context.Usuario.FirstOrDefault(u => u.Email == email && u.Contrasenia == contra);
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
 


}
