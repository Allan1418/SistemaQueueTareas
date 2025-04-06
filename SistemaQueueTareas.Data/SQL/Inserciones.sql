--Usuario 1--
INSERT INTO [AspNetUsers] ([Id]) VALUES ('f44da914-ecf1-4b56-8727-eb0bccae1021');
INSERT INTO [AspNetUsers] ([Email]) VALUES ('leomessi@gmail.com');
INSERT INTO [AspNetUsers] ([EmailConfirmed]) VALUES (0);
INSERT INTO [AspNetUsers] ([PasswordHash]) VALUES ('AN/j+4gWdupQyQ3RJG/ZQd4B2GJ6msatFovaCN+vsnqx2Fe8fSUNFQWqFnKa+oAGUg==');
INSERT INTO [AspNetUsers] ([SecurityStamp]) VALUES ('bc777642-83eb-4671-904f-4c061970094e');
INSERT INTO [AspNetUsers] ([PhoneNumber]) VALUES ('87654321');
INSERT INTO [AspNetUsers] ([PhoneNumberConfirmed]) VALUES (0);
INSERT INTO [AspNetUsers] ([TwoFactorEnabled]) VALUES (0);
INSERT INTO [AspNetUsers] ([LockoutEndDateUtc]) VALUES (NULL);
INSERT INTO [AspNetUsers] ([LockoutEnabled]) VALUES (0);
INSERT INTO [AspNetUsers] ([AccessFailedCount]) VALUES (0);
INSERT INTO [AspNetUsers] ([UserName]) VALUES ('leo.messi');

--Usuario 2--
INSERT INTO [AspNetUsers] ([Id]) VALUES ('516eb6b8-18cd-4e4d-ba3c-ab553adf0862');
INSERT INTO [AspNetUsers] ([Email]) VALUES ('monkeydluffy@gmail.com');
INSERT INTO [AspNetUsers] ([EmailConfirmed]) VALUES (0);
INSERT INTO [AspNetUsers] ([PasswordHash]) VALUES ('AHUYeF2V3utE+z3cl2Zhqgilth7Fe5LCzI0KzO17qTVfkKUx6hUUwApkYVdzniVoiQ==');
INSERT INTO [AspNetUsers] ([SecurityStamp]) VALUES ('28ffe3e0-008b-4de6-8895-a6127835e3a5');
INSERT INTO [AspNetUsers] ([PhoneNumber]) VALUES ('89012345');
INSERT INTO [AspNetUsers] ([PhoneNumberConfirmed]) VALUES (0);
INSERT INTO [AspNetUsers] ([TwoFactorEnabled]) VALUES (0);
INSERT INTO [AspNetUsers] ([LockoutEndDateUtc]) VALUES (NULL);
INSERT INTO [AspNetUsers] ([LockoutEnabled]) VALUES (0);
INSERT INTO [AspNetUsers] ([AccessFailedCount]) VALUES (0);
INSERT INTO [AspNetUsers] ([UserName]) VALUES ('monkey.luffy');

--Usuario 3--
INSERT INTO [AspNetUsers] ([Id]) VALUES ('7523a730-ee3a-495e-9eb0-fbef864512cf');
INSERT INTO [AspNetUsers] ([Email]) VALUES ('tonysoprano@gmail.com');
INSERT INTO [AspNetUsers] ([EmailConfirmed]) VALUES (0);
INSERT INTO [AspNetUsers] ([PasswordHash]) VALUES ('ABNr6rGlmaXGglGDNucKcmfn7qTUSmz07lZnNL0g+/dRgj4OAtb65qi1XF6r/+Q9ZQ==');
INSERT INTO [AspNetUsers] ([SecurityStamp]) VALUES ('5f06fe07-0312-4e26-8ebc-d25ae1278dc4');
INSERT INTO [AspNetUsers] ([PhoneNumber]) VALUES ('87960542');
INSERT INTO [AspNetUsers] ([PhoneNumberConfirmed]) VALUES (0);
INSERT INTO [AspNetUsers] ([TwoFactorEnabled]) VALUES (0);
INSERT INTO [AspNetUsers] ([LockoutEndDateUtc]) VALUES (NULL);
INSERT INTO [AspNetUsers] ([LockoutEnabled]) VALUES (0);
INSERT INTO [AspNetUsers] ([AccessFailedCount]) VALUES (0);
INSERT INTO [AspNetUsers] ([UserName]) VALUES ('tony.soprano');

--Tarea 1--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('f44da914-ecf1-4b56-8727-eb0bccae1021', 'Revisar informe', 'Revisar el informe de ventas de este mes y agregar los datos finales.', 1, 1);

--Tarea 2--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('f44da914-ecf1-4b56-8727-eb0bccae1021', 'Reunión de equipo', 'Reunión semanal para revisar el progreso de los proyectos.', 2, 1);

--Tarea 3--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('f44da914-ecf1-4b56-8727-eb0bccae1021', 'Actualizar página web', 'Actualizar la página de inicio con los nuevos productos.', 3, 1);

--Tarea 4--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('f44da914-ecf1-4b56-8727-eb0bccae1021', 'Estudio de mercado', 'Investigar la competencia y elaborar un informe con conclusiones.', 1, 1);

--Tarea 5--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('f44da914-ecf1-4b56-8727-eb0bccae1021', 'Implementar cambios', 'Implementar los cambios sugeridos en el código del proyecto.', 2, 1);

--Tarea 6--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('516eb6b8-18cd-4e4d-ba3c-ab553adf0862', 'Revisar presupuesto', 'Revisar los presupuestos y determinar ajustes necesarios.', 1, 1);

--Tarea 7--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('516eb6b8-18cd-4e4d-ba3c-ab553adf0862', 'Preparar presentación', 'Preparar la presentación de resultados para la reunión mensual.', 3, 1);

--Tarea 8--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('516eb6b8-18cd-4e4d-ba3c-ab553adf0862', 'Enviar reportes', 'Enviar los reportes de desempeño a los directores.', 2, 1);

--Tarea 9--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('516eb6b8-18cd-4e4d-ba3c-ab553adf0862', 'Revisión de contrato', 'Revisar el contrato con el proveedor y hacer correcciones.', 1, 1);

--Tarea 10--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('516eb6b8-18cd-4e4d-ba3c-ab553adf0862', 'Tarea de soporte', 'Brindar soporte técnico a los clientes que tengan problemas con la plataforma.', 2, 1);

--Tarea 11--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('7523a730-ee3a-495e-9eb0-fbef864512cf', 'Desarrollo de funciones', 'Desarrollar nuevas funciones para la aplicación móvil.', 3, 1);

--Tarea 12--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('7523a730-ee3a-495e-9eb0-fbef864512cf', 'Revisión de código', 'Revisar y limpiar el código del sistema antes de la entrega.', 1, 1);

--Tarea 13--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('7523a730-ee3a-495e-9eb0-fbef864512cf', 'Revisar tickets', 'Revisar los tickets de soporte y priorizar las respuestas.', 2, 1);

--Tarea 14--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('7523a730-ee3a-495e-9eb0-fbef864512cf', 'Análisis de datos', 'Analizar los datos de las encuestas y preparar un informe.', 3, 1);

--Tarea 15--
INSERT INTO [Tasks] ([id_user], [name], [description], [id_priority], [id_state]) 
VALUES ('7523a730-ee3a-495e-9eb0-fbef864512cf', 'Planificación de campaña', 'Planificar la próxima campaña publicitaria para el próximo trimestre.', 1, 1);


