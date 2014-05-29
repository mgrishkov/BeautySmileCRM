insert into ADM.Privilege (ID, Name, Description, GroupID)
values (34, 'CreateService', 'Создание услуги', 2),
       (35, 'ModifyService', 'Изменение услуги', 2),
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

alter table CST.DiscountCard add InitialDiscountPercent decimal (4,2) not null default (0)
go
exec sp_addextendedproperty N'MS_Description',
                            N'Признак фиксированной скидки (без пересчета)',
                            'SCHEMA',
                            N'CST',
                            'TABLE',
                            N'DiscountCard',
                            'COLUMN',
                            N'FixedDiscount'
go

exec sp_addextendedproperty N'MS_Description',
                            N'Начальная скидка (устанавливается при ручной корректировки скидки',
                            'SCHEMA',
                            N'CST',
                            'TABLE',
                            N'DiscountCard',
                            'COLUMN',
                            N'InitialDiscountPercent'
go

alter FUNCTION CONF.GetCumulativeDiscountID(@discountCardID int)
returns int
begin
    declare @result int,
            @initialDiscount decimal(4,2);

    select @initialDiscount = dc.InitialDiscountPercent
      from CST.DiscountCard dc
     where dc.ID = @discountCardID;


    with discounts
        as (select cd.ID,
                   cd.PurchaseTopLimit, 
                   cd.[Percent],
                   row_number() over (order by cd.PurchaseTopLimit) RowNumber
              from (select ID,
                           PurchaseTopLimit,
                           [Percent] 
                      from CONF.CumulativeDiscount
                    union all
                    select top 1
                           ID,
                           99999999999999,
                           1
                      from CONF.CumulativeDiscount
                     where PurchaseTopLimit = (select max(PurchaseTopLimit)
                                                 from CONF.CumulativeDiscount)) cd
             where 1 = 1),
         discountQuant 
        as (select d1.ID, 
                   isnull(d2.PurchaseTopLimit, 0) as PurchaseTopLimitFrom,
                   d1.PurchaseTopLimit as PurchaseTopLimitTo,
                   d2.[Percent]
              from discounts d1
                   left outer join discounts d2
                on d2.RowNumber = d1.RowNumber - 1
             where 1 = 1)
    select top 1 
           @result = dq.ID
      from CST.DiscountCard dc
           inner join discountQuant dq
        on (dc.TotalPurchaseValue >= dq.PurchaseTopLimitFrom
            and dc.TotalPurchaseValue < dq.PurchaseTopLimitTo)
     where dc.ID = @discountCardID
     order by dq.PurchaseTopLimitTo desc
    
    if(not exists (select 1 
                     from CONF.CumulativeDiscount cd
                    where cd.ID = @result
                      and cd.[Percent] >= @initialDiscount))
    begin
        select top 1
               @result = 1
          from CONF.CumulativeDiscount cd
         where cd.[Percent] >= @initialDiscount
         order by cd.[Percent];
    end;

    return @result;
end;
GO

alter TRIGGER TIUD#FinancialTransaction
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
   
    if(exists(select 1
                from DELETED d
                     inner join CST.Appointment a
                  on d.AppointmentID = a.ID
               where a.ToPay > isnull((select sum(ft.Amount)
                                         from CST.FinancialTransaction ft
                                        where ft.AppointmentID = d.AppointmentID
                                          and ft.TransactionTypeID = 1
                                          and ft.IsCanceled = 0), 0)))
    begin
        update a
           set a.StateID = 2
          from CST.Appointment a
               inner join DELETED d
            on a.ID = d.AppointmentID
         where 1 = 1;
    end;

end
GO

create table CRM.CONF.Service (
    ID int identity,
    Description nvarchar(4000) not null,
    WorkingMinuts int not null,
    Price decimal(13, 2) not null,
    constraint PK#Service primary key (ID)
) on [PRIMARY]
go

exec sp_addextendedproperty N'MS_Description',
                            N'Справочник услуг',
                            'SCHEMA',
                            N'CONF',
                            'TABLE',
                            N'Service'
go

exec sp_addextendedproperty N'MS_Description',
                            N'ИД услуги',
                            'SCHEMA',
                            N'CONF',
                            'TABLE',
                            N'Service',
                            'COLUMN',
                            N'ID'
go

exec sp_addextendedproperty N'MS_Description',
                            N'Описание',
                            'SCHEMA',
                            N'CONF',
                            'TABLE',
                            N'Service',
                            'COLUMN',
                            N'Description'
go

