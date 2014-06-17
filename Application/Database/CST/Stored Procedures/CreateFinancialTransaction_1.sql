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
            @message nvarchar(4000),
            @isFixedDiscount bit;
    set @now = getdate();
    begin transaction;
    begin try
        insert into CST.FinancialTransaction 
            (CustomerID, AppointmentID, TransactionTypeID, Amount, Comment, CreationTIme, CreatedBy)
        values 
            (@customerID, @appointmentID, @transactionTypeID, @amount, @comment, getdate(), @userID);
        set @id = scope_identity();
        if(exists(select 1 /* РѕРїРµСЂР°С†РёСЏ СѓРІРµР»РёС‡РёРІР°РµС‚ Р±Р°Р»Р°РЅСЃ */
                    from CONF.TransactionType tt
                   where tt.OperationSign = 1
                     and tt.ID = @transactionTypeID)
           and exists(select 1 /* РєР»РёРµРЅС‚ РёРјРµРµС‚ РґРёСЃРєРѕРЅС‚РЅСѓСЋ РєР°СЂС‚Сѓ */
                        from CST.DiscountCard dc 
                             inner join CST.Customer c 
                          on dc.ID = c.DiscountCardID
                       where c.ID = @customerID))
        begin
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
               and @discountType = 1 /* РЅР°РєРѕРїРёС‚РµР»СЊРЅР°СЏ СЃРєРёРґРєР° */)
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

        declare @apointmentStateID int,
                @appointmentToPay decimal(13,2),
                @payed decimal(13,2);
        select @apointmentStateID = a.StateID,
               @appointmentToPay = a.ToPay
          from CST.Appointment a
         where a.ID = @appointmentID;

        set @payed = isnull((select sum(ft.Amount)
                               from CST.FinancialTransaction ft
                              where ft.AppointmentID = @appointmentID
                                and ft.TransactionTypeID = 1
                                and ft.IsCanceled = 0), 0);
        
        if(@apointmentStateID = 3 /* canceled */)
        begin
            return;
        end        
        else
        begin
            update CST.Appointment
               set StateID = case when @appointmentToPay > @payed
                                  then 2
                                  else 4
                             end
             where ID = @appointmentID;
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

