CREATE VIEW CST.FinancialTransactionView
AS
 select  ft.ID,
         ft.CustomerID,
         c.FirstName as CustomerFirstName,
         c.LastName as CustomerLastName,
         c.MiddleName as CustomerMiddleName,
         dc.Code as DiscountCardCode,
         dc.DiscountPercent as DiscountCardPercent,
         dc.TotalPurchaseValue as DiscountCardTotalPurchaseValue,
         dc.MinDiscount as DiscountCardMinDiscount,
         dc.MaxDiscount as DiscountCardMaxDiscount,
         ft.AppointmentID,
         a.Purpose as AppointmentPurpose,
         a.StartTime as AppointmentStartTime,
         a.EndTime as AppointmentEndTime,
         a.StaffID,
         a.Price,
         a.ToPay,
         a.DiscountPercent,
         a.Discount,
         ft.TransactionTypeID,
         ft.Amount,
         ft.Comment,
         ft.CreationTime,
         ft.CreatedBy,
         ft.ModificationTime,
         ft.ModifiedBy
   from CST.FinancialTransaction ft
        inner join CST.Appointment a
     on ft.AppointmentID = a.ID
        inner join CST.Customer c
     on ft.CustomerID = c.ID
        left outer join CST.DiscountCard dc
     on c.DiscountCardID = dc.ID   
  where 1 = 1

GO
GRANT SELECT
    ON OBJECT::[CST].[FinancialTransactionView] TO [AppUser]
    AS [dbo];

