CREATE TABLE [CST].[FinancialTransaction] (
    [ID]                INT             IDENTITY (1, 1) NOT NULL,
    [CustomerID]        INT             NOT NULL,
    [AppointmentID]     INT             NOT NULL,
    [TransactionTypeID] INT             NOT NULL,
    [Amount]            DECIMAL (13, 2) NOT NULL,
    [Comment]           NVARCHAR (4000) NULL,
    [CreationTime]      DATETIME        DEFAULT (getdate()) NOT NULL,
    [CreatedBy]         INT             NOT NULL,
    [ModificationTime]  DATETIME        NULL,
    [ModifiedBy]        INT             NULL,
    [IsCanceled]        BIT             DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK#FinancialTransaction] PRIMARY KEY NONCLUSTERED ([ID] ASC),
    CONSTRAINT [FK#FinancialTransaction@AppointmentID#Appointment@iD] FOREIGN KEY ([AppointmentID]) REFERENCES [CST].[Appointment] ([ID]),
    CONSTRAINT [FK#FinancialTransaction@CreatedBy#User@ID] FOREIGN KEY ([CreatedBy]) REFERENCES [ADM].[User] ([ID]),
    CONSTRAINT [FK#FinancialTransaction@CustomerID#Customer@ID] FOREIGN KEY ([CustomerID]) REFERENCES [CST].[Customer] ([ID]),
    CONSTRAINT [FK#FinancialTransaction@ModifiedBy#User@ID] FOREIGN KEY ([ModifiedBy]) REFERENCES [ADM].[User] ([ID]),
    CONSTRAINT [FK#FinancialTransaction@TransactionTypeID#TransactionType@ID] FOREIGN KEY ([TransactionTypeID]) REFERENCES [CONF].[TransactionType] ([ID])
);




GO
CREATE NONCLUSTERED INDEX [IFK#FinancialTransaction@CustomerID#Customer@ID]
    ON [CST].[FinancialTransaction]([CustomerID] ASC);


GO
CREATE NONCLUSTERED INDEX [IFK#FinancialTransaction@TransactionTypeID#TransactionType@ID]
    ON [CST].[FinancialTransaction]([TransactionTypeID] ASC);


GO
CREATE NONCLUSTERED INDEX [IFK#FinancialTransaction@CreatedBy#User@ID]
    ON [CST].[FinancialTransaction]([CreatedBy] ASC);


GO
CREATE NONCLUSTERED INDEX [IFK#FinancialTransaction@ModifiedBy#User@ID]
    ON [CST].[FinancialTransaction]([ModifiedBy] ASC);


GO
CREATE NONCLUSTERED INDEX [IFK#FinancialTransaction@AppointmentID#Appointment@iD]
    ON [CST].[FinancialTransaction]([AppointmentID] ASC);


GO
CREATE TRIGGER CST.TIUD#FinancialTransaction
    on CST.FinancialTransaction
    after insert, update, delete as
begin
    with balanceChanges
      as (select ft.CustomerID, 
                 sum(tt.OperationSign 
                     * case when ft.IsCanceled = 1
                            then 0
                            else ft.Amount
                       end) as Balance
            from CST.FinancialTransaction ft
                 inner join (select distinct CustomerID 
                               from INSERTED
                              where 1 = 1
                             union
                             select distinct CustomerID 
                               from DELETED
                              where 1 = 1) i
              on ft.CustomerID = i.CustomerID
                 inner join CONF.TransactionType tt
              on tt.ID = ft.TransactionTypeID
           where 1 = 1
           group by ft.CustomerID)
    update c
       set c.MoneyBalance = b.Balance
      from CST.Customer c
           inner join balanceChanges b
        on (c.ID = b.CustomerID)
     where 1 = 1;
   
end

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Финансовая операции клиента', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'FinancialTransaction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД платежа', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'FinancialTransaction', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД клиента', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'FinancialTransaction', @level2type = N'COLUMN', @level2name = N'CustomerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД события', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'FinancialTransaction', @level2type = N'COLUMN', @level2name = N'AppointmentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД типа операции', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'FinancialTransaction', @level2type = N'COLUMN', @level2name = N'TransactionTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Сумма', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'FinancialTransaction', @level2type = N'COLUMN', @level2name = N'Amount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Комментарий по операции', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'FinancialTransaction', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и время создания', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'FinancialTransaction', @level2type = N'COLUMN', @level2name = N'CreationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД пользвателя, создавшего событие', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'FinancialTransaction', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата и время изменения', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'FinancialTransaction', @level2type = N'COLUMN', @level2name = N'ModificationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД пользвателя, выполнившего изменение', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'FinancialTransaction', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Признак отмены', @level0type = N'SCHEMA', @level0name = N'CST', @level1type = N'TABLE', @level1name = N'FinancialTransaction', @level2type = N'COLUMN', @level2name = N'IsCanceled';

