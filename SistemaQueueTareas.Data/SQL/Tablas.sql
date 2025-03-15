------------------------------
USE master
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'SistemaQueueTareas')
BEGIN
    ALTER DATABASE [SistemaQueueTareas] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
    DROP DATABASE [SistemaQueueTareas]
END
GO

CREATE DATABASE [SistemaQueueTareas]
GO

USE [SistemaQueueTareas]
GO
------------------------------

-- Tabla de Usuarios
CREATE TABLE Users (
    id_usuario INT PRIMARY KEY,
    Nombre VARCHAR(50),
    Apellido VARCHAR(50),
    Email VARCHAR(100),
    Telefono VARCHAR(20)
);

-- Tabla de Estados
CREATE TABLE Estados (
    id_estado INT AUTO_INCREMENT PRIMARY KEY,
    descripcion VARCHAR(50) UNIQUE NOT NULL
);

-- Tabla de Prioridades
CREATE TABLE Prioridades (
    id_prioridad INT AUTO_INCREMENT PRIMARY KEY,
    descripcion VARCHAR(50) UNIQUE NOT NULL
);

-- Tabla de TAREAS
CREATE TABLE Tareas (
    id_tarea INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    id_prioridad INT NOT NULL,
    id_estado INT NOT NULL,
    fecha_creacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    fecha_ejecucion DATETIME NULL,
    fecha_finalizacion DATETIME NULL,
    FOREIGN KEY (id_usuario) REFERENCES Users(id_usuario),
    FOREIGN KEY (id_prioridad) REFERENCES Prioridades(id_prioridad),
    FOREIGN KEY (id_estado) REFERENCES Estados(id_estado)
);

-- Tabla de NOTIFICACIONES
CREATE TABLE Notificaciones (
    id_notificacion INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    mensaje TEXT NOT NULL,
    fecha_envio DATETIME DEFAULT CURRENT_TIMESTAMP,
    leido BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (id_usuario) REFERENCES Users(id_usuario)
);

-- Tabla de HISTORIAL DE EJECUCIÓN
CREATE TABLE Historial_Ejecucion (
    id_historial INT AUTO_INCREMENT PRIMARY KEY,
    id_tarea INT NOT NULL,
    fecha_inicio DATETIME NOT NULL,
    fecha_fin DATETIME NULL,
    id_resultado INT NOT NULL,
    detalle_log TEXT,
    FOREIGN KEY (id_tarea) REFERENCES Tareas(id_tarea)
);

-- Tabla de RESULTADOS
CREATE TABLE RESULTADOS (
    id_resultado INT AUTO_INCREMENT PRIMARY KEY,
    descripcion VARCHAR(50) UNIQUE NOT NULL
);



