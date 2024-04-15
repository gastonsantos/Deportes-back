using Deportes.Modelo.Custom;
using Deportes.Modelo.HistorialRefreshModel;
using Deportes.Modelo.UsuarioModel;
using Deportes.Servicio.Servicios.UsuarioServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IUsuario;

public interface IUsuarioRepository
{
    public IList<DtoUsuario> GetAll();
    public Usuario? ObtenerUsuarioMailContraseña(string email, string contra);

    public Usuario? ObtenerUsuarioPorId(int id);

    public void GuardarHistorialRefreshToken(HistorialRefreshToken historial);
    public HistorialRefreshToken? DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest, int idUsuario);
    public int GuardarUsuarioEnBd(Usuario usuario);
    public Usuario ObtenerUsuarioPorEmail(string email);

    public Usuario ObtenerUsuarioPorToken(string token);
    public bool ConfirmarEmailUsuarioYNullearToken(string token);
    public void GuardoTokenCambioContraseniaPorEmailUsuario(string email, string tokenCambio);
    public void CambioContraseniaPorToken(string contraseniaNueva, string tokenCambio);
    public Usuario ObtenerUsuarioPorTokenCambioContrasenia(string token);
}
