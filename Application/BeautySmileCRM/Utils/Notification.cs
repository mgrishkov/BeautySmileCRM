using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.WindowsUI;

namespace BeautySmileCRM.Utils
{
    public class Notification
    {
        public static void ShowAlert(System.Windows.FrameworkElement owner, string header, string content)
        {
            WinUIMessageBox.Show(owner, content, header, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.None);
        }
        public static void ShowAlert(string header, string content)
        {
            WinUIMessageBox.Show(content, header, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.None);
        }

        public static System.Windows.MessageBoxResult ShowConfirm(System.Windows.FrameworkElement owner, string header, string content)
        {
            return WinUIMessageBox.Show(owner, content, header, System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.None);
        }
        public static System.Windows.MessageBoxResult ShowCancelableConfirm(System.Windows.FrameworkElement owner, string header, string content)
        {
            return WinUIMessageBox.Show(owner, content, header, System.Windows.MessageBoxButton.YesNoCancel, System.Windows.MessageBoxImage.None);
        }
        public static System.Windows.MessageBoxResult ShowCancelableConfirm(string header, string content)
        {
            return WinUIMessageBox.Show(content, header, System.Windows.MessageBoxButton.YesNoCancel, System.Windows.MessageBoxImage.None);
        }
    }
}
