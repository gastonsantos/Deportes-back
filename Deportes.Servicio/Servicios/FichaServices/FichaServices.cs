using Deportes.Modelo.FichaDeportistaModel;
using Deportes.Modelo.FichaFutbolModel;
using Deportes.Servicio.Interfaces.IDeporte;
using Deportes.Servicio.Interfaces.IFichas;
using Deportes.Servicio.Interfaces.IUsuario;
using Deportes.Servicio.Servicios.FichaServices.Dto;
using Deportes.Servicio.Servicios.UsuarioServices.Errores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Servicios.FichaServices;

public class FichaServices : IFichaServices
{
    private readonly IFichaRepository _fichaRespoitory;
    private readonly IUsuarioService _usuarioService;
    public FichaServices(IFichaRepository fichaRespoitory, IUsuarioService usuarioService)
    {
        _fichaRespoitory = fichaRespoitory;
        _usuarioService = usuarioService;
    }

    public void ActualizarFichaDeportista(DtoFichaDeportista fichaDeportista)
    {
        var usuario = _usuarioService.ObtenerUsuarioPorId(fichaDeportista.IdUsuario);
        if(usuario == null)
        {
            throw new UsuarioNoEncontradoException();
        }
        _fichaRespoitory.ActualizarFichaDeportista(fichaDeportista);
    }

    public void ActualizarFichaFutbol(FichaFutbol fichaFutbol)
    {
        _fichaRespoitory.ActualizarFichaFutbol(fichaFutbol);
    }

    public void AgregarFichaDeportista(DtoFichaDeportista fichaDeportista)
    {
        FichaDeportistum fichaDep = new FichaDeportistum();
         var usuario = _usuarioService.ObtenerUsuarioPorId(fichaDeportista.IdUsuario);
        if(usuario == null)
        {
            throw new UsuarioNoEncontradoException();
        }

        fichaDep.IdUsuario = fichaDeportista.IdUsuario;
        fichaDep.Avatar = fichaDeportista.Avatar;
        fichaDep.Altura = fichaDeportista.Altura;
        fichaDep.Edad = fichaDeportista.Edad;
        fichaDep.Peso = fichaDeportista.Peso;
        fichaDep.ManoHabil = fichaDeportista.ManoHabil;
        fichaDep.PieHabil = fichaDeportista.PieHabil;
        fichaDep.Posicion = fichaDeportista.Posicion;


        _fichaRespoitory.AgregarFichaDeportista(fichaDep);
    }

    public void AgregarFichaFutbol(FichaFutbol fichaFutbol)
    {
        _fichaRespoitory.AgregarFichaFutbol(fichaFutbol);
    }

    public DtoFichaDeportista DevolverFichaDeportistaPorId(int id)
    {
        var usuario = _usuarioService.ObtenerUsuarioPorId(id);
        if(usuario == null)
        {
            throw new UsuarioNoEncontradoException();
        }
        DtoFichaDeportista fichaDep = new DtoFichaDeportista();
        var deportista = _fichaRespoitory.DevolverFichaDeportistaPorId(usuario.Id);

        fichaDep.Altura = deportista.Altura;
        fichaDep.Avatar = deportista.Avatar;
        fichaDep.Edad= deportista.Edad;
        fichaDep.Peso = deportista.Peso;
        fichaDep.IdUsuario= deportista.IdUsuario;
        fichaDep.ManoHabil = deportista.ManoHabil;
        fichaDep.PieHabil = deportista.PieHabil;
        fichaDep.Posicion= deportista.Posicion;
        return fichaDep;
    }

    public FichaFutbol DevolverFichaFutbol(int id)
    {
        return _fichaRespoitory.DevolverFichaFutbol(id);
    }
}
