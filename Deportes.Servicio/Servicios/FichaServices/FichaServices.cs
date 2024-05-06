using Deportes.Modelo.FichaDeportistaModel;
using Deportes.Modelo.FichaFutbolModel;
using Deportes.Modelo.FichaTenisModel;
using Deportes.Modelo.FichaBasquetModel;
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
    private static Random random = new Random();
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
       


        _fichaRespoitory.AgregarFichaDeportista(fichaDep);
    }

    public void AgregarFichaFutbol(DtoFutbolServices dtoFutbolServices)
    {
        var media = CalcularMedia( dtoFutbolServices.Velocidad, dtoFutbolServices.Resistencia, dtoFutbolServices.Precision, dtoFutbolServices.Fuerza,  
           dtoFutbolServices.Tecnica, dtoFutbolServices.Agilidad
           );

        var fichaDeFutbol = new FichaFutbol();
        fichaDeFutbol.Velocidad = dtoFutbolServices.Velocidad;
        fichaDeFutbol.Resistencia = dtoFutbolServices.Resistencia;
        fichaDeFutbol.Fuerza= dtoFutbolServices.Fuerza;
        fichaDeFutbol.Precision= dtoFutbolServices.Precision;
        fichaDeFutbol.Tecnica= dtoFutbolServices.Tecnica;
        fichaDeFutbol.Agilidad= dtoFutbolServices.Agilidad;
        fichaDeFutbol.IdUsuario= dtoFutbolServices.IdUsuario;
       
        fichaDeFutbol.Media = media;
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
        
        return fichaDep;
    }

    public FichaFutbol DevolverFichaFutbol(int id)
    {
        return _fichaRespoitory.DevolverFichaFutbol(id);
    }

    public List<FichaFutbol> DevolverFichasDeFutbol(){

            return _fichaRespoitory.DevolverFichasDeFutbol();
        }

     public void EliminarFichaFutbol(int id)
     {

        var ficha =  _fichaRespoitory.DevolverFichaFutbol(id);
        if(ficha != null)
        {
        _fichaRespoitory.EliminarFichaFutbol(ficha);
        }


     }

     public void ActaulizarFichaFutbol( DtoFutbolServices dtoFutbolServices){
        var media = CalcularMedia( dtoFutbolServices.Velocidad, dtoFutbolServices.Resistencia, dtoFutbolServices.Precision, dtoFutbolServices.Fuerza,
           dtoFutbolServices.Tecnica, dtoFutbolServices.Agilidad
            );

        var ficha = _fichaRespoitory.DevolverFichaFutbol(dtoFutbolServices.IdUsuario);
            if(ficha != null)
            {
            ficha.Resistencia = dtoFutbolServices.Resistencia;
            ficha.Fuerza= dtoFutbolServices.Fuerza;
            ficha.Velocidad=dtoFutbolServices.Velocidad;
            ficha.Precision =dtoFutbolServices.Precision;
            ficha.Tecnica= dtoFutbolServices.Tecnica;
            ficha.Agilidad=dtoFutbolServices.Agilidad;
            
            ficha.Media = media;
        _fichaRespoitory.ActaulizarFichaFutbol(ficha);

            }
     }

    private int CalcularMedia(int disparo, int fuerza, int velocidad, int defensa, int pase, int regate)
    {
        int media = 0;
        
        int randomCase = random.Next(1, 5);
        switch (randomCase)
        {
            case 1:

                media = (defensa * 3 + disparo + fuerza + velocidad) / 6;
                return media;

            case 2:
                media = (defensa * 2 + disparo + fuerza * 2 + velocidad) / 6;
                return media;
            case 3:
                media = (disparo + fuerza + velocidad + defensa + pase + regate) / 6;
                return media;
            case 4:
                media = (disparo * 2 + fuerza + velocidad + pase + regate)/6;
                return media;

            default:
                break;
        }
                 throw new ArgumentException("Posición no válida");
        
    }

    //agregar ficha de tenis, leer, buscar, y eliminar 
            public void AgregarFichaTenis(FichaTeni dtoTenis){
               
                _fichaRespoitory.AgregarFichaTenis(dtoTenis);
            }


        public void EliminarFichaTenis(int id){

            var ficha = _fichaRespoitory.DevolverFichaTenis(id);
                if(ficha != null)
                {
                     _fichaRespoitory.EliminarFichaTenis(ficha);

                }

        }

        public FichaTeni DevolverFichaTenis(int id){
            return _fichaRespoitory.DevolverFichaTenis(id);
        }

        public List<FichaTeni> DevolverFichasDeTenis(){
            return _fichaRespoitory.DevolverFichasTenis();
        }

        public void ActaulizarFichaTenis(int id,DtoFichaTenis dtoFichaTenisController){
                
               var fichaDeTenis = _fichaRespoitory.DevolverFichaTenis(id);
               if(fichaDeTenis != null){
                    fichaDeTenis.Fuerza = dtoFichaTenisController.Fuerza;
                    fichaDeTenis.Drive= dtoFichaTenisController.Drive;
                    fichaDeTenis.Servicio= dtoFichaTenisController.Servicio;
                    fichaDeTenis.Reves= dtoFichaTenisController.Reves;
                    fichaDeTenis.Velocidad = dtoFichaTenisController.Velocidad;
                    fichaDeTenis.Volea = dtoFichaTenisController.Volea;
                    _fichaRespoitory.ActualizarFichaTenis(fichaDeTenis);  
               }

        }


        /*fichas de basquet */
        public void AgregarFichaBasquet(FichaBasquet fichaBasquet){
            _fichaRespoitory.AgregarFichaBasquet(fichaBasquet);
         }
        public void EliminarFichaBasquet(int id)
        {
            var ficha = _fichaRespoitory.ObtenerFichaBasquet(id);
                if(ficha != null)
                {
                    _fichaRespoitory.EliminarFichaBasquet(ficha);
                }
        }
        public FichaBasquet ObtenerFichaBasquet(int id){
          return  _fichaRespoitory.ObtenerFichaBasquet( id);
        }
        public List<FichaBasquet> BuscarFichaBasquet(){
            return _fichaRespoitory.BuscarFichaBasquet();
        }
        public  void ActualizarFichaBasquet(int id, DtoFichaBasquet dtoFichaBasquet){
                var fichaBasquet = _fichaRespoitory.ObtenerFichaBasquet(id);
                if(fichaBasquet != null){
                    fichaBasquet.Finalizacion= dtoFichaBasquet.Finalizacion;
                    fichaBasquet.Organizacion= dtoFichaBasquet.Organizacion;
                    fichaBasquet.Fuerza= dtoFichaBasquet.Fuerza;
                    fichaBasquet.Velocidad= dtoFichaBasquet.Velocidad;
                    fichaBasquet.Tiro= dtoFichaBasquet.Tiro;
                    fichaBasquet.Defensa= dtoFichaBasquet.Defensa;
                    _fichaRespoitory.ActualizarFichaBasquet(fichaBasquet);
                    
                }
        }


}



