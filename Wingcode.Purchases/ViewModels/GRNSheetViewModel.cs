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

        private bool _isCode;

        public bool IsCode
        {
            get { return _isCode; }
            set { SetProperty(ref _isCode, value); }
        }

        private bool _isBarcode;

        public bool IsBarcode
        {
            get { return _isBarcode; }
            set { SetProperty(ref _isBarcode, value); }
        }

        private bool _isName;

        public bool IsName
        {
            get { return _isName; }
            set { SetProperty(ref _isName, value); }
        }

        private bool _isPurchase;

        public bool IsPurchase
        {
            get { return _isPurchase; }
            set { SetProperty(ref _isPurchase, value); }
        }

        private bool _isFree;

        public bool IsFree
        {
            get { return _isFree; }
            set { SetProperty(ref _isFree, value); }
        }

        private bool _isNew;

        public bool IsNew
        {
            get { return _isNew; }
            set { SetProperty(ref _isNew, value); }
        }

        private string _displayMem;

        public string DisplayMem
        {
            get { return _displayMem; }
            set { SetProperty(ref _displayMem, value); }
        }


        private ISuggestionProvider _supSugges;
        public ISuggestionProvider SupSugges
        {
            get { return _supSugges; }
            set { SetProperty(ref _supSugges, value); }
        }

        private ISuggestionProvider _itmSugges;
        public ISuggestionProvider ItmSugges
        {
            get { return _itmSugges; }
            set { SetProperty(ref _itmSugges, value); }
        }

        private ISuggestionProvider _bacSugges;
        public ISuggestionProvider BacSugges
        {
            get { return _bacSugges; }
            set { SetProperty(ref _bacSugges, value); }
        }

        private SupplierCriteria selectedSupct;

        public SupplierCriteria SelectedSupCt
        {
            get { return selectedSupct; }
            set { SetProperty(ref selectedSupct, value); }
        }

        private ItemCriteria selectedItmCt;

        public ItemCriteria SelectedItmCt
        {
            get { return selectedItmCt; }
            set { SetProperty(ref selectedItmCt, value); }
        }

        private BranchAccount selectedBac;

        public BranchAccount SelectedBac
        {
            get { return selectedBac; }
            set { SetProperty(ref selectedBac, value); }
        }

        private Purchase currenPurchase;

        public Purchase CurrenPurchase
        {
            get { return currenPurchase; }
            set { SetProperty(ref currenPurchase, value); }
        }

        private CashBook currenCashBook;

        public CashBook CurrenCashBook
        {
            get { return currenCashBook; }
            set { SetProperty(ref currenCashBook, value); }
        }

        private ChequeBook currenChqueBook;

        public ChequeBook CurrenChqueBook
        {
            get { return currenChqueBook; }
            set { SetProperty(ref currenChqueBook, value); }
        }

        private Supplier selectedSupplier;

        public Supplier SelectedSupplier
        {
            get { return selectedSupplier; }
            set { SetProperty(ref selectedSupplier, value); }
        }

        private Item selectedItem;

        public Item SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

        private PurchaseItem currentPurchaseItem;

        public PurchaseItem CurrentPurchaseItem
        {
            get { return currentPurchaseItem; }
            set { SetProperty(ref currentPurchaseItem, value); }
        }

        private ObservableCollection<PurchaseItem> purchaseItems;

        public ObservableCollection<PurchaseItem> PurchaseItems
        {
            get { return purchaseItems; }
            set { SetProperty(ref purchaseItems, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand<string> _optionChangeCommand;

        public DelegateCommand<string> OptionChangeCommand
        {
            get { return _optionChangeCommand; }
            set { SetProperty(ref _optionChangeCommand, value); }
        }

        private DelegateCommand<string> _purchaseoptionCommand;

        public DelegateCommand<string> PurchaseoptionCommand
        {
            get { return _purchaseoptionCommand; }
            set { SetProperty(ref _purchaseoptionCommand, value); }
        }
        

        private DelegateCommand searchItemCommand;

        public DelegateCommand SearchItemCommand
        {
            get { return searchItemCommand; }
            set { SetProperty(ref searchItemCommand, value); }
        }

        private DelegateCommand searchSupplierCommand;

        public DelegateCommand SearchSupplierCommand
        {
            get { return searchSupplierCommand; }
            set { SetProperty(ref searchSupplierCommand, value); }
        }

        private DelegateCommand searchOldCommand;

        public DelegateCommand SearchOldCommand
        {
            get { return searchOldCommand; }
            set { SetProperty(ref searchOldCommand, value); }
        }

        private DelegateCommand endPurchaseCommand;

        public DelegateCommand EndPurchaseCommand
        {
            get { return endPurchaseCommand; }
            set { SetProperty(ref endPurchaseCommand, value); }
        }

        private DelegateCommand createPurchaseCommand;

        public DelegateCommand CreatePurchaseCommand
        {
            get { return createPurchaseCommand; }
            set { SetProperty(ref createPurchaseCommand, value); }
        }
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
        }

        private async void CreatePurchase()
        {
            
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
        }

        private void SearchOldPurChase()
        {
            string inv = CurrenPurchase.invoiceNo;
            aggregator.GetEvent<UIElementFocusEvent>().Publish("sup");
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
        #endregion
    }
}
