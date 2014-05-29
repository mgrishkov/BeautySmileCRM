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
    public class StaffIDToShortNameConverter : MarkupExtension, IValueConverter
    {
        public StaffIDToShortNameConverter()
        {
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = String.Empty;
            try
            {
                if (value is int)
                {
                    var staffs = SesionService.Cache["AllStaffs"] as IEnumerable<Models.Staff>;
                    var staff = staffs.SingleOrDefault(x => x.ID == (int)value);

                    if (staff != null)
                    {
                        result = staff.ShortName;
                    };
                }
            }
            catch { }
            return result;
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