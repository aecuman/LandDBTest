
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/24/2016 18:13:05
-- Generated from EDMX file: c:\users\mokure\documents\visual studio 2015\Projects\LandDBTest\LandDBTest\Models\landdb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [landdb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Values]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Values];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Values'
CREATE TABLE [dbo].[Values] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [tenure] nvarchar(max)  NOT NULL,
    [block] float  NOT NULL,
    [district] nvarchar(max)  NOT NULL,
    [county] nvarchar(max)  NOT NULL,
    [acreage] float  NOT NULL,
    [use_value] nvarchar(max)  NOT NULL,
    [fair_value] nvarchar(max)  NOT NULL,
    [village] nvarchar(max)  NOT NULL,
    [user] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Values'
ALTER TABLE [dbo].[Values]
ADD CONSTRAINT [PK_Values]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------