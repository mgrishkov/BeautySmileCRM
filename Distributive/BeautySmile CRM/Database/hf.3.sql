insert into ADM.Privilege (ID, Name, Description, GroupID)
values (34, 'CreateService', 'Создание услуги', 2),
       (35, 'ModifySerice', 'Изменение услуги', 2),
       (36, 'DeleteService', 'Удаление услуги', 2),
       (37, 'ViewService', 'Просмотр правочника услуги', 2);
go
insert into ADM.UserPrivilege (UserID, PrivilegeID)
values (5, 34),
       (5, 35),
       (5, 36),
       (5, 37),
       (8, 34),
       (8, 35),
       (8, 36),
       (8, 37);
go