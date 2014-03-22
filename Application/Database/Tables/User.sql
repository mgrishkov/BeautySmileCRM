CREATE TABLE [ADM].[User] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [Login]          NVARCHAR (256) NOT NULL,
    [Password]       NVARCHAR (256) NOT NULL,
    [Email]          NVARCHAR (255) NULL,
    [ExpirationDate] DATE           NULL,
    [IsSystem]       BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK#User] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IU#User@Login@Password]
    ON [ADM].[User]([Login] ASC, [Password] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Учетная запись пользователя', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'User';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД пользователя', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Логин пользователя', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Login';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Пароль пользователя', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Password';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Адрес эл. почты', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Дата блокировки учетной записи', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'ExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Признак системного пользователя', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'User', @level2type = N'COLUMN', @level2name = N'IsSystem';

