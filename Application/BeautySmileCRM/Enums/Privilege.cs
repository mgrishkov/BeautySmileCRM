using System;
using System.ComponentModel;

namespace BeautySmileCRM.Enums
{
    /// <summary>
    /// Privilege auto generated enumeration
    /// </summary>
    public enum Privilege
    {
		[Description("Запуск консоли")]
		Login                                                                       = 1,
		[Description("Просмотр конфигурационных таблиц и справочников")]
		ViewConfiguration                                                           = 20,
		[Description("Изменение данных конфигурационных таблиц и справочников")]
		ModifyConfiguration                                                         = 21,
		[Description("Создание накопительных дисконтов")]
		CreateCumulativeDiscount                                                    = 31,
		[Description("Изменение накопительных дисконтов")]
		ModifyCumulativeDiscount                                                    = 32,
		[Description("Удаление накопительных скидок")]
		DeleteCumulativeDiscount                                                    = 33,
		[Description("Просмотр данных пользователей системы")]
		ViewUser                                                                    = 100,
		[Description("Создание новых пользователей системы")]
		CreateUser                                                                  = 101,
		[Description("Изменение личных данных пользователей системы")]
		ModifyUser                                                                  = 102,
		[Description("Удаление пользователя")]
		DeleteUser                                                                  = 103,
		[Description("Редактирование системных пользователей")]
		ModifySystemUser                                                            = 111,
		[Description("Управление правами пользователей")]
		GrantPrivelege                                                              = 150,
		[Description("Просмотр личных карточек сотрудников")]
		VIewStaff                                                                   = 200,
		[Description("Создание личных карточек персонала")]
		CreateStaff                                                                 = 201,
		[Description("Редактирование личных карточек персонала")]
		ModifyStaff                                                                 = 202,
		[Description("Удаление сотрудника")]
		DeleteStaff                                                                 = 203,
		[Description("Просмотр клиентов")]
		ViewCustomer                                                                = 300,
		[Description("Создание клиентов")]
		CreateCustomer                                                              = 301,
		[Description("Изменение клиентов")]
		ModifyCustomer                                                              = 302,
		[Description("Удаление клиента")]
		DeleteCustomer                                                              = 303,
		[Description("Создание дисконтных карт")]
		CreateDiscountCard                                                          = 310,
		[Description("Изменение дисконтных карт")]
		ModifyDiscontCard                                                           = 311,
		[Description("Просмотр событий")]
		ViewAppointment                                                             = 320,
		[Description("Создание событий")]
		CreateAppointment                                                           = 321,
		[Description("Редактирование событий")]
		ModifyAppointment                                                           = 322,
		[Description("Устанавливать произвольный дисконт")]
		SetCustomDiscount                                                           = 323,
		[Description("Просмотр финансовых операций")]
		ViewFinancialTransaction                                                    = 400,
		[Description("Создание финансовой операции")]
		CreateFinancialTransaction                                                  = 401,
		[Description("Изменение финансовой операции")]
		ModifyFinancialOperation                                                    = 402,
		[Description("Удаление финансовой операции")]
		DeleteFinancialTransaction                                                  = 403,
    }        
}