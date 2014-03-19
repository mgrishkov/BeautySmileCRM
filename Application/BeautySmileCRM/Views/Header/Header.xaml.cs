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
using DevExpress.Xpf.Mvvm;
using DevExpress.Xpf.WindowsUI.Internal;

namespace BeautySmileCRM.Views
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public bool ShowNavigationPanel
        {
            get { return (bool)GetValue(ShowNavigationPanelProperty); }
            set { SetValue(ShowNavigationPanelProperty, value); }
        }
        public static readonly DependencyProperty ShowNavigationPanelProperty =
            DependencyProperty.Register("ShowNavigationPanel", typeof(bool), typeof(Header), new PropertyMetadata(true));

        public string SubTitle
        {
            get { return (string)GetValue(SubTitleProperty); }
            set { SetValue(SubTitleProperty, value); }
        }
        public static readonly DependencyProperty SubTitleProperty =
            DependencyProperty.Register("SubTitle", typeof(string), typeof(Views.Header), new PropertyMetadata(String.Empty));

        public Header()
        {
            InitializeComponent();   
        }

    }
}
