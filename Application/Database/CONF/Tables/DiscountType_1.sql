CREATE TABLE [CONF].[DiscountType] (
    [ID]          INT             NOT NULL,
    [Code]        VARCHAR (255)   NOT NULL,
    [Name]        NVARCHAR (255)  NOT NULL,
    [Description] NVARCHAR (4000) NULL,
    CONSTRAINT [PK#DiscountType] PRIMARY KEY CLUSTERED ([ID] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тип дисконта', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'DiscountType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД типа дисконта', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'DiscountType', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Код типа дисконта', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'DiscountType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Название типа дисконта', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'DiscountType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Описание типа дисконта', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'DiscountType', @level2type = N'COLUMN', @level2name = N'Description';


GO
GRANT UPDATE
    ON OBJECT::[CONF].[DiscountType] TO [AppUser]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[CONF].[DiscountType] TO [AppUser]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[CONF].[DiscountType] TO [AppUser]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[CONF].[DiscountType] TO [AppUser]
    AS [dbo];

