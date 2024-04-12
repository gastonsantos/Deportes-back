using Deportes.Modelo.FichaBasquetModel;
using Deportes.Modelo.FichaDeportistaModel;
using Deportes.Modelo.FichaFutbolModel;
using Deportes.Modelo.FichaTenisModel;
using Deportes.Servicio.Servicios.FichaServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deportes.Servicio.Interfaces.IFichas;

public interface IFichaRepository
{

    public FichaDeportistum DevolverFichaDeportistaPorId(int id);
    public void AgregarFichaDeportista(FichaDeportistum fichaDeportista);
    public FichaFutbol DevolverFichaFutbol(int id);
    public void AgregarFichaFutbol(FichaFutbol fichaFutbol);
    public void ActualizarFichaDeportista(DtoFichaDeportista fichaDeportista);

    public void ActualizarFichaFutbol(FichaFutbol fichaFutbol);

    public List<FichaFutbol> DevolverFichasDeFutbol();
   public void EliminarFichaFutbol(FichaFutbol ficha);
    public void AgregarFichaTenis(FichaTeni fichaTenis);
    public void EliminarFichaTenis(FichaTeni fichaTeni);

    public FichaTeni DevolverFichaTenis(int id );
    public List<FichaTeni> DevolverFichasTenis();
   public bool ActualizarFichaTenis(FichaTeni fichaDeTenis);
   public bool ActaulizarFichaFutbol(FichaFutbol ficha);
    public void AgregarFichaBasquet(FichaBasquet fichaBasquet);
   public  FichaBasquet ObtenerFichaBasquet(int id);
   public void EliminarFichaBasquet(FichaBasquet ficha);
   public  List<FichaBasquet> BuscarFichaBasquet();
    public void ActualizarFichaBasquet(FichaBasquet fichaBasquet);
}