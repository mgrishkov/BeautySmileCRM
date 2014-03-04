using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using BeautySmileCRM.Interfaces;
using DevExpress.Xpf.Mvvm;

namespace BeautySmileCRM.ViewModels
{
    public class DialogWindow : BaseNavigationViewModel
    {
        private bool _allowSave;
        private string _title;
        private string _subTitle;
        private IDialogUserControl _innerControl;
        private bool _readOnly;
        private double _width;
        private double _height;

        public bool AllowSave
        {
            get { return _allowSave; }
            set
            {
                if(_allowSave != value)
                {
                    _allowSave = value;
                    RaisePropertiesChanged("AllowSave");
                };
            }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                if(_title != value)
                {
                    _title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }
        public string SubTitle
        {
            get { return _subTitle; }
            set
            {
                if (_subTitle != value)
                {
                    _subTitle = value;
                    RaisePropertyChanged("SubTitle");
                }
            }
        }
        public IDialogUserControl InnerControl
        {
            get { return _innerControl; }
            private set
            {
                if (_innerControl != value)
                {
                    _innerControl = value;
                    _innerControl.AllowSaveChanged += _innerControl_AllowSaveChanged;
                    RaisePropertyChanged();
                };
            }
        }
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                if (_readOnly != value)
                {
                    _readOnly = value;
                    RaisePropertiesChanged("ReadOnly");
                }
            }
        }
        public double Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    RaisePropertiesChanged("Width");
                }
            }
        }
        public double Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    RaisePropertiesChanged("Height");
                }
            }
        }


        public ICommand OnCloseButtonClickCommand { get; private set; }
        public ICommand OnSaveButtonClickCommand { get; private set; }
        public ICommand OnWindowMouseDownCommand { get; private set; }
        
        
        public DialogWindow()
        {
            Width = 400;
            Height = 300;
            AllowSave = false;
            OnCloseButtonClickCommand = new DelegateCommand(OnCloseButtonClickCommandExecute);
            OnSaveButtonClickCommand = new DelegateCommand(OnSaveButtonClickCommandExecute,
                () => { return AllowSave; });
            OnWindowMouseDownCommand = new DelegateCommand<MouseButtonEventArgs>(OnWindowMouseDownCommandExecute);
        }
        public DialogWindow(IDialogUserControl innerControl, bool readOnly)
            : this()
        {
            InnerControl = innerControl;
            var ctrl = (UserControl)InnerControl;
            
            Width = ctrl.Width + 30;
            Height = ctrl.Height + 120;
            ReadOnly = readOnly;
        }

        private void OnCloseButtonClickCommandExecute() 
        {
            
        }
        private void OnSaveButtonClickCommandExecute()
        {

        }
        private void OnWindowMouseDownCommandExecute(MouseButtonEventArgs e)
        {

        }
        private void _innerControl_AllowSaveChanged(object sender, EventArgs e)
        {
            var ctrl = (IDialogUserControl)sender;
            AllowSave = ctrl.AllowSave;
        }
        public void Bind<T>(T ID)
        {
            if (_innerControl != null)
            {
                _innerControl.Bind<T>(ID);
            }
        }

    }
}
