using System;
using System.Collections.Generic;
using Deportes.Modelo.DeporteModel;
using Deportes.Modelo.UsuarioModel;
using Microsoft.EntityFrameworkCore;


namespace Deportes.Infra;

public class DeportesContext : DbContext
{
    public DbSet<Usuario> Usuario { get; set; }
    
    public DbSet<Deporte> Deporte { get; set; }
    public DeportesContext(DbContextOptions<DeportesContext> options)
   : base(options)
    {
     
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    //=> optionsBuilder.UseSqlServer("Data Source=DESKTOP-TS9IBN4;Initial Catalog=Deportes;Integrated Security=True; TrustServerCertificate=True;");

    // => optionsBuilder.UseSqlServer(this._configuration.GetConnectionString("DefaultConnection"));


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Usuario");
        });

        modelBuilder.Entity<Deporte>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Deporte");
        });

    }
}