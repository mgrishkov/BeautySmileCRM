CREATE PROCEDURE CST.CreateFinancialTransaction
    @userID int,
    @transactionTypeID int,
    @customerID int,
    @appointmentID int,
    @amount decimal(13, 2),
    @comment nvarchar(4000)
AS 
begin
    declare @id int,
            @discountCardID int,
            @discountType int,
            @totalPurchaseValue decimal(13, 2),
            @now datetime,
            @message nvarchar(4000);
    set @now = getdate();
    begin transaction;
    begin try
        insert into CST.FinancialTransaction 
            (CustomerID, AppointmentID, TransactionTypeID, Amount, Comment, CreationTIme, CreatedBy)
        values 
            (@customerID, @appointmentID, @transactionTypeID, @amount, @comment, getdate(), @userID);
        set @id = scope_identity();
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
            select @discountCardID = dc.ID,
                   @discountType = dc.DiscountTypeID
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

            if(@discountType = 1 /* накопительная скидка */)
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
    ON OBJECT::[CST].[CreateFinancialTransaction] TO [AppUser]
    AS [dbo];

