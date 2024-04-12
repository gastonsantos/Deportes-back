using Deportes.Modelo.FichaBasquetModel;
using Deportes.Modelo.FichaDeportistaModel;
using Deportes.Modelo.FichaFutbolModel;
using Deportes.Modelo.FichaTenisModel;
using Deportes.Servicio.Interfaces.IFichas;
using Deportes.Servicio.Servicios.FichaServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Infra.Database.FichaRepository;

public class FichaRepository : IFichaRepository
{

    private readonly DeportesContext _context;

    public FichaRepository(DeportesContext context)
    {
        _context = context;
    }

    public FichaDeportistum DevolverFichaDeportistaPorId(int id)
    {
        var ficha = _context.FichaDeportista.FirstOrDefault(f => f.IdUsuario == id);
        return ficha;
    }

    public  FichaFutbol DevolverFichaFutbol(int id)
    {
        var fichaFutbol = _context.FichaFutbols.FirstOrDefault(f => f.IdUsuario == id);
        return fichaFutbol;
    }

    public void AgregarFichaDeportista(FichaDeportistum fichaDeportista)
    {
        _context.FichaDeportista.Add(fichaDeportista);
        _context.SaveChanges();
    }

    public void AgregarFichaFutbol (FichaFutbol fichaFutbol)
    {
        _context.FichaFutbols.Add(fichaFutbol);
        _context.SaveChanges();
    }

    public void ActualizarFichaDeportista(DtoFichaDeportista fichaDeportista)
    {
        var ficha = DevolverFichaDeportistaPorId(fichaDeportista.IdUsuario);

        ficha.Avatar = fichaDeportista?.Avatar;
        ficha.Altura = fichaDeportista?.Altura;
        ficha.Edad = fichaDeportista?.Edad;
        ficha.ManoHabil = fichaDeportista?.ManoHabil;
        ficha.PieHabil = fichaDeportista?.PieHabil;
        ficha.Peso = fichaDeportista?.Peso;
        ficha.Posicion = fichaDeportista?.Posicion;

        _context.SaveChanges();
        
    }

    public void ActualizarFichaFutbol(FichaFutbol fichaFutbol)
    {
        var fichaFulbo = DevolverFichaFutbol(fichaFutbol.IdUsuario);

        fichaFulbo.Disparo = fichaFutbol?.Disparo;
        fichaFulbo.Fuerza = fichaFutbol?.Fuerza;
        fichaFulbo.Velocidad = fichaFutbol?.Velocidad;
        fichaFulbo.Defensa  = fichaFutbol?.Defensa;
        fichaFulbo.Pase = fichaFutbol?.Pase;
        fichaFulbo.Regate = fichaFutbol?.Regate;

        _context.SaveChanges(); 

    }


    public List<FichaFutbol> DevolverFichasDeFutbol(){
             return _context.FichaFutbols.ToList();
    }

      public void EliminarFichaFutbol(FichaFutbol ficha){

        _context.FichaFutbols.Remove(ficha);
        _context.SaveChanges();      }

     public bool ActaulizarFichaFutbol(FichaFutbol ficha){
        try
                {
                    _context.FichaFutbols.Update(ficha);
                    _context.SaveChanges();
                    return true; // Actualización exitosa
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción aquí
                    Console.WriteLine($"Error al actualizar la ficha de tenis: {ex.Message}");
                    return false; // Actualización fallida
                }
     }   

    public void AgregarFichaTenis(FichaTeni fichaTenis){
        _context.FichaTeni.Add(fichaTenis);
                _context.SaveChanges();

        
    }
   
    public void EliminarFichaTenis(FichaTeni fichaTenis){
        _context.FichaTeni.Remove(fichaTenis);
        _context.SaveChanges();
    }

    public FichaTeni DevolverFichaTenis(int id ){

        var ficha = _context.FichaTeni.FirstOrDefault(f => f.Id == id);

        return ficha;
    }

    public List<FichaTeni> DevolverFichasTenis(){

            return _context.FichaTeni.ToList();
    }

     public bool ActualizarFichaTenis(FichaTeni fichaDeTenis)
     {
              try
                {
                    _context.FichaTeni.Update(fichaDeTenis);
                    _context.SaveChanges();
                    return true; // Actualización exitosa
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción aquí
                    Console.WriteLine($"Error al actualizar la ficha de tenis: {ex.Message}");
                    return false; // Actualización fallida
                }

     }

        /*repositorios de ficha de basquet actualizar, crear, eliminar y buscar */
         public void AgregarFichaBasquet(FichaBasquet fichaBasquet){
                _context.FichaBasquet.Add(fichaBasquet);
                _context.SaveChanges();

         }
        public  FichaBasquet ObtenerFichaBasquet(int id){
        var fichaBasquet = _context.FichaBasquet.First(f => f.Id  == id);
        return fichaBasquet;
        }
   public void EliminarFichaBasquet(FichaBasquet ficha){
    _context.FichaBasquet.Remove(ficha);
     _context.SaveChanges();

   }
   public  List<FichaBasquet> BuscarFichaBasquet(){
            return _context.FichaBasquet.ToList();

   }
    public void ActualizarFichaBasquet(FichaBasquet fichaBasquet){
        _context.FichaBasquet.Update(fichaBasquet);
    }


}
