CREATE TABLE [dbo].[Person] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]        NVARCHAR (MAX)   NOT NULL,
    [Email]       NVARCHAR (MAX)   NOT NULL,
    [DateOfBirth] DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

