-- =============================================
-- Script Template
-- =============================================
USE master
DECLARE @database_name nvarchar(100)
SET @database_name = 'Employee_Records'
IF db_id(@database_name) IS NOT NULL
	BEGIN
		Print 'Database exists..no need to re-create!'
	END
ELSE
	BEGIN
		Print 'Database does not exists..creating the database..'
		CREATE DATABASE Employee_Records
	END
--USE a0010


