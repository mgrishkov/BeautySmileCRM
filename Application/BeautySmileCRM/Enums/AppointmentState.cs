using System;
using System.ComponentModel;

namespace BeautySmileCRM.Enums
{
    /// <summary>
    /// AppointmentState auto generated enumeration
    /// </summary>
    public enum AppointmentState
    {
		[Description("Активный")]
		Active              = 2,
		[Description("Отмененный")]
		Canceled            = 3,
		[Description("Выполнен")]
		Completed           = 4,
    }        
}