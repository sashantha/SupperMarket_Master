using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base;
using Wingcode.Base.DataModel;
using Wingcode.Base.Event;
using Wingcode.Base.Extensions;
using Wingcode.Controls;
using Wingcode.Data.Rest.Model;
using Wingcode.Data.Rest.Service;
using Wingcode.Purchases.Extensions;

namespace Wingcode.Purchases.ViewModels
{
    public class GRNSheetViewModel : BaseViewModel
    {

        #region Property

        private IDialogService dialogService;
        private IContainerExtension containerExtension;
        private IEventAggregator aggregator;
        private ILoggedUserProvider loggedUser;
        private Branch branch;

        public bool IsCode { get; set; }

        public bool IsBarcode { get; set; }

        public bool IsName { get; set; }

        public bool IsPurchase { get; set; }

        public bool IsFree { get; set; }

        public bool IsNew { get; set; }

        public string DisplayMem { get; set; }

        public ISuggestionProvider SupSugges { get; set; }

        public ISuggestionProvider ItmSugges { get; set; }

        public ISuggestionProvider BacSugges { get; set; }

        public SupplierCriteria SelectedSupCt { get; set; }

        public ItemCriteria SelectedItmCt { get; set; }

        public BranchAccount SelectedBac { get; set; }

        public Purchase CurrenPurchase { get; set; }

        public CashBook CurrenCashBook { get; set; }

        public ChequeBook CurrenChqueBook { get; set; }

        public Supplier SelectedSupplier { get; set; }

        public Item SelectedItem { get; set; }

        public PurchaseItem CurrentPurchaseItem { get; set; }

        public ObservableCollection<PurchaseItem> PurchaseItems { get; set; }

        #endregion

        #region Commands

        public DelegateCommand<string> OptionChangeCommand { get; set; }

        public DelegateCommand<string> PurchaseoptionCommand { get; set; }

        public DelegateCommand SearchItemCommand { get; set; }

        public DelegateCommand SearchSupplierCommand { get; set; }

        public DelegateCommand SearchOldCommand { get; set; }

        public DelegateCommand EndPurchaseCommand { get; set; }

        public DelegateCommand CreatePurchaseCommand { get; set; }

        public DelegateCommand CalDiscountCommand { get; set; }

        #endregion

        #region Constructor

        public GRNSheetViewModel(IContainerExtension container)
        {
            containerExtension = container;
            loggedUser = containerExtension.Resolve<ILoggedUserProvider>();
            dialogService = containerExtension.Resolve<IDialogService>();
            aggregator = containerExtension.Resolve<IEventAggregator>();
            branch = loggedUser.LoggedUser.branch;
            CreateCommands();
            InitiOptions();
        }

        #endregion

        #region Function and Events

        internal void Initialize()
        {
            LoadAutoData();
            NewPurchase();
        }

        private void CreateCommands()
        {
            SearchItemCommand = new DelegateCommand(SearchItem);
            SearchSupplierCommand = new DelegateCommand(SearchSupplier);
            SearchOldCommand = new DelegateCommand(SearchOldPurChase);
            OptionChangeCommand = new DelegateCommand<string>(OptionChange);
            PurchaseoptionCommand = new DelegateCommand<string>(PurcuaseOptionChange);
            CreatePurchaseCommand = new DelegateCommand(CreatePurchase);
            CalDiscountCommand = new DelegateCommand(CalWithDiscount);
        }

        private void CalWithDiscount()
        {
            CurrentPurchaseItem.CalculateCostWithDiscount();
        }

        private async void CreatePurchase()
        {
            if (IsNew)
            {
                IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
                CurrenPurchase.branch = branch;
                CurrenPurchase.supplier = SelectedSupplier;
                CurrenPurchase.user = loggedUser.LoggedUser;
                CurrenPurchase.payMethod = ConstValues.INVT_CASH;
                CurrenPurchase = await PurchaseRestService.CreatePurchaseAsync(mapper, CurrenPurchase);
                if (CurrenPurchase.id == 0)
                {
                    if (ShowMessageDialg("Purchase Creating", "Found some problem, Can't Create Purchase", MsgDialogType.error))
                    {
                        Initialize();
                        return;
                    }
                }

            }
        }

        private async void SearchSupplier()
        {
            if (SelectedSupCt == null)
                return;
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            SelectedSupplier = await SupplierRestService.GetSupplierByIdAndBranchIdAsync(mapper, SelectedSupCt.id, branch.id);
        }

        private async void SearchItem()
        {
            if (SelectedItmCt == null)
                return;
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            SelectedItem = await ItemRestService.GetItemByParamAsync(mapper, ItemRestService.ITEM_CODE_FLAG, SelectedItmCt.code);
            CurrentPurchaseItem = CurrentPurchaseItem.CreateNew(CurrenPurchase, SelectedItem);
        }

