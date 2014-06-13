

CREATE PROCEDURE CST.RecalculateDiscount
    @discountCardID int
AS 
begin
    begin transaction;
    begin try
        declare @customerID int,
                @discountType int,
                @totalPurchaseValue decimal(13,2),
                @isFixedDiscount bit;

        select @discountType = dc.DiscountTypeID,
               @isFixedDiscount = dc.FixedDiscount,
               @customerID = c.ID
          from CST.DiscountCard dc 
               inner join CST.Customer c 
            on dc.ID = c.DiscountCardID
         where dc.ID = @discountCardID;

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
        commit;

        select dc.ID,
               dc.Code,
               dc.DiscountPercent,
               dc.TotalPurchaseValue,
               dc.DiscountTypeID,
               dc.MinDiscount,
               dc.MaxDiscount,
               dc.FixedDiscount
          from CST.DiscountCard dc
         where dc.ID = @discountCardID;

    end try
    begin catch
        declare @message nvarchar(max) = error_message();
        rollback;
        raiserror(@message, 16, 1);
    end catch;
end
GO
GRANT EXECUTE
    ON OBJECT::[CST].[RecalculateDiscount] TO [AppUser]
    AS [dbo];

