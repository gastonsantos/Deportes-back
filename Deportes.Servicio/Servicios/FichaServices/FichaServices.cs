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

    public void AgregarFichaFutbol(DtoFutbolServices fichaFutbol)
    {
        var fichaDeFutbol = new FichaFutbol();
        fichaDeFutbol.Disparo = fichaFutbol.Disparo;
        fichaDeFutbol.Fuerza= fichaFutbol.Fuerza;
        fichaDeFutbol.Velocidad=fichaFutbol.Velocidad;
        fichaDeFutbol.Defensa=fichaFutbol.Defensa;
        fichaDeFutbol.Regate=fichaFutbol.Regate;
        fichaDeFutbol.IdUsuario=fichaFutbol.IdUsuario;

 


        _fichaRespoitory.AgregarFichaFutbol(fichaDeFutbol);
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

        fichaDep.IdUsuario = deportista.IdUsuario;
        fichaDep.Altura = deportista.Altura ?? null;
        fichaDep.Avatar = deportista.Avatar ?? null;
        fichaDep.Edad = deportista.Edad ?? null;
        fichaDep.Peso = deportista.Peso ?? null;
        fichaDep.ManoHabil = deportista.ManoHabil ?? null;
        fichaDep.PieHabil = deportista.PieHabil ?? null;
        fichaDep.Posicion = deportista.Posicion ?? null;
        return fichaDep;
    }

    public FichaFutbol DevolverFichaFutbol(int id)
    {
        return _fichaRespoitory.DevolverFichaFutbol(id);
    }
}
