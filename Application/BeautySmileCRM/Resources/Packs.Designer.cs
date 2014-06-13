﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeautySmileCRM.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Packs {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Packs() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BeautySmileCRM.Resources.Packs", typeof(Packs).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;
        ///
        ///SET NUMERIC_ROUNDABORT OFF;
        ///GO
        ///
        ///USE [master];
        ///GO
        ///
        ///IF EXISTS (SELECT 1
        ///           FROM   [master].[dbo].[sysdatabases]
        ///           WHERE  [name] = N&apos;CRM&apos;)
        ///    BEGIN
        ///        ALTER DATABASE [CRM]
        ///            SET ANSI_NULLS ON,
        ///                ANSI_PADDING ON,
        ///                ANSI_WARNINGS ON,
        ///                ARITHABORT ON,
        ///                CONCAT_NULL_YIELDS_NULL ON,
        ///                NUMERIC_ [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string v_1_0_0_0 {
            get {
                return ResourceManager.GetString("v_1_0_0_0", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to use CRM
        ///go
        ///
        ///PRINT N&apos;Altering CONF.GetCumulativeDiscountID...&apos;;
        ///go
        ///
        ///alter FUNCTION CONF.GetCumulativeDiscountID(@discountCardID int)
        ///returns int
        ///begin
        ///    declare @result int;
        ///    with discounts
        ///        as (select cd.ID,
        ///                   cd.PurchaseTopLimit, 
        ///                   row_number() over (order by cd.PurchaseTopLimit) RowNumber
        ///              from (select ID,
        ///                           PurchaseTopLimit
        ///                      from CONF.CumulativeDiscount
        ///                    union all
        /// </summary>
        internal static string v_1_0_0_1 {
            get {
                return ResourceManager.GetString("v_1_0_0_1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to use CRM
        ///go
        ///
        ///PRINT N&apos;Altering CST.DiscountCard...&apos;;
        ///go
        ///
        ///alter table CST.DiscountCard add FixedDiscount bit not null default 0
        ///go
        ///
        ///PRINT N&apos;Altering CST.CreateFinancialTransaction...&apos;;
        ///go
        ///
        ///alter PROCEDURE CST.CreateFinancialTransaction
        ///    @userID int,
        ///    @transactionTypeID int,
        ///    @customerID int,
        ///    @appointmentID int,
        ///    @amount decimal(13, 2),
        ///    @comment nvarchar(4000)
        ///AS 
        ///begin
        ///    declare @id int,
        ///            @discountCardID int,
        ///            @discountType int,
        ///            @t [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string v_1_0_0_2 {
            get {
                return ResourceManager.GetString("v_1_0_0_2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to use CRM
        ///go
        ///
        ///insert into ADM.Privilege (ID, Name, Description, GroupID)
        ///values (34, &apos;CreateService&apos;, &apos;Создание услуги&apos;, 2),
        ///       (35, &apos;ModifyService&apos;, &apos;Изменение услуги&apos;, 2),
        ///       (36, &apos;DeleteService&apos;, &apos;Удаление услуги&apos;, 2),
        ///       (37, &apos;ViewService&apos;, &apos;Просмотр правочника услуги&apos;, 2);
        ///go
        ///insert into ADM.UserPrivilege (UserID, PrivilegeID)
        ///values (5, 34),
        ///       (5, 35),
        ///       (5, 36),
        ///       (5, 37),
        ///       (8, 34),
        ///       (8, 35),
        ///       (8, 36),
        ///       (8, 37);
        ///go
        ///
        ///alter table CST. [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string v_1_1_0_2 {
            get {
                return ResourceManager.GetString("v_1_1_0_2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to use CRM
        ///go
        ///
        ///alter TRIGGER [CST].[TIUD#FinancialTransaction]
        ///    on CST.FinancialTransaction
        ///    after insert, update, delete as
        ///begin
        ///
        ///    with affectedCustomers 
        ///      as (select distinct CustomerID 
        ///           from INSERTED
        ///          where 1 = 1
        ///         union
        ///         select distinct CustomerID 
        ///           from DELETED
        ///          where 1 = 1),
        ///        balanceChanges
        ///      as (select ft.CustomerID, 
        ///                 sum(tt.OperationSign 
        ///                     * case when ft.IsCanceled = 1 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string v_1_2_0_4 {
            get {
                return ResourceManager.GetString("v_1_2_0_4", resourceCulture);
            }
        }
    }
}