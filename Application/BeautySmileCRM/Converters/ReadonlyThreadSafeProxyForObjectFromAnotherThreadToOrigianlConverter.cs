using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using System.Globalization;
using DevExpress.Data.Async.Helpers;

namespace BeautySmileCRM.Converters
{
    public class ReadonlyThreadSafeProxyForObjectFromAnotherThreadToOrigianlConverter : MarkupExtension, IValueConverter
    {
        public ReadonlyThreadSafeProxyForObjectFromAnotherThreadToOrigianlConverter()
        {
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var obj = value as ReadonlyThreadSafeProxyForObjectFromAnotherThread;
            if (obj != null)
                return obj.OriginalRow;
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
