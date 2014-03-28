CREATE TABLE [ADM].[UserPrivilege] (
    [UserID]      INT NOT NULL,
    [PrivilegeID] INT NOT NULL,
    CONSTRAINT [PK#UserPrivilege] PRIMARY KEY CLUSTERED ([PrivilegeID] ASC, [UserID] ASC),
    CONSTRAINT [FK#UserPrivilege@PrivelegeID#Privelege@ID] FOREIGN KEY ([PrivilegeID]) REFERENCES [ADM].[Privilege] ([ID]),
    CONSTRAINT [FK#UserPrivilege@UserID#User@ID] FOREIGN KEY ([UserID]) REFERENCES [ADM].[User] ([ID]) ON DELETE CASCADE
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД учетной записи', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'UserPrivilege', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ИД привилегии', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'TABLE', @level1name = N'UserPrivilege', @level2type = N'COLUMN', @level2name = N'PrivilegeID';


GO
GRANT UPDATE
    ON OBJECT::[ADM].[UserPrivilege] TO [AppUser]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[ADM].[UserPrivilege] TO [AppUser]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[ADM].[UserPrivilege] TO [AppUser]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[ADM].[UserPrivilege] TO [AppUser]
    AS [dbo];

