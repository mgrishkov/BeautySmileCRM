using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BeautySmileCRM.Enums;
using DevExpress.Xpf.Mvvm;
using SmartClasses.Attributes;
using SmartClasses.Extensions;
using SmartClasses.Utils;

namespace BeautySmileCRM.ViewModels.Base
{
    public class BaseEditDialogViewModel : ViewModelBase, IDataErrorInfo
    {
        protected Models.CRMContext _dc;

        protected bool _allowSave;
        protected string _title;
        protected string _subTitle;
        protected bool _readOnly;
        protected DialogMode _mode;

        protected IDialogService DialogService { get; set; }
        protected IMessageBoxService MessageService { get; set; }

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
                    AllowSaveChanged();
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

        public bool IsValid { get; protected set; }
        public string Error { get; protected set; }
        public virtual string this[string columnName]
        {
            get
            {
                return String.Empty;
            }
        }

        public BaseEditDialogViewModel(DialogMode mode, IDialogService dialogService, IMessageBoxService messageService)
        {
            _dc = new Models.CRMContext();
            AllowSave = false;
            Mode = mode;
            DialogService = dialogService;
            MessageService = messageService;
        }

        public MessageBoxResult ShowEditDialog()
        {
            var commands = new List<UICommand>();
            commands.Add(new UICommand()
            {
                Id = MessageBoxResult.OK,
                Caption = "Сохранить",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand<CancelEventArgs>(
                    OnDialogApplyCommandtExecuting,
                    x => { return this.AllowSave; }
                ),
            });
            commands.Add(new UICommand()
            {
                Id = MessageBoxResult.Cancel,
                Caption = "Закрыть",
                IsCancel = true,
                IsDefault = false,
                Command = new DelegateCommand<CancelEventArgs>(OnDialogCancelCommandtExecuting)
            });
            ExtendDialogCommands(commands);

            var result = DialogService.ShowDialog(
                dialogCommands: commands,
                title: this.FullTitle,
                viewModel: this
            );
            return result != null ? (MessageBoxResult)result.Id : MessageBoxResult.None;
        }

        private void OnDialogApplyCommandtExecuting(CancelEventArgs parameter)
        {
            if (Validate())
            {
                ApplyCommandExecuted();
            }
            else
            {
                MessageService.Show(messageBoxText: String.Format("Невозможно применить изменения, т.к. форма содержит следующие ошибки:{0}{1}{0}Устраните ошибки и повторите попытку.", Environment.NewLine, Error),
                    caption: "Ошибка сохранения данных",
                    button: MessageBoxButton.OK);
                parameter.Cancel = true;
            }
        }
        private void OnDialogCancelCommandtExecuting(CancelEventArgs parameter)
        {
            CancelCommandExecuted();
        }
            
        public virtual bool Validate()
        {
            var validationProperties = AttributeUtils.GetProperties<ValidateAttribute>(this.GetType());
            var sb = new StringBuilder();
            foreach(var itm in validationProperties)
            {
                sb.Append(this[itm.Name]);
            };
            Error = sb.ToString();
            return String.IsNullOrWhiteSpace(Error);
        }
        
        protected virtual void ApplyCommandExecuted()
        {
        }
        protected virtual void CancelCommandExecuted()
        {
        }
        protected virtual void AllowSaveChanged()
        {
            RaisePropertiesChanged("AllowSave");
        }
        protected virtual void ExtendDialogCommands(List<UICommand> commands)
        {
        }

        public Models.User CurrentUser
        {
            get { return Services.UserProfileService.CurrentUser; }
            set
            {
                Services.UserProfileService.CurrentUser = value;
            }
        }
    }
}
