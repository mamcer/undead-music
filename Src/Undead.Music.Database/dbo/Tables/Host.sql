CREATE TABLE [dbo].[Host] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255) NOT NULL,
    [Playlist_Id] INT            NULL,
    CONSTRAINT [PK_dbo.Host] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Host_dbo.Playlist_Playlist_Id] FOREIGN KEY ([Playlist_Id]) REFERENCES [dbo].[Playlist] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Playlist_Id]
    ON [dbo].[Host]([Playlist_Id] ASC);

