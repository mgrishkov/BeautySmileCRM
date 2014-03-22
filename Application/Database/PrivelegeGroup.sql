CREATE TABLE [CONF].[PrivelegeGroup] (
    [ID]          INT             NOT NULL,
    [Code]        VARCHAR (50)    NOT NULL,
    [Name]        NVARCHAR (255)  NOT NULL,
    [Description] NVARCHAR (4000) NULL,
    CONSTRAINT [PK#PrivelegeGroup] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Группа привелегий', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'PrivelegeGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД группы', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'PrivelegeGroup', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Код', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'PrivelegeGroup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Название', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'PrivelegeGroup', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'PrivelegeGroup', @level2type = N'COLUMN', @level2name = N'Description';

