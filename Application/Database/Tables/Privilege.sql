CREATE TABLE [ADM].[Privilege] (
    [ID]          INT            NOT NULL,
    [Name]        NVARCHAR (256) NOT NULL,
    [Description] NVARCHAR (256) NOT NULL,
    [GroupID]     INT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK#Privilege] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK#Privilege@GroupID#PrivelegeGroup@ID] FOREIGN KEY ([GroupID]) REFERENCES [CONF].[PrivelegeGroup] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IFK#Privilege@GroupID#PrivelegeGroup@ID]
    ON [ADM].[Privilege]([GroupID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Справочник полномочий', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'Privilege';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД полномочия', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'Privilege', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Название ', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'Privilege', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'Privilege', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД группы привелегий', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'Privilege', @level2type = N'COLUMN', @level2name = N'GroupID';

