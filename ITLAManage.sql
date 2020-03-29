CREATE DATABASE ITLAManage
GO
USE ITLAManage
GO

CREATE TABLE Estudiantes
(
	IDEstudiante int PRIMARY KEY NOT NULL IDENTITY(0,1),
	Nombre varchar(30) NOT NULL,
	Apellido varchar(30) NOT NULL,
	FechaNacimiento date NOT NULL,
	Sexo char(1) NOT NULL,
	FechaRegistro datetime
)

CREATE TABLE Asignaturas
(
	IDAsignatura int PRIMARY KEY NOT NULL IDENTITY(0,1),
	Nombre varchar(30) NOT NULL,
	FechaRegistro datetime
)

CREATE TABLE Profesores
(
	IDProfesor int PRIMARY KEY NOT NULL IDENTITY(0,1),
	Nombre varchar(30) NOT NULL,
	Apellido varchar(30) NOT NULL,
	Sexo char(1) NOT NULL,
	FechaRegistro datetime
)
GO

CREATE TABLE DetalleProfesorAsignatura
(
	ID int PRIMARY KEY IDENTITY(0,1),
	IDAsignatura int FOREIGN KEY REFERENCES Asignaturas(IDAsignatura) NOT NULL,
	IDProfesor int FOREIGN KEY REFERENCES Profesores(IDProfesor) NOT NULL,
	FechaAsignacion date NOT NULL,
	Cuatrimestre varchar(30) --Ejemplo 2020-C1
)

CREATE TABLE EstudianteSeleccionAsignatura
(
	ID int PRIMARY KEY NOT NULL IDENTITY(0,1),
	IDAsignatura int FOREIGN KEY REFERENCES Asignaturas(IDAsignatura) NOT NULL,
	IDEstudiante int FOREIGN KEY REFERENCES Estudiantes(IDEstudiante) NOT NULL,
	FechaSeleccion date NOT NULL,
	Cuatrimestre varchar(30) NOT NULL --Ejemplo 2020-C1
)

--nombre asignatura
SELECT a.Nombre FROM DetalleProfesorAsignatura dpa
INNER JOIN Asignaturas a ON dpa.IDAsignatura = a.IDAsignatura
INNER JOIN Profesores p ON dpa.IDProfesor = p.IDProfesor
WHERE dpa.IDProfesor = 9

--datos asignacion materia a prof
SELECT dp.ID, dp.IDAsignatura, dp.IDProfesor, dp.FechaAsignacion FROM DetalleProfesorAsignatura dp
INNER JOIN DetalleProfesorAsignatura dpa ON dp.IDProfesor = dpa.IDProfesor
WHERE dp.IDAsignatura != dpa.IDAsignatura
GO

--stored procedure para borrar asignatura y sus referencias
CREATE PROCEDURE sp_borrarAsignatura(@IDAsignatura int)
AS BEGIN
	DELETE FROM EstudianteSeleccionAsignatura
	WHERE IDAsignatura = @IDAsignatura

	DELETE FROM DetalleProfesorAsignatura
	WHERE IDAsignatura = @IDAsignatura

	DELETE FROM Asignaturas
	WHERE IDAsignatura = @IDAsignatura
END
GO

--stored procedure para borrar profesor y sus referencias
CREATE PROCEDURE sp_borrarProfesor(@IDProf int)
AS BEGIN
	DELETE FROM DetalleProfesorAsignatura
	WHERE IDProfesor = @IDProf

	DELETE FROM Profesores
	WHERE IDProfesor = @IDProf
END
GO

--stored procedure para borrar estudiante y sus referencias
CREATE PROCEDURE sp_borrarEstudiante(@IDEst int)
AS BEGIN
	DELETE FROM EstudianteSeleccionAsignatura
	WHERE IDEstudiante = @IDEst

	DELETE FROM Estudiantes
	WHERE IDEstudiante = @IDEst
END
GO


--Extras
--ID Asignacion
SELECT dpa.ID FROM DetalleProfesorAsignatura dpa
WHERE dpa.IDProfesor = 9 AND dpa.IDAsignatura = 5

--Asignaturas por estudiantes
-------------------------------------------

SELECT a.Nombre, e.Nombre FROM Estudiantes e
INNER JOIN EstudianteSeleccionAsignatura esa ON e.IDEstudiante = esa.IDEstudiante
INNER JOIN Asignaturas a ON esa.IDAsignatura = a.IDAsignatura

INSERT INTO EstudianteSeleccionAsignatura
VALUES(5, 14, GETDATE(), '2020-C1')
