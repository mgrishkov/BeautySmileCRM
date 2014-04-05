
CREATE PROCEDURE CST.UpdateFinancialTransaction
    @id int,
    @userID int,
    @transactionTypeID int,
    @customerID int,
    @appointmentID int,
    @amount decimal(13, 2),
    @comment nvarchar(4000)
AS 
begin
    begin transaction;
    begin try
        declare @message nvarchar(4000);
        
        select @transactionTypeID = ft.TransactionTypeID,
               @customerID = ft.CustomerID
          from CST.FinancialTransaction ft
         where ID = @id;

        update CST.FinancialTransaction 
           set CustomerID = @customerID,
               TransactionTypeID = @transactionTypeID,
               AppointmentID = @appointmentID,
               Amount = @amount,
               Comment = @comment,
               ModifiedBy = @userID,
               ModificationTime = getdate() 
         where ID = @id;
            
        if(exists(select 1 /* операция увеличивает баланс */
                    from CONF.TransactionType tt
                   where tt.OperationSign = 1
                     and tt.ID = @transactionTypeID)
           and exists(select 1 /* клиент имеет дисконтную карту */
                        from CST.DiscountCard dc 
                             inner join CST.Customer c 
                          on dc.ID = c.DiscountCardID
                       where c.ID = @customerID))
        begin
            declare @discountCardID int,
                    @discountType int,
                    @totalPurchaseValue decimal(13,2),
                    @isFixedDiscount bit;

            select @discountCardID = dc.ID,
                   @discountType = dc.DiscountTypeID,
                   @isFixedDiscount = dc.FixedDiscount
              from CST.DiscountCard dc 
                   inner join CST.Customer c 
                on dc.ID = c.DiscountCardID
             where c.ID = @customerID;

            select @totalPurchaseValue = sum(ft.Amount)
              from CST.Customer c
                   inner join CST.FinancialTransaction ft
                on ft.CustomerID = c.ID
                   inner join CONF.TransactionType tt 
                on (ft.TransactionTypeID = tt.ID
                    and tt.OperationSign = 1) 
             where c.DiscountCardID = @discountCardID;

            update CST.DiscountCard
               set TotalPurchaseValue = isnull(@totalPurchaseValue, 0)
             where ID = @discountCardID;

            if(@isFixedDiscount = 0 
               and @discountType = 1 /* накопительная скидка */)
            begin
                declare @cumulativeDiscountID int;
                set @cumulativeDiscountID = CONF.GetCumulativeDiscountID(@discountCardID);
                
                update dc
                   set dc.DiscountPercent = isnull(cd.[Percent], dc.DiscountPercent),
                       dc.MinDiscount = isnull(cd.MinDiscount, dc.MinDiscount),
                       dc.MaxDiscount = isnull(cd.MaxDiscount, dc.MaxDiscount)
                  from CST.DiscountCard dc
                       left outer join CONF.CumulativeDiscount cd
                    on (@cumulativeDiscountID is not null 
                        and cd.ID = @cumulativeDiscountID)
                 where dc.ID = @discountCardID;                
            end;
        end;
        commit;
    end try
    begin catch
        set @message = error_message();
        rollback;
        raiserror(@message, 16, 1);
    end catch;

    select  ft.ID,
            ft.CustomerID,
            ft.AppointmentID,
            ft.TransactionTypeID,
            ft.Amount,
            ft.Comment,
            ft.CreationTime,
            ft.CreatedBy,
            ft.ModificationTime,
            ft.ModifiedBy 
      from CST.FinancialTransaction ft
     where ft.ID = @id;
end

GO
GRANT EXECUTE
    ON OBJECT::[CST].[UpdateFinancialTransaction] TO [AppUser]
    AS [dbo];

