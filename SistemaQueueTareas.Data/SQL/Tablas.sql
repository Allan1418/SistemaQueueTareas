

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

CREATE TABLE Users (
    id INT PRIMARY KEY,
    Nombre VARCHAR(50),
    Apellido VARCHAR(50),
    Email VARCHAR(100),
    Telefono VARCHAR(20)
);