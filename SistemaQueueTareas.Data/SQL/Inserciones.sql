
USE [SistemaQueueTareas]
GO

INSERT INTO [AspNetUsers] 
([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName])
VALUES
-- Usuario 1
('f44da914-ecf1-4b56-8727-eb0bccae1021', 'leomessi@gmail.com', 0, 'AN/j+4gWdupQyQ3RJG/ZQd4B2GJ6msatFovaCN+vsnqx2Fe8fSUNFQWqFnKa+oAGUg==', 'bc777642-83eb-4671-904f-4c061970094e', '87654321', 0, 0, NULL, 0, 0, 'leo.messi'),
-- Usuario 2
('516eb6b8-18cd-4e4d-ba3c-ab553adf0862', 'monkeydluffy@gmail.com', 0, 'AHUYeF2V3utE+z3cl2Zhqgilth7Fe5LCzI0KzO17qTVfkKUx6hUUwApkYVdzniVoiQ==', '28ffe3e0-008b-4de6-8895-a6127835e3a5', '89012345', 0, 0, NULL, 0, 0, 'monkey.luffy'),
-- Usuario 3
('7523a730-ee3a-495e-9eb0-fbef864512cf', 'tonysoprano@gmail.com', 0, 'ABNr6rGlmaXGglGDNucKcmfn7qTUSmz07lZnNL0g+/dRgj4OAtb65qi1XF6r/+Q9ZQ==', '5f06fe07-0312-4e26-8ebc-d25ae1278dc4', '87960542', 0, 0, NULL, 0, 0, 'tony.soprano');
GO

INSERT INTO [Tasks] 
([id_user], [name], [description], [id_priority], [id_state])
VALUES
-- Tarea 1
('f44da914-ecf1-4b56-8727-eb0bccae1021', 'Revisar informe', 'Revisar el informe de ventas de este mes y agregar los datos finales.', 1, 1),
-- Tarea 2
('f44da914-ecf1-4b56-8727-eb0bccae1021', 'Reunión de equipo', 'Reunión semanal para revisar el progreso de los proyectos.', 2, 1),
-- Tarea 3
('f44da914-ecf1-4b56-8727-eb0bccae1021', 'Actualizar página web', 'Actualizar la página de inicio con los nuevos productos.', 3, 1),
-- Tarea 4
('f44da914-ecf1-4b56-8727-eb0bccae1021', 'Estudio de mercado', 'Investigar la competencia y elaborar un informe con conclusiones.', 1, 1),
-- Tarea 5
('f44da914-ecf1-4b56-8727-eb0bccae1021', 'Implementar cambios', 'Implementar los cambios sugeridos en el código del proyecto.', 2, 1),
-- Tarea 6
('516eb6b8-18cd-4e4d-ba3c-ab553adf0862', 'Revisar presupuesto', 'Revisar los presupuestos y determinar ajustes necesarios.', 1, 1),
-- Tarea 7
('516eb6b8-18cd-4e4d-ba3c-ab553adf0862', 'Preparar presentación', 'Preparar la presentación de resultados para la reunión mensual.', 3, 1),
-- Tarea 8
('516eb6b8-18cd-4e4d-ba3c-ab553adf0862', 'Enviar reportes', 'Enviar los reportes de desempeño a los directores.', 2, 1),
-- Tarea 9
('516eb6b8-18cd-4e4d-ba3c-ab553adf0862', 'Revisión de contrato', 'Revisar el contrato con el proveedor y hacer correcciones.', 1, 1),
-- Tarea 10
('516eb6b8-18cd-4e4d-ba3c-ab553adf0862', 'Tarea de soporte', 'Brindar soporte técnico a los clientes que tengan problemas con la plataforma.', 2, 1),
-- Tarea 11
('7523a730-ee3a-495e-9eb0-fbef864512cf', 'Desarrollo de funciones', 'Desarrollar nuevas funciones para la aplicación móvil.', 3, 1),
-- Tarea 12
('7523a730-ee3a-495e-9eb0-fbef864512cf', 'Revisión de código', 'Revisar y limpiar el código del sistema antes de la entrega.', 1, 1),
-- Tarea 13
('7523a730-ee3a-495e-9eb0-fbef864512cf', 'Revisar tickets', 'Revisar los tickets de soporte y priorizar las respuestas.', 2, 1),
-- Tarea 14
('7523a730-ee3a-495e-9eb0-fbef864512cf', 'Análisis de datos', 'Analizar los datos de las encuestas y preparar un informe.', 3, 1),
-- Tarea 15
('7523a730-ee3a-495e-9eb0-fbef864512cf', 'Planificación de campaña', 'Planificar la próxima campaña publicitaria para el próximo trimestre.', 1, 1);
GO


