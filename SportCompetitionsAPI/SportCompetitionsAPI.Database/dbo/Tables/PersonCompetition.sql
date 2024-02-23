CREATE TABLE [dbo].[PersonCompetition] (
    [PersonId]      UNIQUEIDENTIFIER NOT NULL,
    [CompetitionId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([PersonId] ASC, [CompetitionId] ASC),
    FOREIGN KEY ([CompetitionId]) REFERENCES [dbo].[Competition] ([Id]) ON DELETE CASCADE,
    FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]) ON DELETE CASCADE
);

