

CREATE VIEW CST.CustomerView (CustomerID, FirstName, LastName, MiddleName, Gender, Country, City, Region, Zip, Phone, MobilePhone, Email, Address, BirthDate, MoneyBalance, NotifyByEmail, NotifyBySms, NotifyByPost, CreationTime, ModificationTime, DiscountcardID, DiscountCardCode, DiscountPercent, FirstVisit, LastVisit, NextVisit)
AS
    select  c.ID as CustomerID,
        c.FirstName,
        c.LastName,
        c.MiddleName,
        c.Gender,
        c.Country,
        c.City,
        c.Region,
        c.Zip,
        c.Phone,
        c.MobilePhone,
        c.Email,
        c.Address,
        c.BirthDate,
        c.MoneyBalance,
        c.NotifyByEmail,
        c.NotifyBySms,
        c.NotifyByPost,
        c.CreationTime,
        c.ModificationTime,
        dc.ID as DiscountcardID,
        dc.Code as DiscountCardCode,
        dc.DiscountPercent,
        (select min(a.StartTime)
           from CST.Appointment a
          where a.CustomerID = c.ID
            and a.StateID = 4 /* Completed */) as FirstVisit,
        (select max(a.StartTime)
           from CST.Appointment a
          where a.CustomerID = c.ID
            and a.StartTime < getdate()
            and a.StateID = 4 /* Completed */) as LastVisit,
        (select max(a.StartTime)
           from CST.Appointment a
          where a.CustomerID = c.ID
            and a.StartTime > getdate()
            and a.StateID not in (3, 4) /* Canceled, Completed */) as NextVisit
  from CST.Customer c
       left outer join CST.DiscountCard dc
    on c.DiscountCardID = dc.ID
 where 1 =1            

GO
GRANT SELECT
    ON OBJECT::[CST].[CustomerView] TO [AppUser]
    AS [dbo];

