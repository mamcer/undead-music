CREATE TABLE [dbo].[PlaylistSong] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [Position]    INT NOT NULL,
    [Song_Id]     INT NULL,
    [User_Id]     INT NULL,
    [Playlist_Id] INT NULL,
    CONSTRAINT [PK_dbo.PlaylistSong] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.PlaylistSong_dbo.Playlist_Playlist_Id] FOREIGN KEY ([Playlist_Id]) REFERENCES [dbo].[Playlist] ([Id]),
    CONSTRAINT [FK_dbo.PlaylistSong_dbo.Song_Song_Id] FOREIGN KEY ([Song_Id]) REFERENCES [dbo].[Song] ([Id]),
    CONSTRAINT [FK_dbo.PlaylistSong_dbo.User_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Song_Id]
    ON [dbo].[PlaylistSong]([Song_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_User_Id]
    ON [dbo].[PlaylistSong]([User_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Playlist_Id]
    ON [dbo].[PlaylistSong]([Playlist_Id] ASC);

