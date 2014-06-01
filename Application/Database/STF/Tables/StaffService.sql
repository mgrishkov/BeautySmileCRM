CREATE TABLE [STF].[StaffService] (
    [StaffID]   INT NOT NULL,
    [ServiceID] INT NOT NULL,
    CONSTRAINT [PK#StaffService] PRIMARY KEY CLUSTERED ([StaffID] ASC, [ServiceID] ASC),
    CONSTRAINT [FK#StaffService@ServiceID#Service@ID] FOREIGN KEY ([ServiceID]) REFERENCES [CONF].[Service] ([ID]),
    CONSTRAINT [FK#StaffService@StaffID#Staff@ID] FOREIGN KEY ([StaffID]) REFERENCES [STF].[Staff] ([ID]) ON DELETE CASCADE
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД услуги', @level0type = N'SCHEMA', @level0name = N'STF', @level1type = N'TABLE', @level1name = N'StaffService', @level2type = N'COLUMN', @level2name = N'ServiceID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД отрудника', @level0type = N'SCHEMA', @level0name = N'STF', @level1type = N'TABLE', @level1name = N'StaffService', @level2type = N'COLUMN', @level2name = N'StaffID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Услуги персонала', @level0type = N'SCHEMA', @level0name = N'STF', @level1type = N'TABLE', @level1name = N'StaffService';


GO
GRANT UPDATE
    ON OBJECT::[STF].[StaffService] TO [AppUser]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[STF].[StaffService] TO [AppUser]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[STF].[StaffService] TO [AppUser]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[STF].[StaffService] TO [AppUser]
    AS [dbo];

