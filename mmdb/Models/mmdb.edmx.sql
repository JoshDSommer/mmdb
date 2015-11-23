
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/20/2015 15:53:10
-- Generated from EDMX file: C:\CODE\mmdb\mmdb\Models\mmdb.edmx
-- --------------------------------------------------
SET QUOTED_IDENTIFIER OFF;

GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Movies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Movies];
GO
IF OBJECT_ID(N'[dbo].[Actors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Actors];
GO
IF OBJECT_ID(N'[dbo].[MovieActors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MovieActors];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Movies'
CREATE TABLE [dbo].[Movies] (
    [MovieId] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Genre] nvarchar(max)  NOT NULL,
    [Poster] nvarchar(max)  NOT NULL,
    [ReleaseDate] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Actors'
CREATE TABLE [dbo].[Actors] (
    [ActorId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MovieActors'
CREATE TABLE [dbo].[MovieActors] (
    [MovieId] int  NOT NULL,
    [ActorId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MovieId] in table 'Movies'
ALTER TABLE [dbo].[Movies]
ADD CONSTRAINT [PK_Movies]
    PRIMARY KEY CLUSTERED ([MovieId] ASC);
GO

-- Creating primary key on [ActorId] in table 'Actors'
ALTER TABLE [dbo].[Actors]
ADD CONSTRAINT [PK_Actors]
    PRIMARY KEY CLUSTERED ([ActorId] ASC);
GO

-- Creating primary key on [MovieId], [ActorId] in table 'MovieActors'
ALTER TABLE [dbo].[MovieActors]
ADD CONSTRAINT [PK_MovieActors]
    PRIMARY KEY CLUSTERED ([MovieId], [ActorId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------