        private async void SearchOldPurChase()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            Purchase p = await PurchaseRestService.GetAllPurchaseByInvoiceNoAndRecordStAsync(mapper, branch.id, CurrenPurchase.invoiceNo, ConstValues.RCS_NEW);
            if (p != null)
            {
                string msg = "Found Exist Purchase Invoice, Do you want to Continue it?";
                if (ShowMessageDialg("Find Exist Purchase", msg, MsgDialogButtonType.YesNo, MsgDialogType.Confirmation))
                {
                    CurrenPurchase = p;
                    IsNew = false;
                    SelectedSupCt = CurrenPurchase.supplier.ToSupplierCriterita();
                    SelectedSupplier = CurrenPurchase.supplier;
                    PurchaseItems = await PurchaseRestService.GetAllPurchaseItemByParamAsync(mapper, branch.id, PurchaseRestService.PI_PID_FLAG, p.id);
                    aggregator.GetEvent<UIElementFocusEvent>().Publish("pdt");
                }
                else
                {
                    CurrenPurchase.invoiceNo = string.Empty;
                    aggregator.GetEvent<UIElementFocusEvent>().Publish("inv");
                }
            }
            else
            {
                aggregator.GetEvent<UIElementFocusEvent>().Publish("sup");
            }
        }

        private void InitiOptions()
        {
            DisplayMem = "code";
            IsCode = true;
            IsName = false;
            IsBarcode = false;
            IsPurchase = true;
            IsFree = false;
        }

        private void PurcuaseOptionChange(string obj)
        {
            if (obj.Equals(ConstValues.PT_PURCHASE))
            {
                IsPurchase = true;
                IsFree = false;
            }
            else
            {
                IsPurchase = false;
                IsFree = true;
            }
        }

        private string GetPurchaseType()
        {
            if (IsPurchase && !IsFree)
                return ConstValues.PT_PURCHASE;
            else
                return ConstValues.PT_FREE;
        }

        private async void OptionChange(string obj)
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            ObservableCollection<ItemCriteria> its = new ObservableCollection<ItemCriteria>();
            switch (obj)
            {
                case "code":
                    DisplayMem = "code";
                    IsCode = true;
                    IsBarcode = false;
                    IsName = false;
                    its = await ItemRestService.GetItemAttribsByBranchIdAsync(mapper);
                    ItmSugges = new ItemSuggestionProvider() { itemCriterias = its, DisplayMember = DisplayMem };
                    break;
                case "barcode":
                    DisplayMem = "barcode";
                    IsBarcode = true;
                    IsCode = false;
                    IsName = false;
                    its = await ItemRestService.GetItemAttribsByBranchIdAsync(mapper);
                    ItmSugges = new ItemSuggestionProvider() { itemCriterias = its, DisplayMember = DisplayMem };
                    break;
                case "name":
                    DisplayMem = "name";
                    IsName = true;
                    IsBarcode = false;
                    IsCode = false;
                    its = await ItemRestService.GetItemAttribsByBranchIdAsync(mapper);
                    ItmSugges = new ItemSuggestionProvider() { itemCriterias = its, DisplayMember = DisplayMem };
                    break;
                default:
                    DisplayMem = "code";
                    IsCode = true;
                    IsBarcode = false;
                    IsName = false;
                    its = await ItemRestService.GetItemAttribsByBranchIdAsync(mapper);
                    ItmSugges = new ItemSuggestionProvider() { itemCriterias = its, DisplayMember = DisplayMem };
                    break;
            }
        }

        private async void LoadAutoData()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            ObservableCollection<SupplierCriteria> sus = await SupplierRestService.GetSupplierAttribsByBranchIdAsync(mapper, loggedUser.LoggedUser.branch.id);
            SupSugges = new SupplierSuggestionProvider() { supplierCriterias = sus };
            ObservableCollection<ItemCriteria> its = await ItemRestService.GetItemAttribsByBranchIdAsync(mapper);
            ItmSugges = new ItemSuggestionProvider() { itemCriterias = its, DisplayMember = DisplayMem };
            ObservableCollection<BranchAccount> bacs = await BranchRestService.GetAllBranchAccountAsync(mapper, loggedUser.LoggedUser.id);
            BacSugges = new BranchAccountSuggestionProvider() { brancgAcCriterias = bacs };
        }

        private async void NewPurchase()
        {
            IsNew = true;
            aggregator.GetEvent<UIElementFocusEvent>().Publish("inv");
            await Task.Delay(500);
            CurrenPurchase = CurrenPurchase.CreateNew();
            PurchaseItems = new ObservableCollection<PurchaseItem>();
        }

        private bool ShowMessageDialg(string title, string message, MsgDialogType dialogType)
        {
            bool result = false;
            dialogService.ShowMsgDialog(title, message, dialogType, (r) =>
            {
                if (r.Result == ButtonResult.OK || r.Result == ButtonResult.Yes)
                {
                    result = true;
                }
            });
            return result;
        }

        private bool ShowMessageDialg(string title, string message, MsgDialogButtonType dialogButtonType, MsgDialogType dialogType)
        {
            bool result = false;
            dialogService.ShowMsgDialog(title, message, dialogButtonType, dialogType, (r) =>
            {
                if (r.Result == ButtonResult.OK || r.Result == ButtonResult.Yes)
                {
                    result = true;
                }
            });
            return result;
        }

        #endregion
    }
}
