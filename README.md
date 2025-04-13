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

* **Entity Framework**: Para la gestión de la base de datos.
  Paquete: `Microsoft.EntityFramework`
* **ASP.NET MVC**: Para la construcción de la aplicación web y el patrón MVC.
  Paquete: `Microsoft.AspNet.Mvc`

### Principios SOLID

* **S - Principio de Responsabilidad Única (Single Responsibility Principle)**: Cada clase tiene una única responsabilidad.
* **O - Principio de Abierto/Cerrado (Open/Closed Principle)**: El sistema está diseñado para permitir la extensión sin modificar el código existente.
* **L - Principio de Sustitución de Liskov (Liskov Substitution Principle)**: Se asegura que las clases derivadas puedan sustituir a sus clases base sin alterar el comportamiento correcto del sistema.
* **I - Principio de Segregación de Interfaces (Interface Segregation Principle)**: Las interfaces son pequeñas y específicas para que las clases no se vean forzadas a implementar métodos que no necesitan.
* **D - Principio de Inversión de Dependencias (Dependency Inversion Principle)**: El sistema utiliza inyección de dependencias (DI) para desacoplar las clases entre sí y facilitar el testing y mantenimiento.

### Patrones de Diseño

* **Patrón de Diseño MVC (Modelo-Vista-Controlador)**: Organiza el código en tres componentes separados que permiten una clara separación de responsabilidades.
* **Patrón de Diseño Repositorio**: Se utiliza para abstraer la capa de acceso a datos y separar la lógica de negocio de la base de datos.

## Repositorio GitHub

* https://github.com/Allan1418/SistemaQueueTareas

