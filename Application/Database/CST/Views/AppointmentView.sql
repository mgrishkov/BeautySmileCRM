CREATE VIEW CST.AppointmentView 
AS
 select a.ID as AppointmentID,
        a.CustomerID,
        c.FirstName as CustomerFirstName,
        c.LastName as CustomerLastName,
        c.MiddleName as CustomerMiddleName,
        dc.Code as DiscountCardCode,
        dc.DiscountPercent as DiscountCardPercent,
        dc.TotalPurchaseValue as DiscountCardTotalPurchaseValue,
        dc.MinDiscount as DiscountCardMinDiscount,
        dc.MaxDiscount as DiscountCardMaxDiscount,
        a.StartTime,
        a.EndTime,
        a.StaffID,
        a.Purpose,
        a.Price,
        a.DiscountPercent,
        a.Discount,
        a.ToPay,
        cast(isnull((select sum(ft.Amount)
                       from CST.FinancialTransaction ft
                            inner join CONF.TransactionType tt 
                         on ft.TransactionTypeID = tt.ID
                      where ft.AppointmentID = a.ID
                        and tt.OperationSign = 1), 0) as decimal(13,2)) as Payed,
        a.StateID,
        a.CreationTime,
        a.CreatedBy,
        a.ModificationTime,
        a.ModifiedBy
   from CST.Appointment a
        inner join CST.Customer c
     on a.CustomerID = c.ID
        left outer join CST.DiscountCard dc
     on c.DiscountCardID = dc.ID
  where 1 = 1

GO
GRANT SELECT
    ON OBJECT::[CST].[AppointmentView] TO [AppUser]
    AS [dbo];

