/*
CREATE DATABASE employee_schedule
GO
*/
-----------Grafiki pracownicze i ewidencja czasu pracy----------
-----------created by:Grzegorz Piszczek-------------------------
USE [employee_schedule]
GO
BEGIN TRANSACTION
BEGIN TRY

CREATE TABLE [dbo].[employee_schedule_type]
(
[ID_employee_schedule_type] INT NOT NULL
,[name] VARCHAR(30) NOT NULL
,[avilable] BIT NOT NULL DEFAULT 1
,CONSTRAINT PK_employee_schedule_type PRIMARY KEY ([ID_employee_schedule_type])
);

INSERT INTO [dbo].[employee_schedule_type] ([ID_employee_schedule_type],[name]) VALUES (1,'Planowany');
INSERT INTO [dbo].[employee_schedule_type] ([ID_employee_schedule_type],[name]) VALUES (2,'Wykonania');

CREATE TABLE [dbo].[employee_absenteeism]
(
[ID_employee_absenteeism] INT NOT NULL IDENTITY(1,1)
,[short_name] varchar(2) NOT NULL
,[name] VARCHAR(30) NOT NULL
,[avilable] BIT NOT NULL DEFAULT 1
);

INSERT INTO [dbo].[employee_absenteeism] ([short_name],[name]) VALUES ('R','Dzienna zmiana');
INSERT INTO [dbo].[employee_absenteeism] ([short_name],[name]) VALUES ('N','Nocna zmiana');
INSERT INTO [dbo].[employee_absenteeism] ([short_name],[name]) VALUES ('L4','Chorobowe');
INSERT INTO [dbo].[employee_absenteeism] ([short_name],[name]) VALUES ('UW','Urlop wypoczynkowy');
INSERT INTO [dbo].[employee_absenteeism] ([short_name],[name]) VALUES ('UZ','Urlop na ¿¹danie');

CREATE TABLE [dbo].[employee]
(
    [ID_employee] INT NOT NULL IDENTITY(1,1),   
    [employee_name] VARCHAR(30) NOT NULL,       
	[monthly_work_hours] DATETIME NOT NULL,              
    [available] BIT NOT NULL DEFAULT 1,         
);
ALTER TABLE [dbo].[employee]
	ADD CONSTRAINT PK_employee
	PRIMARY KEY ([ID_employee]);

CREATE TABLE [dbo].[employee_schedule]
(
    [ID_employee_schedule] INT NOT NULL IDENTITY(1,1),
    [year] INT NOT NULL,
    [month] INT NOT NULL,
	[employee_schedule_type_ID] INT NOT NULL,
    [name_of_employee_group] VARCHAR(30) NOT NULL,
    [avilable] BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_employee_schedule PRIMARY KEY ([ID_employee_schedule]),
    CONSTRAINT UQ_employee_schedule UNIQUE ([year], [month], [avilable])
);
ALTER TABLE [dbo].[employee_schedule]
	ADD CONSTRAINT FK_employee_schedule_employee_schedule_type
	FOREIGN KEY ([employee_schedule_type_ID]) REFERENCES [dbo].[employee_schedule_type]([ID_employee_schedule_type]);

CREATE TABLE [dbo].[employee_schedule_position]
(
[ID_employee_schedule_position] INT NOT NULL IDENTITY(1,1)
,[employee_schedule_ID] INT NOT NULL	--FK
,[employee_ID] INT NOT NULL				--FK
,[for_date] DATETIME NOT NULL 
,[employee_absenteeism_ID] INT NOT NULL
,[hours] DATETIME NOT NULL
);

ALTER TABLE [dbo].[employee_schedule_position]
	ADD CONSTRAINT FK_employee_schedule_employee_schedule_position
	FOREIGN KEY ([employee_schedule_ID]) REFERENCES [dbo].[employee_schedule]([ID_employee_schedule]) ON DELETE CASCADE;

ALTER TABLE [dbo].[employee_schedule_position]
	ADD CONSTRAINT FK_employee_employee_schedule_position
	FOREIGN KEY ([employee_ID]) REFERENCES [dbo].[employee]([ID_employee]) ON DELETE CASCADE;

COMMIT
END TRY
BEGIN CATCH
	ROLLBACK
	SELECT 'ERROR OCCURRED'
END CATCH

-----------------------------------------------
