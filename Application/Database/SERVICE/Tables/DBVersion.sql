CREATE TABLE [SERVICE].[DBVersion] (
    [ID]                   INT           IDENTITY (1, 1) NOT NULL,
    [Major]                INT           NOT NULL,
    [Minor]                INT           NOT NULL,
    [Build]                INT           NOT NULL,
    [Revision]             INT           NOT NULL,
    [InstallationStarted]  DATETIME2 (7) NOT NULL,
    [InstallationFinished] DATETIME2 (7) NULL,
    CONSTRAINT [PK#DBVersion] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UK#DBVersion] UNIQUE NONCLUSTERED ([Major] ASC, [Minor] ASC, [Build] ASC, [Revision] ASC)
);

