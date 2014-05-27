using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Globalization;
using DevExpress.Xpf.Grid;
using BeautySmileCRM.Services;

namespace BeautySmileCRM.Converters
{
    public class StaffForServiceConverter : MarkupExtension, IValueConverter
    {
        public StaffForServiceConverter()
        {
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rowValueConverter = new RowPropertyValueConverter();
            var serviceID = (int?)(rowValueConverter as IValueConverter).Convert(value, null, "ServiceID", null);
            var staffs = (IEnumerable<Models.Staff>)SesionService.Cache["AllStaffs"];
            if (serviceID.HasValue)
            {
                return staffs.Where(x => x.Services.Any(s => s.ID == serviceID));
            };
            return staffs;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}