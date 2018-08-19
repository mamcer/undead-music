/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


USE [UndeadMusic]
GO

-- User

CREATE TABLE #User (
    [Id]       INT NOT NULL,
    [NickName] NVARCHAR (255) NOT NULL
);

INSERT #User ([Id], [NickName]) VALUES (1, N'mario.moreno')
GO

SET IDENTITY_INSERT [dbo].[User] ON 
GO

MERGE [dbo].[User] dst
USING #User src
ON (src.Id = dst.Id)
WHEN MATCHED THEN
UPDATE SET 
dst.[NickName] = src.[NickName]
WHEN NOT MATCHED THEN
INSERT ([Id], [NickName]) VALUES (src.Id, src.[NickName])
WHEN NOT MATCHED BY SOURCE THEN
DELETE;
GO

SET IDENTITY_INSERT [dbo].[User] OFF
GO

DROP TABLE #User
GO