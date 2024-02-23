CREATE TABLE [dbo].[Competition] (
    [Id]      UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Name]    NVARCHAR (MAX)   NOT NULL,
    [Date]    DATETIME         NOT NULL,
    [SportId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([SportId]) REFERENCES [dbo].[Sport] ([Id]) ON DELETE CASCADE
);

