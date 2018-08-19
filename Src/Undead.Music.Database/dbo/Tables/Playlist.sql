CREATE TABLE [dbo].[Playlist] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_dbo.Playlist] PRIMARY KEY CLUSTERED ([Id] ASC)
);

