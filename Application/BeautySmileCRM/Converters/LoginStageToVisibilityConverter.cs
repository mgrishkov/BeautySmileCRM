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
using BeautySmileCRM.Enums;

namespace BeautySmileCRM.Converters
{
    public class LoginStageToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public LoginStageToVisibilityConverter()
        {
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stage = (LoginStage)value;
            var panelStage = (LoginStage)Enum.Parse(typeof(LoginStage), (string)parameter);

            return (stage == panelStage) ? Visibility.Visible : Visibility.Collapsed;
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