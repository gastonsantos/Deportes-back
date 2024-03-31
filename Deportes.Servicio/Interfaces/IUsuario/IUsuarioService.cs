using Deportes.Modelo.Custom;
using Deportes.Modelo.UsuarioModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IUsuario
{
    public interface IUsuarioService
    {
        public IList<Usuario> GetAll();
        public Usuario? ObtenerUsuarioMailContraseña(string email, string contra);
        public Usuario? ObtenerUsuarioPorId(int id);
        public void GuardarUsuarioEnBd(string nombre, string apellido,string apodo,string email, string contrasenia, string provincia, string localidad, string direccion, string numero);
        public Usuario ObtenerUsuarioPorEmail(string email);
        public Usuario ObtenerUsuarioPorToken(string token);
        public bool ConfirmarEmailUsuario(string token);
        public bool ConfirmarEmailUsuarioYNullearToken(string token);
        public bool EnvioCambiarContrasenia(string email);
        public bool CambioContrasenia(string constraseniaNueva, string token);
    }
}
