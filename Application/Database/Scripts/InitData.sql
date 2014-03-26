
SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

MERGE INTO CONF.PrivelegeGroup t1 USING (SELECT 1 id) t2 ON (t1.ID = 1)
WHEN MATCHED THEN UPDATE  SET Code = N'ADM', Name = N'Администрирование', Description = NULL
WHEN NOT MATCHED THEN INSERT (ID, Code, Name, Description) VALUES (1, N'ADM', N'Администрирование', NULL);
MERGE INTO CONF.PrivelegeGroup t1 USING (SELECT 1 id) t2 ON (t1.ID = 2)
WHEN MATCHED THEN UPDATE  SET Code = N'CONF', Name = N'Конфигурация', Description = NULL
WHEN NOT MATCHED THEN INSERT (ID, Code, Name, Description) VALUES (2, N'CONF', N'Конфигурация', NULL);
MERGE INTO CONF.PrivelegeGroup t1 USING (SELECT 1 id) t2 ON (t1.ID = 3)
WHEN MATCHED THEN UPDATE  SET Code = N'USER', Name = N'Пользователи', Description = NULL
WHEN NOT MATCHED THEN INSERT (ID, Code, Name, Description) VALUES (3, N'USER', N'Пользователи', NULL);
MERGE INTO CONF.PrivelegeGroup t1 USING (SELECT 1 id) t2 ON (t1.ID = 4)
WHEN MATCHED THEN UPDATE  SET Code = N'STAFF', Name = N'Персонал', Description = NULL
WHEN NOT MATCHED THEN INSERT (ID, Code, Name, Description) VALUES (4, N'STAFF', N'Персонал', NULL);
MERGE INTO CONF.PrivelegeGroup t1 USING (SELECT 1 id) t2 ON (t1.ID = 5)
WHEN MATCHED THEN UPDATE  SET Code = N'CST', Name = N'Клиент', Description = NULL
WHEN NOT MATCHED THEN INSERT (ID, Code, Name, Description) VALUES (5, N'CST', N'Клиент', NULL);
MERGE INTO CONF.PrivelegeGroup t1 USING (SELECT 1 id) t2 ON (t1.ID = 6)
WHEN MATCHED THEN UPDATE  SET Code = N'FIN', Name = N'Финансовые операции', Description = NULL
WHEN NOT MATCHED THEN INSERT (ID, Code, Name, Description) VALUES (6, N'FIN', N'Финансовые операции', NULL);
GO

MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 1)
WHEN MATCHED THEN UPDATE  SET Name = N'Login', Description = N'Запуск консоли', GroupID = 1
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (1, N'Login', N'Запуск консоли', 1);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 20)
WHEN MATCHED THEN UPDATE  SET Name = N'ViewConfiguration', Description = N'Просмотр конфигурационных таблиц и справочников', GroupID = 2
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (20, N'ViewConfiguration', N'Просмотр конфигурационных таблиц и справочников', 2);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 21)
WHEN MATCHED THEN UPDATE  SET Name = N'ModifyConfiguration', Description = N'Изменение данных конфигурационных таблиц и справочников', GroupID = 2
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (21, N'ModifyConfiguration', N'Изменение данных конфигурационных таблиц и справочников', 2);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 31)
WHEN MATCHED THEN UPDATE  SET Name = N'CreateCumulativeDiscount', Description = N'Создание накопительных дисконтов', GroupID = 2
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (31, N'CreateCumulativeDiscount', N'Создание накопительных дисконтов', 2);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 32)
WHEN MATCHED THEN UPDATE  SET Name = N'ModifyCumulativeDiscount', Description = N'Изменение накопительных дисконтов', GroupID = 2
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (32, N'ModifyCumulativeDiscount', N'Изменение накопительных дисконтов', 2);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 33)
WHEN MATCHED THEN UPDATE  SET Name = N'DeleteCumulativeDiscount', Description = N'Удаление накопительных скидок', GroupID = 1
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (33, N'DeleteCumulativeDiscount', N'Удаление накопительных скидок', 1);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 100)
WHEN MATCHED THEN UPDATE  SET Name = N'ViewUser', Description = N'Просмотр данных пользователей системы', GroupID = 3
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (100, N'ViewUser', N'Просмотр данных пользователей системы', 3);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 101)
WHEN MATCHED THEN UPDATE  SET Name = N'CreateUser', Description = N'Создание новых пользователей системы', GroupID = 3
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (101, N'CreateUser', N'Создание новых пользователей системы', 3);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 102)
WHEN MATCHED THEN UPDATE  SET Name = N'ModifyUser', Description = N'Изменение личных данных пользователей системы', GroupID = 3
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (102, N'ModifyUser', N'Изменение личных данных пользователей системы', 3);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 103)
WHEN MATCHED THEN UPDATE  SET Name = N'DeleteUser', Description = N'Удаление пользователя', GroupID = 2
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (103, N'DeleteUser', N'Удаление пользователя', 2);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 111)
WHEN MATCHED THEN UPDATE  SET Name = N'ModifySystemUser', Description = N'Редактирование системных пользователей', GroupID = 3
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (111, N'ModifySystemUser', N'Редактирование системных пользователей', 3);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 150)
WHEN MATCHED THEN UPDATE  SET Name = N'GrantPrivelege', Description = N'Управление правами пользователей', GroupID = 3
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (150, N'GrantPrivelege', N'Управление правами пользователей', 3);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 200)
WHEN MATCHED THEN UPDATE  SET Name = N'VIewStaff', Description = N'Просмотр личных карточек сотрудников', GroupID = 4
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (200, N'VIewStaff', N'Просмотр личных карточек сотрудников', 4);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 201)
WHEN MATCHED THEN UPDATE  SET Name = N'CreateStaff', Description = N'Создание личных карточек персонала', GroupID = 4
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (201, N'CreateStaff', N'Создание личных карточек персонала', 4);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 202)
WHEN MATCHED THEN UPDATE  SET Name = N'ModifyStaff', Description = N'Редактирование личных карточек персонала', GroupID = 4
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (202, N'ModifyStaff', N'Редактирование личных карточек персонала', 4);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 203)
WHEN MATCHED THEN UPDATE  SET Name = N'DeleteStaff', Description = N'Удаление сотрудника', GroupID = 3
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (203, N'DeleteStaff', N'Удаление сотрудника', 3);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 300)
WHEN MATCHED THEN UPDATE  SET Name = N'ViewCustomer', Description = N'Просмотр клиентов', GroupID = 5
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (300, N'ViewCustomer', N'Просмотр клиентов', 5);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 301)
WHEN MATCHED THEN UPDATE  SET Name = N'CreateCustomer', Description = N'Создание клиентов', GroupID = 5
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (301, N'CreateCustomer', N'Создание клиентов', 5);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 302)
WHEN MATCHED THEN UPDATE  SET Name = N'ModifyCustomer', Description = N'Изменение клиентов', GroupID = 5
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (302, N'ModifyCustomer', N'Изменение клиентов', 5);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 303)
WHEN MATCHED THEN UPDATE  SET Name = N'DeleteCustomer', Description = N'Удаление клиента', GroupID = 4
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (303, N'DeleteCustomer', N'Удаление клиента', 4);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 310)
WHEN MATCHED THEN UPDATE  SET Name = N'LinkDiscountCard', Description = N'Создание дисконтных карт', GroupID = 5
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (310, N'LinkDiscountCard', N'Создание дисконтных карт', 5);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 311)
WHEN MATCHED THEN UPDATE  SET Name = N'ModifyDiscontCard', Description = N'Изменение дисконтных карт', GroupID = 5
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (311, N'ModifyDiscontCard', N'Изменение дисконтных карт', 5);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 312)
WHEN MATCHED THEN UPDATE  SET Name = N'UnlinkDiscountCard', Description = N'Удаление дисконтной карты', GroupID = 5
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (312, N'UnlinkDiscountCard', N'Удаление дисконтной карты', 5);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 320)
WHEN MATCHED THEN UPDATE  SET Name = N'ViewAppointment', Description = N'Просмотр событий', GroupID = 5
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (320, N'ViewAppointment', N'Просмотр событий', 5);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 321)
WHEN MATCHED THEN UPDATE  SET Name = N'CreateAppointment', Description = N'Создание событий', GroupID = 5
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (321, N'CreateAppointment', N'Создание событий', 5);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 322)
WHEN MATCHED THEN UPDATE  SET Name = N'ModifyAppointment', Description = N'Редактирование событий', GroupID = 5
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (322, N'ModifyAppointment', N'Редактирование событий', 5);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 323)
WHEN MATCHED THEN UPDATE  SET Name = N'DeleteAppointment', Description = N'Удаление визитов', GroupID = 5
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (323, N'DeleteAppointment', N'Удаление визитов', 5);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 350)
WHEN MATCHED THEN UPDATE  SET Name = N'SetCustomDiscount', Description = N'Устанавливать произвольный дисконт', GroupID = 5
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (350, N'SetCustomDiscount', N'Устанавливать произвольный дисконт', 5);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 400)
WHEN MATCHED THEN UPDATE  SET Name = N'ViewFinancialTransaction', Description = N'Просмотр финансовых операций', GroupID = 6
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (400, N'ViewFinancialTransaction', N'Просмотр финансовых операций', 6);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 401)
WHEN MATCHED THEN UPDATE  SET Name = N'CreateFinancialTransaction', Description = N'Создание финансовой операции', GroupID = 6
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (401, N'CreateFinancialTransaction', N'Создание финансовой операции', 6);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 402)
WHEN MATCHED THEN UPDATE  SET Name = N'ModifyFinancialTransaction', Description = N'Изменение финансовой операции', GroupID = 6
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (402, N'ModifyFinancialTransaction', N'Изменение финансовой операции', 6);
MERGE INTO ADM.Privilege t1 USING (SELECT 1 id) t2 ON (t1.ID = 403)
WHEN MATCHED THEN UPDATE  SET Name = N'DeleteFinancialTransaction', Description = N'Удаление финансовой операции', GroupID = 6
WHEN NOT MATCHED THEN INSERT (ID, Name, Description, GroupID) VALUES (403, N'DeleteFinancialTransaction', N'Удаление финансовой операции', 6);
GO


