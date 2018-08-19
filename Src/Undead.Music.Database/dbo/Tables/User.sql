CREATE TABLE [dbo].[User] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [NickName] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

