CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(50) NOT NULL, 
    [EmailAddress] NVARCHAR(100) NOT NULL, 
    [Product] NVARCHAR(50) NOT NULL, 
    [OrderTime] DATETIME NOT NULL, 
    [Status] NVARCHAR(50) NOT NULL
)
