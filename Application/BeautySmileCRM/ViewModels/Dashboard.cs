using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BeautySmileCRM.Enums;
using DevExpress.Xpf.Mvvm;
using BeautySmileCRM.ViewModels.Base;
using DevExpress.Xpf.LayoutControl;
using SmartClasses.Utils;
using SmartClasses.Attributes;
using System.ComponentModel;

namespace BeautySmileCRM.ViewModels
{
    public class Dashboard : BaseNavigationViewModel, IDataErrorInfo
    {
        #region Validation
        public string Error { get; private set; }

        public string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                switch (columnName)
                {
                    case "SearchString":
                        if (!String.IsNullOrWhiteSpace(this.SearchString) && this.SearchString.Length < 3)
                        {
                            errorMessage = "Для поиска введите по крайней мере 3 символа фамилии, имени или номера дисконтной карты.";
                        };
                        break;
                };
                return errorMessage;
            }
        }

        private bool Validate()
        {
            var validationProperties = AttributeUtils.GetProperties<ValidateAttribute>(this.GetType());
            var sb = new StringBuilder();
            foreach (var itm in validationProperties)
            {
                sb.Append(this[itm.Name]);
            };
            Error = sb.ToString();
            return String.IsNullOrWhiteSpace(Error);
        }
        #endregion

        public ICommand OnNavigatingToClientView { get; private set; }
        public ICommand OnTileClick { get; private set; }

        private DeferredExecuter _findData;
        private IEnumerable<Models.CustomerDashboardData> _customers;
        private IEnumerable<Models.CustomerDashboardData> _data;
        private string _searchString;

        public IEnumerable<Models.CustomerDashboardData> Data
        {
            get { return _data; }
            set
            {
                if(_data != value)
                {
                    _data = value;
                    RaisePropertyChanged("Data");
                }
            }
        }
        [Validate()]
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                if(_searchString != value)
                {
                    _searchString = value;
                    RaisePropertyChanged("SearchString");
                    if(Validate())
                        _findData.PostponeExecution();
                }
            }
        }

        public Dashboard()
        {
            OnNavigatingToClientView = new DelegateCommand(OnNavigatingToClientViewExecuted);
            OnTileClick = new DelegateCommand<Tile>(OnTileClickExecuted);

            using(var dc = new Models.CRMContext())
            {
                _customers = dc.GetCustomerDashboardData(DateTime.Now).ToList();
            };
            
            FindData();
            
            _findData = new DeferredExecuter()
            {
                Delay = 1000,
                Action = () => FindData()
            };
        }

        private void OnNavigatingToClientViewExecuted()
        {
            NavigationService.Navigate("ClientView", null, this);
        }
        private void OnTileClickExecuted(Tile tile)
        {
            var customer = tile.Content as Models.CustomerDashboardData;
            if(customer != null)
                NavigationService.Navigate("ClientPageView", customer.CustomerID, this);
        }

        private void FindData()
        {
            if (CurrentUser.HasPrivilege(Privilege.ViewCustomer))
            {
                if (String.IsNullOrWhiteSpace(SearchString))
                {
                    Data = _customers;
                }
                else
                {
                    Data = _customers.Where(x => x.LastName.ToUpper().Contains(SearchString.ToUpper())
                            || x.FirstName.ToUpper().Contains(SearchString.ToUpper())
                            || (x.DiscountCardCode != null && x.DiscountCardCode.ToUpper().StartsWith(SearchString.ToUpper())));
                };
            };
        }
    }
}