SET IDENTITY_INSERT ADM.[User] ON
GO
MERGE INTO ADM.[User] t1 USING (SELECT 1 id) t2 ON (t1.ID = 5)
WHEN MATCHED THEN UPDATE  SET Login = N'sysdba', Password = N'2B23880480BCC13E69C0FAC5CF09832C', Email = NULL, ExpirationDate = NULL, IsSystem = CONVERT(bit, 'True')
WHEN NOT MATCHED THEN INSERT (ID, Login, Password, Email, ExpirationDate, IsSystem) VALUES (5, N'sysdba', N'2B23880480BCC13E69C0FAC5CF09832C', NULL, NULL, CONVERT(bit, 'True'));
MERGE INTO ADM.[User] t1 USING (SELECT 1 id) t2 ON (t1.ID = 8)
WHEN MATCHED THEN UPDATE  SET Login = N'administrator', Password = N'200CEB26807D6BF99FD6F4F0D1CA54D4', Email = NULL, ExpirationDate = NULL, IsSystem = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (ID, Login, Password, Email, ExpirationDate, IsSystem) VALUES (8, N'administrator', N'200CEB26807D6BF99FD6F4F0D1CA54D4', NULL, NULL, CONVERT(bit, 'False'));
GO
SET IDENTITY_INSERT ADM.[User] OFF
GO

MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 1)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 1);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 1)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 1);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 20)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 20);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 20)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 20);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 21)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 21);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 21)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 21);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 31)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 31);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 31)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 31);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 32)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 32);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 32)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 32);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 33)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 33);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 33)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 33);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 100)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 100);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 100)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 100);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 101)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 101);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 101)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 101);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 102)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 102);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 102)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 102);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 103)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 103);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 103)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 103);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 111)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 111);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 150)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 150);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 150)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 150);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 200)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 200);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 200)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 200);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 201)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 201);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 201)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 201);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 202)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 202);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 202)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 202);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 203)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 203);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 203)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 203);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 300)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 300);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 300)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 300);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 301)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 301);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 301)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 301);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 302)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 302);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 302)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 302);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 303)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 303);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 303)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 303);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 310)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 310);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 310)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 310);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 311)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 311);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 311)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 311);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 312)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 312);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 312)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 312);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 320)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 320);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 320)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 320);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 321)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 321);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 321)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 321);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 322)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 322);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 322)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 322);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 323)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 323);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 323)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 323);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 350)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 350);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 350)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 350);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 400)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 400);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 400)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 400);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 401)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 401);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 401)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 401);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 402)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 402);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 402)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 402);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 5 AND PrivilegeID = 403)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (5, 403);
MERGE INTO ADM.UserPrivilege t1 USING (SELECT 1 id) t2 ON (t1.UserID = 8 AND PrivilegeID = 403)
WHEN NOT MATCHED THEN INSERT (UserID, PrivilegeID) VALUES (8, 403);
GO

MERGE INTO CONF.AppointmentState t1 USING (SELECT 1 id) t2 ON (t1.ID = 2)
WHEN MATCHED THEN UPDATE  SET Code = N'Active', Name = N'Активный', Description = NULL
WHEN NOT MATCHED THEN INSERT (ID, Code, Name, Description) VALUES (2, N'Active', N'Активный', NULL);
MERGE INTO CONF.AppointmentState t1 USING (SELECT 1 id) t2 ON (t1.ID = 3)
WHEN MATCHED THEN UPDATE  SET Code = N'Canceled', Name = N'Отмененный', Description = NULL
WHEN NOT MATCHED THEN INSERT (ID, Code, Name, Description) VALUES (3, N'Canceled', N'Отмененный', NULL);
MERGE INTO CONF.AppointmentState t1 USING (SELECT 1 id) t2 ON (t1.ID = 4)
WHEN MATCHED THEN UPDATE  SET Code = N'Completed', Name = N'Выполнен', Description = NULL
WHEN NOT MATCHED THEN INSERT (ID, Code, Name, Description) VALUES (4, N'Completed', N'Выполнен', NULL);
GO

MERGE INTO CONF.DiscountType t1 USING (SELECT 1 id) t2 ON (t1.ID = 1)
WHEN MATCHED THEN UPDATE  SET Code = N'Cumulative', Name = N'Накопительная скидка', Description = NULL
WHEN NOT MATCHED THEN INSERT (ID, Code, Name, Description) VALUES (1, N'Cumulative', N'Накопительная скидка', NULL);
GO


SET IDENTITY_INSERT CONF.TransactionType ON
GO
MERGE INTO CONF.TransactionType t1 USING (SELECT 1 id) t2 ON (t1.ID = 1)
WHEN MATCHED THEN UPDATE  SET Code = N'Deposit', Name = N'Пополнение', Description = NULL, OperationSign = 1
WHEN NOT MATCHED THEN INSERT (ID, Code, Name, Description, OperationSign) VALUES (1, N'Deposit', N'Пополнение', NULL, 1);
MERGE INTO CONF.TransactionType t1 USING (SELECT 1 id) t2 ON (t1.ID = 2)
WHEN MATCHED THEN UPDATE  SET Code = N'Withdrawal', Name = N'Списание', Description = NULL, OperationSign = -1
WHEN NOT MATCHED THEN INSERT (ID, Code, Name, Description, OperationSign) VALUES (2, N'Withdrawal', N'Списание', NULL, -1);
GO
SET IDENTITY_INSERT CONF.TransactionType OFF
