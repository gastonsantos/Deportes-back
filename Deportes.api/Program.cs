using Deportes.api.Config;
using Deportes.Infra;
using Deportes.Infra.Database.CalificacionRepository;
using Deportes.Infra.Database.DeporteRepository;
using Deportes.Infra.Database.EventoRepository;
using Deportes.Infra.Database.FichaRepository;
using Deportes.Infra.Database.ParticipantesRepository;
using Deportes.Infra.Database.ResultadoRepository;
using Deportes.Infra.Database.UsuarioRepository;
using Deportes.Servicio.Interfaces.ICalificacion;
using Deportes.Servicio.Interfaces.IChathub;
using Deportes.Servicio.Interfaces.ICorreo;
using Deportes.Servicio.Interfaces.IDeporte;
using Deportes.Servicio.Interfaces.IEvento;
using Deportes.Servicio.Interfaces.IFichas;
using Deportes.Servicio.Interfaces.IParticipantes;
using Deportes.Servicio.Interfaces.IResultado;
using Deportes.Servicio.Interfaces.IToken;
using Deportes.Servicio.Interfaces.IUsuario;
using Deportes.Servicio.Servicios.CalificacionServices;
using Deportes.Servicio.Servicios.ChatServices;
using Deportes.Servicio.Servicios.CorreoServices;
using Deportes.Servicio.Servicios.DeporteServices;
using Deportes.Servicio.Servicios.EventoServices;
using Deportes.Servicio.Servicios.FichaServices;
using Deportes.Servicio.Servicios.MiClaseSignalR;
using Deportes.Servicio.Servicios.ParticipantesServices;
using Deportes.Servicio.Servicios.TokenServices;
using Deportes.Servicio.Servicios.UsuarioServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //Titulo del dise�o

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Software Lion", Version = "v1" });
    //Boton Authorize
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header, 
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }

    });
});

//Son las configuraciones de Jwt, de donde las saca(appseting.json) y demas
/*
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer= true,
        ValidateAudience= true,
        ValidateLifetime= true,
        ValidateIssuerSigningKey= true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"], //de donde toma el Issuer quees de appSteging
        ValidAudience = builder.Configuration["Jwy:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };

});
*/
//creo el Context
builder.Services.AddDbContext<DeportesContext>(options =>
   //options.UseSqlServer("Data Source=DESKTOP-TS9IBN4;Initial Catalog=Deportes;Integrated Security=True; TrustServerCertificate=True;"));
   // options.UseSqlServer("Server=localhost,1433;Database=Deportes;User Id=sa;Password=Ergittek2023;TrustServerCertificate=True;"));
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<DeportesContext>();

builder.Services.AddHttpContextAccessor();

//Agrego las Dependencias
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAutorizacionService, AutorizacionService>();
builder.Services.AddScoped<IDeporteService, DeporteService>();
builder.Services.AddScoped<IDeporteRepository, DeporteRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IEventoServices, EventoServices>();
builder.Services.AddScoped<IParticipantesRepository, ParticipantesRepository>();
builder.Services.AddScoped<IParticipantesServices, ParticipantesServices>();
builder.Services.AddScoped<IFichaRepository, FichaRepository>();
builder.Services.AddScoped<IFichaServices, FichaServices>();
builder.Services.AddScoped<ICorreoServices, CorreoServices>();
builder.Services.AddScoped<IChatHub, ChatHub>();
builder.Services.AddScoped<ICalificacionRepository, CalificacionRepository>();
builder.Services.AddScoped<ICalificacionServices, CalificacionServices>();
builder.Services.AddScoped<IResultadoRepository, ResultadoRepository>();
//Configuraciones JWT
var key = builder.Configuration.GetValue<string>("Jwt:key");
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata= false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey= true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero

    };
});

builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://deportes-front.vercel.app", "http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseRouting();
//Cors
app.UseCors();

app.UseHttpsRedirection();

// Esto tambien se agrega para JWT
app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MiClaseSignalR>("/chatHub");
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