exec sp_addextendedproperty N'MS_Description',
                            N'Норма времени, мин',
                            'SCHEMA',
                            N'CONF',
                            'TABLE',
                            N'Service',
                            'COLUMN',
                            N'WorkingMinuts'
go

exec sp_addextendedproperty N'MS_Description',
                            N'Стоимость услуги',
                            'SCHEMA',
                            N'CONF',
                            'TABLE',
                            N'Service',
                            'COLUMN',
                            N'Price'
go

grant select, update, insert, delete on CONF.Service to AppUser
go

create table CRM.STF.StaffService (
    StaffID int not null,
    ServiceID int not null,
    constraint PK#StaffService primary key (StaffID, ServiceID),
    constraint FK#StaffService@ServiceID#Service@ID foreign key (ServiceID) references CONF.Service (ID),
    constraint FK#StaffService@StaffID#Staff@ID foreign key (StaffID) references STF.Staff (ID) on delete cascade
) on [PRIMARY]
go

exec sp_addextendedproperty N'MS_Description',
                            N'Услуги персонала',
                            'SCHEMA',
                            N'STF',
                            'TABLE',
                            N'StaffService'
go

exec sp_addextendedproperty N'MS_Description',
                            N'ИД отрудника',
                            'SCHEMA',
                            N'STF',
                            'TABLE',
                            N'StaffService',
                            'COLUMN',
                            N'StaffID'
go

exec sp_addextendedproperty N'MS_Description',
                            N'ИД услуги',
                            'SCHEMA',
                            N'STF',
                            'TABLE',
                            N'StaffService',
                            'COLUMN',
                            N'ServiceID'
go

grant select, update, insert, delete on STF.StaffService to AppUser
go

create table CRM.CST.AppointmentDetail (
    ID int identity,
    AppointmentID int not null,
    StaffID int not null,
    ServiceID int not null,
    Price decimal(13, 2) not null,
    constraint PK#AppointmentDetail primary key (ID),
    constraint FK#AppointmentDetail@AppointmentID#Appointment@ID foreign key (AppointmentID) references CST.Appointment (ID),
    constraint FK#AppointmentDetail@ServiceID#Service@ID foreign key (ServiceID) references CONF.Service (ID),
    constraint FK#AppointmentDetail@StaffID#Staff@ID foreign key (StaffID) references STF.Staff (ID)
) on [PRIMARY]
go

create index FK#AppointmentDetail@ServiceID#Service@ID
on CRM.CST.AppointmentDetail (ServiceID)
on [PRIMARY]
go

create index IFK#AppointmentDetail@AppointmentID#Appointment@ID
on CRM.CST.AppointmentDetail (AppointmentID)
on [PRIMARY]
go

create index IFK#AppointmentDetail@StaffID#Staff@ID
on CRM.CST.AppointmentDetail (StaffID)
on [PRIMARY]
go

exec sp_addextendedproperty N'MS_Description',
                            N'Деталб события',
                            'SCHEMA',
                            N'CST',
                            'TABLE',
                            N'AppointmentDetail'
go

exec sp_addextendedproperty N'MS_Description',
                            N'ИД детали события',
                            'SCHEMA',
                            N'CST',
                            'TABLE',
                            N'AppointmentDetail',
                            'COLUMN',
                            N'ID'
go

exec sp_addextendedproperty N'MS_Description',
                            N'ИД события',
                            'SCHEMA',
                            N'CST',
                            'TABLE',
                            N'AppointmentDetail',
                            'COLUMN',
                            N'AppointmentID'
go

exec sp_addextendedproperty N'MS_Description',
                            N'ИД сотрудника',
                            'SCHEMA',
                            N'CST',
                            'TABLE',
                            N'AppointmentDetail',
                            'COLUMN',
                            N'StaffID'
go

exec sp_addextendedproperty N'MS_Description',
                            N'ИД услуги',
                            'SCHEMA',
                            N'CST',
                            'TABLE',
                            N'AppointmentDetail',
                            'COLUMN',
                            N'ServiceID'
go

exec sp_addextendedproperty N'MS_Description',
                            N'Фактическая стоимость',
                            'SCHEMA',
                            N'CST',
                            'TABLE',
                            N'AppointmentDetail',
                            'COLUMN',
                            N'Price'
go

grant select, update, insert, delete on CST.AppointmentDetail to AppUser
go

alter table CST.Appointment drop constraint FK#Appointment@StaffID#Staff@ID
go
drop index I#Appointment@StaffID@StateID on CST.Appointment
go
alter table CST.Appointment drop column StaffID
go
alter table CST.Appointment drop column Purpose
go


