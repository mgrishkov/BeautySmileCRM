CREATE PROCEDURE APP.GetCustomerDashboardData
    @date datetime2
AS 
    with data
        as (select c.ID as CustomerID,
                   c.FirstName,
                   c.LastName,
                   c.MiddleName,
                   c.MobilePhone,
                   c.DiscountCardID,
                   a.StartDate as NearestAppointmentDate,
                   row_number() over (order by a.StartDate desc, 
                                               c.LastName,
                                               c.FirstName) as RowNum
              from CST.Customer c
                   left outer join (select CustomerID,
                                           min(StartTime) as StartDate
                                      from CST.Appointment
                                     where EndTime > @date
                                     group by CustomerID) a
                on a.CustomerID = c.ID
             where 1 = 1)
    select  d.CustomerID,
            d.FirstName,
            d.LastName,
            d.MiddleName,
            d.MobilePhone,
            dc.Code as DiscountCardCode,
            d.NearestAppointmentDate,
            case when d.NearestAppointmentDate is null 
                  and d1.NearestAppointmentDate is not null 
                 then 1
            else 0
            end as NewGroup
      from data d
           left outer join data d1
        on d1.RowNum= d.RowNum - 1
           left outer join CST.DiscountCard dc
        on dc.ID = d.DiscountCardID
     order by isnull(d.NearestAppointmentDate, '2199/01/01'),
              d.LastName,
              d.FirstName;

GO
GRANT EXECUTE
    ON OBJECT::[APP].[GetCustomerDashboardData] TO [AppUser]
    AS [dbo];

