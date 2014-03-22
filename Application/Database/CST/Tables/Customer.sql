CREATE TABLE [CST].[Customer] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]        NVARCHAR (255)  NOT NULL,
    [LastName]         NVARCHAR (255)  NOT NULL,
    [MiddleName]       NVARCHAR (255)  NULL,
    [Phone]            VARCHAR (18)    NULL,
    [Email]            VARCHAR (255)   NULL,
    [Address]          NVARCHAR (4000) NULL,
    [BirthDate]        DATETIME2 (7)   NULL,
    [DiscountCardID]   INT             NULL,
    [UserID]           INT             NULL,
    [Photo]            IMAGE           NULL,
    [MoneyBalance]     DECIMAL (13, 2) DEFAULT ((0)) NOT NULL,
    [MobilePhone]      VARCHAR (18)    NULL,
    [Gender]           INT             DEFAULT ((1)) NOT NULL,
    [Country]          NVARCHAR (4000) NULL,
    [City]             NVARCHAR (4000) NULL,
    [Region]           NVARCHAR (4000) NULL,
    [Zip]              INT             NULL,
    [NotifyByEmail]    BIT             DEFAULT ((0)) NOT NULL,
    [NotifyBySms]      BIT             DEFAULT ((0)) NOT NULL,
    [NotifyByPost]     BIT             DEFAULT ((0)) NOT NULL,
    [CreationTime]     DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    [ModificationTime] DATETIME2 (7)   NULL,
    CONSTRAINT [PK#Customer] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK#Customer@DiscountCardID#DiscountCard@ID] FOREIGN KEY ([DiscountCardID]) REFERENCES [CST].[DiscountCard] ([ID]),
    CONSTRAINT [FK#Customer@UserID#User@ID] FOREIGN KEY ([UserID]) REFERENCES [ADM].[User] ([ID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IU#Customer@LastName@FirstName@BirthDate@MiddleName]
    ON [CST].[Customer]([LastName] ASC, [FirstName] ASC, [BirthDate] ASC, [MiddleName] ASC);


GO
CREATE NONCLUSTERED INDEX [IFK#Customer@DiscountCardID#DiscountCard@ID]
    ON [CST].[Customer]([DiscountCardID] ASC);


GO
CREATE NONCLUSTERED INDEX [IFK#Customer@UserID#User@ID]
    ON [CST].[Customer]([UserID] ASC);


GO
CREATE TRIGGER [CST].TU#Customer
    on CST.Customer
    after update as
begin
    update c
       set c.ModificationTime = getdate()
      from CST.Customer c
           inner join INSERTED i
        on (i.ID = c.ID)
     where 1 = 1;      
end

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Личная карточка (ЛК) клиента', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД клиента', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Имя', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'FirstName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Фамилия', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'LastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Отчество', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'MiddleName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Номер телефона', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Адрес эл.почты', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Фактический адрес проживания', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата рождения', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'BirthDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД дисконтной карты', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'DiscountCardID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД зарегистрированного пользователя', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Фотография', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Photo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Баланс клиента', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'MoneyBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Номер контектного телефона', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'MobilePhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Пол: 1-мужской, 2 - женский', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Gender';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Страна', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Country';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Город', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'City';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Область', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Region';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Индекс', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'Zip';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Оповещать по эл.почте', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'NotifyByEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Оповещать по СМС', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'NotifyBySms';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Оповещать по почте', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'NotifyByPost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата создания записи', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CreationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата последнего изменения', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ModificationTime';

