CREATE DATABASE [Sample]
GO

USE  [Sample]
GO

CREATE TABLE [Sample].[dbo].[Configurations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(150) NOT NULL, 
    [Type] VARCHAR(50) NOT NULL, 
    [Value] NVARCHAR(500) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [ApplicationName] NVARCHAR(150) NOT NULL
)

GO

INSERT INTO dbo.Configurations VALUES ('SiteName', 'String' ,'Boyner.com.tr', 1,'SERVICE-A')
INSERT INTO dbo.Configurations VALUES ('IsBasketEnabled', 'Boolean' ,1, 1,'SERVICE-B')
INSERT INTO dbo.Configurations VALUES ('MaxItemCount', 'Int' ,50, 0,'SERVICE-A')
INSERT INTO dbo.Configurations VALUES ('SameKey', 'Int' ,1, 1,'SERVICE-B')
INSERT INTO dbo.Configurations VALUES ('SameKey', 'Int' ,50, 1,'SERVICE-A')
