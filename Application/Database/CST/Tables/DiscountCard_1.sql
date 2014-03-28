CREATE TABLE [CST].[DiscountCard] (
    [ID]                 INT             IDENTITY (1, 1) NOT NULL,
    [Code]               VARCHAR (50)    NOT NULL,
    [DiscountPercent]    DECIMAL (4, 2)  DEFAULT ((0)) NOT NULL,
    [TotalPurchaseValue] DECIMAL (13, 2) DEFAULT ((0)) NOT NULL,
    [DiscountTypeID]     INT             DEFAULT ((1)) NOT NULL,
    [MinDiscount]        DECIMAL (13, 2) DEFAULT ((0)) NOT NULL,
    [MaxDiscount]        DECIMAL (13, 2) DEFAULT ((9999999)) NOT NULL,
    CONSTRAINT [PK#DiscountCard] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK#DiscountCard@DiscountTypeID#DiscountTYpe@ID] FOREIGN KEY ([DiscountTypeID]) REFERENCES [CONF].[DiscountType] ([ID])
);




GO
CREATE NONCLUSTERED INDEX [IFK#DiscountCard@DiscountTypeID#DiscountTYpe@ID]
    ON [CST].[DiscountCard]([DiscountTypeID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IU#DiscountCard@Code]
    ON [CST].[DiscountCard]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дисконтная карта', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'DiscountCard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД дисконтной карты', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'DiscountCard', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Код карты', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'DiscountCard', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Текущий процент скидки', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'DiscountCard', @level2type = N'COLUMN', @level2name = N'DiscountPercent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Полная стоимость расходов по карте', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'DiscountCard', @level2type = N'COLUMN', @level2name = N'TotalPurchaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Правило распределения дисконта', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'DiscountCard', @level2type = N'COLUMN', @level2name = N'DiscountTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Минимальная скидка по карте', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'DiscountCard', @level2type = N'COLUMN', @level2name = N'MinDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Максимальная скидка по карте', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'DiscountCard', @level2type = N'COLUMN', @level2name = N'MaxDiscount';


GO
GRANT UPDATE
    ON OBJECT::[CST].[DiscountCard] TO [AppUser]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[CST].[DiscountCard] TO [AppUser]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[CST].[DiscountCard] TO [AppUser]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[CST].[DiscountCard] TO [AppUser]
    AS [dbo];

