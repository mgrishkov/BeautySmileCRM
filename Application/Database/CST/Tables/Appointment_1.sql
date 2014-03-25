CREATE TABLE [CST].[Appointment] (
    [ID]               INT             IDENTITY (1, 1) NOT NULL,
    [CustomerID]       INT             NOT NULL,
    [StartTime]        DATETIME        NOT NULL,
    [EndTime]          DATETIME        NOT NULL,
    [StaffID]          INT             NOT NULL,
    [Purpose]          NVARCHAR (4000) NOT NULL,
    [Price]            DECIMAL (13, 2) NOT NULL,
    [DiscountPercent]  DECIMAL (4, 2)  DEFAULT ((0)) NOT NULL,
    [Discount]         DECIMAL (13, 2) DEFAULT ((0)) NOT NULL,
    [ToPay]            DECIMAL (13, 2) NOT NULL,
    [StateID]          INT             DEFAULT ((1)) NOT NULL,
    [CreationTime]     DATETIME        NOT NULL,
    [CreatedBy]        INT             NOT NULL,
    [ModificationTime] DATETIME        NULL,
    [ModifiedBy]       INT             NULL,
    CONSTRAINT [PK#Appointment] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK#Appointment@CreatedBy#User@ID] FOREIGN KEY ([CreatedBy]) REFERENCES [ADM].[User] ([ID]),
    CONSTRAINT [FK#Appointment@CustomerID#Customer@ID] FOREIGN KEY ([CustomerID]) REFERENCES [CST].[Customer] ([ID]),
    CONSTRAINT [FK#Appointment@ModfiedBy#User@ID] FOREIGN KEY ([ModifiedBy]) REFERENCES [ADM].[User] ([ID]),
    CONSTRAINT [FK#Appointment@StaffID#Staff@ID] FOREIGN KEY ([StaffID]) REFERENCES [STF].[Staff] ([ID]),
    CONSTRAINT [FK#Appointment@StateID#AppointmentState@ID] FOREIGN KEY ([StateID]) REFERENCES [CONF].[AppointmentState] ([ID])
);




GO
CREATE NONCLUSTERED INDEX [I#Appointment@StaffID@StateID]
    ON [CST].[Appointment]([StaffID] ASC, [StateID] ASC);


GO
CREATE NONCLUSTERED INDEX [I#Appointment@StartTime@StateID]
    ON [CST].[Appointment]([StartTime] ASC, [StateID] ASC);


GO
CREATE NONCLUSTERED INDEX [I#Appointment@CustomerID@StateID]
    ON [CST].[Appointment]([CustomerID] ASC, [StateID] ASC);


GO
CREATE TRIGGER CST.TIU#Appointment
    on CST.Appointment
    after insert, update as
begin
    if(update(ToPay))
    begin
        /* создаю операции списания по событиям, если их еще не было */
        insert into CST.FinancialTransaction 
            (CustomerID, AppointmentID, TransactionTypeID, Amount, CreationTime, CreatedBy, Comment)
        select i.CustomerID, i.ID, 2, i.ToPay, i.CreationTime, i.CreatedBy, 'Списание средств за визит'
          from INSERTED i
         where not exists (select 1 
                             from CST.FinancialTransaction ft
                            where ft.AppointmentID = i.ID
                              and ft.TransactionTypeID = 2 /* списание со счета */);
        
        update ft
           set ft.Amount = i.ToPay,
               ft.ModificationTime = i.ModificationTime,
               ft.ModifiedBy = i.ModifiedBy
          from CST.FinancialTransaction ft
               inner join INSERTED i
            on ft.AppointmentID = i.ID
         where ft.TransactionTypeID = 2 /* списание со счета */;
    end;
    if(update(StateID))
    begin
        update ft
           set ft.IsCanceled = 1 
          from CST.FinancialTransaction ft
               inner join INSERTED i
            on (i.ID = ft.AppointmentID)
         where i.StateID = 3; /* canceled */
    end;                
end

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Событие', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД события', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД клиента', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'CustomerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и время начала', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'StartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и время окончания', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'EndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД сотрудника, ответсвенного за встречу', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'StaffID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Цель события', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'Purpose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Сумма', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'Price';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Процент скидки', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'DiscountPercent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Скидка', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'Discount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'К оплате', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'ToPay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД состояния события', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'StateID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и время создания события', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'CreationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД пользвателя, создавшего событие', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и время изменения события', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'ModificationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД пользователя, изменившего событие', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'Appointment', @level2type = N'COLUMN', @level2name = N'ModifiedBy';

