CREATE TABLE [CONF].[CumulativeDiscount] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (255)  NOT NULL,
    [Percent]          DECIMAL (4, 2)  NOT NULL,
    [MinDiscount]      DECIMAL (13, 2) NOT NULL,
    [MaxDiscount]      DECIMAL (13, 2) NOT NULL,
    [PurchaseTopLimit] DECIMAL (13, 2) NOT NULL,
    CONSTRAINT [PK#CumulativeDiscount] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UK#CumulativeDiscount@Name] UNIQUE NONCLUSTERED ([Name] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Справочни нкопительных скидок', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'CumulativeDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД накопительной скидки', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'CumulativeDiscount', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Название скидки', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'CumulativeDiscount', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Процент скидки', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'CumulativeDiscount', @level2type = N'COLUMN', @level2name = N'Percent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Мин. скидка', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'CumulativeDiscount', @level2type = N'COLUMN', @level2name = N'MinDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Макс. скидка', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'CumulativeDiscount', @level2type = N'COLUMN', @level2name = N'MaxDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Верхняя граница потраченной за период суммы', @level0type = N'SCHEMA', @level0name = N'CONF', @level1type = N'TABLE', @level1name = N'CumulativeDiscount', @level2type = N'COLUMN', @level2name = N'PurchaseTopLimit';


GO
GRANT UPDATE
    ON OBJECT::[CONF].[CumulativeDiscount] TO [AppUser]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[CONF].[CumulativeDiscount] TO [AppUser]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[CONF].[CumulativeDiscount] TO [AppUser]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[CONF].[CumulativeDiscount] TO [AppUser]
    AS [dbo];

