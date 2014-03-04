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
using DevExpress.Xpf.Mvvm.UI;

namespace BeautySmileCRM.Converters
{
    public class MouseButtonEventArgsConverter : EventArgsConverterBase<MouseButtonEventArgs>
    {
        protected override object Convert(MouseButtonEventArgs args)
        {
            return args;
        }
    }
}