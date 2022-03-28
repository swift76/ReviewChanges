--USE master
--GO
--if exists (select * from sys.databases where name='BANK_CODE')
--	drop database BANK_CODE
--GO
--CREATE DATABASE BANK_CODE
--GO
--USE BANK_CODE
--GO



-- TABLES --



if exists (select * from sys.objects where name='SCRIPT' and type='U')
	drop table dbo.SCRIPT
GO

CREATE TABLE dbo.SCRIPT (
	PATH			nvarchar(255)	PRIMARY KEY CLUSTERED,
	BODY			varchar(max)	NOT NULL,
	SUBDIRECTORY	nvarchar(255)	NULL
)
GO



if exists (select * from sys.objects where name='CONSTANT_ARM' and type='U')
	drop table dbo.CONSTANT_ARM
GO

CREATE TABLE dbo.CONSTANT_ARM (
	CODE	varchar(100)	COLLATE Latin1_General_BIN	PRIMARY KEY CLUSTERED,
	VALUE	varchar(1000)	NOT NULL,
	LOCAL	bit				NOT NULL
)
GO



if exists (select * from sys.objects where name='CONSTANT_DEF' and type='U')
	drop table dbo.CONSTANT_DEF
GO

CREATE TABLE dbo.CONSTANT_DEF (
	CODE	varchar(100)	COLLATE Latin1_General_BIN	PRIMARY KEY CLUSTERED,
	VALUE	varchar(1000)	NOT NULL,
	LOCAL	bit				NOT NULL
)
GO



-- PROCEDURES --


create or alter procedure ahsp_Insert_SCRIPT(
	@PATH			nvarchar(300),
	@BODY 			varchar(max),
	@SUBDIRECTORY	nvarchar(255) = NULL
)
AS
	insert into SCRIPT (PATH,BODY,SUBDIRECTORY) values (@PATH,@BODY,@SUBDIRECTORY)
GO



create or alter procedure ahsp_Delete_SCRIPT(@PATH nvarchar(300))
AS
	delete from SCRIPT where PATH=@PATH
GO



create or alter procedure ahsp_Insert_CONSTANT_ARM(@CODE varchar(100),@VALUE varchar(1000),@LOCAL bit)
AS
	insert into CONSTANT_ARM (CODE,VALUE,LOCAL) values (@CODE,@VALUE,@LOCAL)
GO



create or alter procedure ahsp_Delete_CONSTANT_ARM
AS
	TRUNCATE TABLE CONSTANT_ARM
GO



create or alter procedure ahsp_Insert_CONSTANT_DEF(@CODE varchar(100),@VALUE varchar(1000),@LOCAL bit)
AS
	insert into CONSTANT_DEF (CODE,VALUE,LOCAL) values (@CODE,@VALUE,@LOCAL)
GO



create or alter procedure ahsp_Delete_CONSTANT_DEF
AS
	TRUNCATE TABLE CONSTANT_DEF
GO



create or alter procedure ahsp_GetScriptList
AS
	select PATH,BODY,SUBDIRECTORY from SCRIPT
GO



create or alter procedure ahsp_GetARMConstantList
AS
	select CODE,VALUE,LOCAL from CONSTANT_ARM
GO



create or alter procedure ahsp_GetDEFConstantList
AS
	select CODE,VALUE,LOCAL from CONSTANT_DEF
GO
