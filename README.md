# Proyecto de curso de SC-601 Programacion Avanzada, 1 Cuatrimestre 2025: SistemaQueueTareas

"SistemaQueueTareas" es un sistema de gestion de colas de tareas disenado para ayudar a los usuarios a gestionar 
y realizar un seguimiento de las tareas de manera organizada. 
Este sistema proporciona una forma simple y eficiente de crear y administrar tareas, 
con diferentes roles de usuario y estados de tarea.

Integrantes:

* Barrantes Prendas Elder Josue
* Nunez Brenes Allan
* Nunez Hernandez Johnny Andres
* Picado Martinez Ana Gabriela
* Vargas Merlo Anyelo Dario

## Funcionalidades Principales

* **Gestion de Usuarios:** Crear y gestionar usuarios con diferentes roles.
* **Gestion de Colas de Tareas:** Agregar, actualizar y realizar un seguimiento de tarea con varios estados.
* **Notificaciones:** Recibir alertas sobre actualizaciones de tareas.
* **Integracion con Base de Datos:** Almacenar los datos de manera segura con una base de datos backend.

## Tecnologias Utilizadas

* **C#**
* **ASP.NET**
* **SQL Server**
* **Javascript**
* **HTML/CSS**

## Especificación Básica del Proyecto

### Arquitectura del Proyecto

El proyecto sigue una arquitectura basada en el patrón **MVC (Modelo-Vista-Controlador)**.
* **Capa de Presentación (Vista)**: Esta capa incluye la interfaz de usuario desarrollada con **HTML**, **CSS**, y **JavaScript**.
* **Capa de Lógica de Negocio (Controlador)**: La lógica de negocio y el procesamiento de las tareas se gestionan en los controladores, que están implementados en **C#** con **ASP.NET**.
* **Capa de Acceso a Datos (Modelo)**: La capa de datos se encarga de interactuar con la base de datos usando **Entity Framework**.

### Libraries o Paquetes de NuGet

**Entity Framework**: 
- `EntityFramework` - ORM para la gestión de base de datos.
- `Microsoft.AspNet.Identity.EntityFramework` - Para la gestión de identidades de usuarios.

**ASP.NET**:
- `Microsoft.AspNet.Identity.Core` - Librería base para la gestión de identidad en ASP.NET.
- `Microsoft.AspNet.Identity.Owin` - Para integrar OWIN con el sistema de identidad de ASP.NET.
- `Microsoft.AspNet.Mvc` - Framework MVC para el desarrollo web.
- `Microsoft.AspNet.Web.Optimization` - Optimización de archivos CSS y JavaScript en la web.
- `Microsoft.AspNet.WebPages` - Para generar contenido dinámico en las vistas.
- `Microsoft.Extensions.Configuration` - Librerías para la configuración de la aplicación.
- `Microsoft.Extensions.DependencyInjection` - Para la inyección de dependencias.
- `Microsoft.Extensions.Logging` - Para agregar capacidades de logging a la aplicación.

**JavaScript y UI**:
- `bootstrap` - Framework de diseño web para crear interfaces atractivas y responsivas.
- `jquery` - Librería JavaScript para simplificar las interacciones con el DOM.
- `jquery.validation` - Plugin para validación de formularios con jQuery.
- `microsoft.jquery.unobtrusive.validation` - Validación de formularios sin interferir con el comportamiento de la página.
- `modernizr` - Herramienta para detectar características HTML5 y CSS3 en los navegadores.
  
**Otras librerías importantes**:
- `Newtonsoft.Json` - Para la manipulación y serialización de JSON.
- `owin` - Interfaces para trabajar con la especificación OWIN en ASP.NET.
- `System.Memory` - Librería para trabajar con memoria en .NET.
- `Unity` - Contenedor de inversión de control (DI) para resolver dependencias.

### Patrones de Diseño

* **Patrón de Diseño MVC (Modelo-Vista-Controlador)**: Organiza el código en tres componentes separados que permiten una clara separación de responsabilidades.
* **Patrón de Diseño Repositorio**: Se utiliza para abstraer la capa de acceso a datos y separar la lógica de negocio de la base de datos.

## Archivo copyConfigFile.bat

El archivo **`copyConfigFile.bat`** es un archivo de script **Batch** utilizado para facilitar la configuración del proyecto. 
Este archivo se encarga de copiar el archivo de configuración de conexión a la base de datos (`connectionString.config`) desde su ubicación de origen hasta la carpeta correcta dentro del proyecto.

### ¿Cómo usarlo?
1. Ejecuta el archivo `copyConfigFile.bat` haciendo doble clic sobre él.
2. El script copiará automáticamente el archivo de configuración a la ubicación correcta, permitiendo que el sistema se conecte a la base de datos sin problemas.

## Repositorio GitHub

* https://github.com/Allan1418/SistemaQueueTareas

