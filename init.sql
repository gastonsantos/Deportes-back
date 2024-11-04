-- Crear la base de datos y seleccionarla
CREATE DATABASE Deportes;
GO
USE Deportes;
GO

-- Tabla Usuario
CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(255) NOT NULL,
    Apellido NVARCHAR(255) NOT NULL, 
    Apodo NVARCHAR(255),
    Email VARCHAR(200),
    Contrasenia VARCHAR(200),
    Provincia NVARCHAR(255) NOT NULL,
    Localidad NVARCHAR(255) NOT NULL, 
    Direccion NVARCHAR(255) NOT NULL, 
    Numero NVARCHAR(100) NOT NULL, 
    VerifyEmail BIT, 
    Activo BIT,
    TokenConfirmacion VARCHAR(255),
    TokenCambioContrasenia VARCHAR(255)
);

-- Tabla Deporte
CREATE TABLE Deporte (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(255) NOT NULL,
    cantJugadores INT,
    imagen NVARCHAR(255)
);

-- Tabla Evento
CREATE TABLE Evento (
    IdEvento INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(200) NOT NULL,
    Direccion VARCHAR(200) NOT NULL,
    Provincia VARCHAR(200),
    Localidad VARCHAR(200),
    Numero VARCHAR(200),
    Hora VARCHAR(200),
    IdUsuarioCreador INT NOT NULL, 
    IdDeporte INT NOT NULL, 
    Fecha DATE, 
    Resultado VARCHAR(255),
    Finalizado AS (CASE WHEN Fecha IS NULL THEN NULL ELSE (CASE WHEN CONVERT(DATE, Fecha) < CONVERT(DATE, GETDATE()) THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) END),
    CONSTRAINT FK_Evento_IdUsuarioCreador FOREIGN KEY (IdUsuarioCreador) REFERENCES Usuario(Id),
    CONSTRAINT FK_Evento_IdDeporte FOREIGN KEY (IdDeporte) REFERENCES Deporte(Id)
);

-- Tabla Participante
CREATE TABLE Participante (
    IdParticipantes INT PRIMARY KEY IDENTITY, 
    IdEvento INT NOT NULL, 
    IdUsuarioCreadorEvento INT NOT NULL,
    IdUsuarioParticipante INT NOT NULL,
    Aceptado BIT,
    NotificacionVista BIT NULL,
    InvitaEsDuenio BIT NULL,
    CONSTRAINT FK_Participante_IdCreadorEvento FOREIGN KEY (IdUsuarioCreadorEvento) REFERENCES Usuario(Id),
    CONSTRAINT FK_Participante_IdParticipante FOREIGN KEY (IdUsuarioParticipante) REFERENCES Usuario(Id),
    CONSTRAINT FK_Participante_IdEvento FOREIGN KEY (IdEvento) REFERENCES Evento(IdEvento)
);

-- Tabla HistorialRefreshToken
CREATE TABLE HistorialRefreshToken (
    IdHistorialToken INT PRIMARY KEY IDENTITY,
    IdUsuario INT REFERENCES Usuario(Id),
    Token VARCHAR(500),
    RefreshToken VARCHAR(200),
    FechaCreacion DATETIME,
    FechaExpiracion DATETIME, 
    EsActivo AS (IIF(FechaExpiracion < GETDATE(), CONVERT(BIT, 0), CONVERT(BIT, 1)))
);

-- Tabla FichaDeportista
CREATE TABLE FichaDeportista (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    IdUsuario INT NOT NULL,
    Avatar VARCHAR(500),
    Edad VARCHAR(20),
    Altura VARCHAR(20),
    Peso VARCHAR(20),
    PieHabil VARCHAR(50), 
    ManoHabil VARCHAR(50),
    Posicion VARCHAR(50),
    CONSTRAINT FK_Ficha_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id)
);

-- Tabla FichaFutbol
CREATE TABLE FichaFutbol (
    Id INT PRIMARY KEY IDENTITY(1,1),       
    IdUsuario INT NOT NULL,                  
    Velocidad INT NULL,                      
    Resistencia INT NULL,                    
    Precision INT NULL,                      
    Fuerza INT NULL,                         
    Tecnica INT NULL,                        
    Agilidad INT NULL,                       
    Media INT NULL,                          
    CONSTRAINT FK_FichaFutbol_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id)
);

-- Tabla FichaTenis
CREATE TABLE FichaTenis (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL,
    Servicio INT,
    Drive INT,
    Reves INT, 
    Volea INT, 
    Fuerza INT,
    Velocidad INT,
    CONSTRAINT FK_FichaTenis_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id)
);

-- Tabla FichaBasquet
CREATE TABLE FichaBasquet (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL, 
    Finalizacion INT,
    Tiro INT,
    Organizacion INT, 
    Defensa INT, 
    Fuerza INT,
    Velocidad INT,
    CONSTRAINT FK_FichaBasquet_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id)
);

-- Tabla Resultado
CREATE TABLE Resultado (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdEvento INT NULL,
    ResultadoLocal INT NULL,
    ResultadoVisitante INT NULL,
    CONSTRAINT FK_Resultado_IdEvento FOREIGN KEY (IdEvento) REFERENCES Evento(IdEvento)
);

-- Insertar datos en la tabla Deporte
INSERT INTO Deporte (Nombre, cantJugadores, imagen) VALUES 
('Futbol 5', 10, '/images/futbol-5.jpg'),
('Futbol 11', 22, '/images/futbol-11.jpg'),
('Tenis Single', 2, '/images/tennis-single.jpg'),
('Tenis Dobles', 4, '/images/tennis-doble.jpg'),
('Paddle Single', 2, '/images/padle-single.jpg'),
('Paddle Dobles', 4, '/images/padle-doble.jpg'),
('Basquet', 10, '/images/basquet-5v5.jpg'),
('Basquet 2v2', 4, '/images/basquet-2v2.jpg');

INSERT INTO Usuario (
    Nombre,
    Apellido,
    Apodo,
    Email,
    Contrasenia,
    Provincia,
    Localidad,
    Direccion,
    Numero,
    VerifyEmail,
    Activo,
    TokenConfirmacion,
    TokenCambioContrasenia
) VALUES (
    'Juan',
    'Pérez',
    'Juano',
    'juan.perez@example.com',
    'ContraseniaSegura123', -- Asegúrate de utilizar un hash en producción
    'Buenos Aires',
    'CABA',
    'Av. Corrientes',
    '1234',
    1, -- Email verificado (1 = true)
    1, -- Activo (1 = true)
    NULL, -- Sin token de confirmación por ahora
    NULL  -- Sin token de cambio de contraseña por ahora
);  
