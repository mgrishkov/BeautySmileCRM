CREATE TABLE [CONF].[TransactionType] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [Code]          VARCHAR (50)    NOT NULL,
    [Name]          NVARCHAR (255)  NOT NULL,
    [Description]   NVARCHAR (4000) NULL,
    [OperationSign] INT             DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK#TransactionType] PRIMARY KEY CLUSTERED ([ID] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Типы финансовых операций', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'TransactionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД типа операции', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'TransactionType', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Код', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'TransactionType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Название', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'TransactionType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'TransactionType', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Знак операции по отношению к балансу клиента (1 - прибавление, -1 - вычитание)', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'TransactionType', @level2type = N'COLUMN', @level2name = N'OperationSign';


GO
GRANT UPDATE
    ON OBJECT::[CONF].[TransactionType] TO [AppUser]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[CONF].[TransactionType] TO [AppUser]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[CONF].[TransactionType] TO [AppUser]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[CONF].[TransactionType] TO [AppUser]
    AS [dbo];

