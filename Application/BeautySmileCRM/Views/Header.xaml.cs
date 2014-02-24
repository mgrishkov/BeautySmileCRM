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

        // Using a DependencyProperty as the backing store for ShowNavigationPanel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowNavigationPanelProperty =
            DependencyProperty.Register("ShowNavigationPanel", typeof(bool), typeof(Header), new PropertyMetadata(true));
        
        public Header()
        {
            InitializeComponent();   
        }

    }
}
