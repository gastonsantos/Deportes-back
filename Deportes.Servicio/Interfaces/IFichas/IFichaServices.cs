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

namespace Deportes.Servicio.Interfaces.IFichas
{
    public interface IFichaServices
    {
        public DtoFichaDeportista DevolverFichaDeportistaPorId(int id);
        public void AgregarFichaDeportista(DtoFichaDeportista fichaDeportista);
        public FichaFutbol DevolverFichaFutbol(int id);
        public void AgregarFichaFutbol(DtoFutbolServices fichaFutbol);
        public void ActualizarFichaDeportista(DtoFichaDeportista fichaDeportista);
        public void ActualizarFichaFutbol(FichaFutbol fichaFutbol);
        public void EliminarFichaFutbol(int id);

        public List<FichaFutbol> DevolverFichasDeFutbol();
        public void AgregarFichaTenis(FichaTeni dtoTenis);
        public void EliminarFichaTenis(int id);
        public FichaTeni DevolverFichaTenis(int id);
        public List<FichaTeni> DevolverFichasDeTenis();
        public void ActaulizarFichaTenis(int id, DtoFichaTenis dtoFichaTenisController);
        public void ActaulizarFichaFutbol(DtoFutbolServices dtoFutbolServices);
        public void AgregarFichaBasquet(FichaBasquet fichaBasquet);
        public void EliminarFichaBasquet(int id);
        public FichaBasquet ObtenerFichaBasquet(int id);
        public List<FichaBasquet> BuscarFichaBasquet();
        public  void ActualizarFichaBasquet(int id, DtoFichaBasquet dtoFichaBasquet);
    }
}
