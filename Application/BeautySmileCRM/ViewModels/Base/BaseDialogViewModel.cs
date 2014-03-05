using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BeautySmileCRM.Enums;
using DevExpress.Xpf.Mvvm;
using SmartClasses.Extensions;

namespace BeautySmileCRM.ViewModels.Base
{
    public class BaseDialogViewModel : ViewModelBase
    {
        protected bool _allowSave;
        protected string _title;
        protected string _subTitle;
        protected bool _readOnly;
        protected DialogMode _mode;

        public DialogMode Mode
        {
            get { return _mode; }
            set
            {
                if(_mode != value)
                {
                    _mode = value;
                    Title = _mode.GetDescription();
                    RaisePropertiesChanged("Mode");
                }
            }
        }
        public bool AllowSave
        {
            get { return _allowSave; }
            set
            {
                if (_allowSave != value)
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
                if (_title != value)
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
        public string FullTitle
        {
            get { return String.Format("{0} {1}", Title, SubTitle.ToLower()); }
        }

        public BaseDialogViewModel()
        {
            AllowSave = false;
        }


    }
}
