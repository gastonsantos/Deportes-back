﻿using Deportes.Modelo.FichaDeportistaModel;
using Deportes.Modelo.FichaFutbolModel;
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
}
