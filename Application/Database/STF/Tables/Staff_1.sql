CREATE TABLE [STF].[Staff] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]     NVARCHAR (100)  NOT NULL,
    [LastName]      NVARCHAR (100)  NOT NULL,
    [MiddleName]    NVARCHAR (100)  NULL,
    [Photo]         IMAGE           NULL,
    [DismissalDate] DATE            NULL,
    [UserID]        INT             NULL,
    [Position]      NVARCHAR (4000) NOT NULL,
    CONSTRAINT [PK#Staff] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK#Staff@UserID#User@ID] FOREIGN KEY ([UserID]) REFERENCES [ADM].[User] ([ID])
);






GO
CREATE NONCLUSTERED INDEX [IU#Staff@LastName@FirstName@MiddleName@DismissalDate]
    ON [STF].[Staff]([LastName] ASC, [FirstName] ASC, [MiddleName] ASC, [DismissalDate] ASC);


GO
CREATE NONCLUSTERED INDEX [IFK#Staff@UserID#User@ID]
    ON [STF].[Staff]([UserID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Личные карточки персонала', @level0type = N'SCHEMA', @level0name = N'STF', @level1type = N'TABLE', @level1name = N'Staff';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД сотрудника', @level0type = N'SCHEMA', @level0name = N'STF', @level1type = N'TABLE', @level1name = N'Staff', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Имя', @level0type = N'SCHEMA', @level0name = N'STF', @level1type = N'TABLE', @level1name = N'Staff', @level2type = N'COLUMN', @level2name = N'FirstName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Фамилия', @level0type = N'SCHEMA', @level0name = N'STF', @level1type = N'TABLE', @level1name = N'Staff', @level2type = N'COLUMN', @level2name = N'LastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Отчество', @level0type = N'SCHEMA', @level0name = N'STF', @level1type = N'TABLE', @level1name = N'Staff', @level2type = N'COLUMN', @level2name = N'MiddleName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Фотография', @level0type = N'SCHEMA', @level0name = N'STF', @level1type = N'TABLE', @level1name = N'Staff', @level2type = N'COLUMN', @level2name = N'Photo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата увольнения', @level0type = N'SCHEMA', @level0name = N'STF', @level1type = N'TABLE', @level1name = N'Staff', @level2type = N'COLUMN', @level2name = N'DismissalDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД пользователя в системе', @level0type = N'SCHEMA', @level0name = N'STF', @level1type = N'TABLE', @level1name = N'Staff', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Должность', @level0type = N'SCHEMA', @level0name = N'STF', @level1type = N'TABLE', @level1name = N'Staff', @level2type = N'COLUMN', @level2name = N'Position';


GO
GRANT UPDATE
    ON OBJECT::[STF].[Staff] TO [AppUser]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[STF].[Staff] TO [AppUser]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[STF].[Staff] TO [AppUser]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[STF].[Staff] TO [AppUser]
    AS [dbo];

