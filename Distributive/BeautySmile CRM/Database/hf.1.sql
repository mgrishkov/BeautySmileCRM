use CRM
go

PRINT N'Altering CONF.GetCumulativeDiscountID...';
go

alter FUNCTION CONF.GetCumulativeDiscountID(@discountCardID int)
returns int
begin
    declare @result int;
    with discounts
        as (select cd.ID,
                   cd.PurchaseTopLimit, 
                   row_number() over (order by cd.PurchaseTopLimit) RowNumber
              from (select ID,
                           PurchaseTopLimit
                      from CONF.CumulativeDiscount
                    union all
                    select top 1
                           ID,
                           99999999999999
                      from CONF.CumulativeDiscount
                     where PurchaseTopLimit = (select max(PurchaseTopLimit)
                                                 from CONF.CumulativeDiscount)) cd
             where 1 = 1),
         discountQuant 
        as (select d1.ID, 
                   isnull(d2.PurchaseTopLimit, 0) as PurchaseTopLimitFrom,
                   d1.PurchaseTopLimit as PurchaseTopLimitTo
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
    
    return @result;
end;
go

PRINT N'Update complete.';
go