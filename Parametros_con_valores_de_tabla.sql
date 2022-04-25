/*	1- Fuente:https://learntutorials.net/es/sql-server/topic/5285/parametros-de-tabla-de-valores#:~:text=Los%20parámetros%20con%20valores%20de,parámetro%20que%20se%20está%20utilizando.
	2- Fuente: https://www.c-sharpcorner.com/article/user-defined-table-types-and-table-valued-parameters/
		Ejemplos de procedimientos almacenado con Insert, Update y Delete con paramatros con valores de tabla.
*/

	/*OBJETIVO DE LOS PARAMETROS CON VALORES DE TABLA: 
		Enviar a un procedimiento almacenado una tabla con N cantidad de FILAS y COLUMNAS, teniendo en cuenta que 
		el procedimiento almacenado recibirá un solo parametro con muchos valores y no con un solo valor.
	*/

/*
Los parámetros con valores de tabla (TVP para abreviar) son parámetros que se pasan a un procedimiento almacenado 
o una función que contiene datos estructurados en tablas. 
El uso de parámetros con valores de tabla requiere la creación de un tipo de tabla definido por el usuario 
para el parámetro que se está utilizando.

Los parámetros de valor de tabla son parámetros de solo lectura.
*/

	--Ejemplo 1:

--Crear tabla
CREATE TABLE tblNames (
	FirstName	NVARCHAR(100),
	LastName	NVARCHAR(100)
)

--Defina un tipo de tabla definida utilizada para usar:
CREATE TYPE tptblNames AS TABLE (
    FirstName	NVARCHAR(100),
    LastName	NVARCHAR(100)
)
GO

--Crear el procedimiento almacenado:
CREATE PROCEDURE prInsertNames (
    @Names tptblNames READONLY -- Nota: Debe especificar READONLY
)
AS
	INSERT INTO dbo.TblNames (FirstName, LastName)
		SELECT FirstName, LastName
		FROM @Names;
GO

--Ejecutando el procedimiento almacenado:
DECLARE @names tptblNames -- Se declara la variable con el tipo de tabla creado previamente
INSERT INTO @Names VALUES ('Akio', 'Kawa')
	,('Daiki', 'Kuchi')
	,('Hiroki', 'Matsu')
	,('Tadashi', 'Shita')
	,('Tsubasa', 'Kita')

EXEC dbo.prInsertNames @names

select * from TblNames

/* 

*/