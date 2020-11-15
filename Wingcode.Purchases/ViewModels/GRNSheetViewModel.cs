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

        public SupplierCreditInvoice CreditInvoice { get; set; }

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

        public StoreInfor SelectedStore { get; set; }

        public UnitOfMeasurement SelectedUom { get; set; }

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

        public DelegateCommand CalculateCommand { get; set; }

        public DelegateCommand SavePurchaseItemCommand { get; set; }

        public DelegateCommand SelectionCommand { get; set; }

        public DelegateCommand CalTotalDiscountCommand { get; set; }

        public DelegateCommand CashPayEnterCommand { get; set; }

        public DelegateCommand ChequePayEnterCommand { get; set; }

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
            CalculateCommand = new DelegateCommand(CalculatePurchaseItem);
            SavePurchaseItemCommand = new DelegateCommand(SavePurchaseItem);
            SelectionCommand = new DelegateCommand(SelectionChanged);
            EndPurchaseCommand = new DelegateCommand(EndPurchase);
            CalTotalDiscountCommand = new DelegateCommand(CalTotalDiscount);
            CashPayEnterCommand = new DelegateCommand(CashEnter);
            ChequePayEnterCommand = new DelegateCommand(ChequePayEnter);
        }

        private void ChequePayEnter()
        {
            if (CurrenPurchase.chqAmount > 0.00m)
            {
                if (CurrenPurchase.chqAmount == CurrenPurchase.netAmount)
                {
                    CurrenPurchase.invoiceType = ConstValues.INVT_PAID;
                    CurrenPurchase.payMethod = ConstValues.INVT_CHEQUE;
                    CurrenPurchase.creditAmount = CurrenPurchase.netAmount - (CurrenPurchase.payAmount + CurrenPurchase.chqAmount);
                    CurrenChqueBook = CurrenChqueBook.CreateNewChequeBook(branch, loggedUser.LoggedUser, CurrenPurchase);
                    aggregator.GetEvent<UIElementFocusEvent>().Publish("chq");
                }
                else if (CurrenPurchase.chqAmount < CurrenPurchase.netAmount)
                {
                    if (CurrenPurchase.chqAmount == CurrenPurchase.creditAmount)
                    {
                        CurrenPurchase.invoiceType = ConstValues.INVT_PAID;
                        if ((CurrenPurchase.payAmount + CurrenPurchase.chqAmount) == CurrenPurchase.netAmount)
                            CurrenPurchase.payMethod = ConstValues.INVT_CASH_CHEQUE;
                        else
                            CurrenPurchase.payMethod = ConstValues.INVT_CHEQUE;
                        CurrenPurchase.creditAmount = CurrenPurchase.netAmount - (CurrenPurchase.payAmount + CurrenPurchase.chqAmount);
                        CurrenChqueBook = CurrenChqueBook.CreateNewChequeBook(branch, loggedUser.LoggedUser, CurrenPurchase);
                        aggregator.GetEvent<UIElementFocusEvent>().Publish("chq");
                    }
                    else
                    {
                        CurrenPurchase.creditAmount = CurrenPurchase.netAmount - (CurrenPurchase.payAmount + CurrenPurchase.chqAmount);
                        CurrenPurchase.invoiceType = ConstValues.INVT_CREDIT;
                        CurrenPurchase.payMethod = ConstValues.INVT_CHEQUE;
                        CurrenChqueBook = CurrenChqueBook.CreateNewChequeBook(branch, loggedUser.LoggedUser, CurrenPurchase);
                        aggregator.GetEvent<UIElementFocusEvent>().Publish("chq");
                    }
                }
                else
                {
                    ShowMessageDialg("Purchasing", "Invoice Cheque Amount Greater Than to Net Amount", MsgDialogType.warrning);
                    aggregator.GetEvent<UIElementFocusEvent>().Publish("chqa");
                }
            }
            else
            {
                CurrenPurchase.creditAmount = CurrenPurchase.netAmount - (CurrenPurchase.payAmount + CurrenPurchase.chqAmount);
                CurrenPurchase.invoiceType = ConstValues.INVT_CREDIT;
                CurrenPurchase.payMethod = ConstValues.INVT_CREDIT;
                aggregator.GetEvent<UIElementFocusEvent>().Publish("sav");
            }
        }

        private void CashEnter()
        {
            if (CurrenPurchase.payAmount > 0.00m)
            {
                if (CurrenPurchase.payAmount == CurrenPurchase.netAmount)
                {
                    CurrenPurchase.invoiceType = ConstValues.INVT_PAID;
                    CurrenPurchase.payMethod = ConstValues.INVT_CASH;
                    aggregator.GetEvent<UIElementFocusEvent>().Publish("sav");
                }
                else if (CurrenPurchase.payAmount < CurrenPurchase.netAmount)
                {
                    CurrenPurchase.creditAmount = CurrenPurchase.netAmount - CurrenPurchase.payAmount;
                    CurrenPurchase.chqAmount = CurrenPurchase.creditAmount;
                    aggregator.GetEvent<UIElementFocusEvent>().Publish("chqa");
                }
                else
                {
                    ShowMessageDialg("Purchasing", "Invoice Pay Amount Greater Than to Net Amount", MsgDialogType.warrning);
                    aggregator.GetEvent<UIElementFocusEvent>().Publish("cah");
                }
            }
            else
            {
                CurrenPurchase.chqAmount = CurrenPurchase.netAmount;
                aggregator.GetEvent<UIElementFocusEvent>().Publish("chqa");
            }
        }

        private void CalTotalDiscount()
        {
            CurrenPurchase.CalaculatePurchase(PurchaseItems);
            if (CurrenPurchase.discountPercent > 0.00m)
            {
                if (CurrenPurchase.netAmount == 0.00m)
                {

                }
            }
            else
            {
                CurrenPurchase.netAmount = CurrenPurchase.invoiceAmount;
                CurrenPurchase.payAmount = CurrenPurchase.netAmount;
            }
            aggregator.GetEvent<UIElementFocusEvent>().Publish("cah");
        }

        private async void EndPurchase()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            CurrenPurchase.recordState = ConstValues.RCS_FINE;
            _ = await PurchaseRestService.UpdatePurchaseAsync(mapper, CurrenPurchase);
            if (CurrenPurchase.creditAmount > 0.00m)
            {
                CurrenPurchase.invoiceType = ConstValues.INVT_CREDIT;
                CreditInvoice = CreditInvoice.CreateNewCreditInvoice(branch, loggedUser.LoggedUser, CurrenPurchase);
                SelectedSupplier.supplierCreditAccount.totalCredit += CurrenPurchase.creditAmount;
                _ = await SupplierCreditRestService.updateSupplierCreditAccountAsync(mapper, SelectedSupplier.supplierCreditAccount);
                _ = await SupplierCreditRestService.CreateSupplierCreditInvoiceAsync(mapper, CreditInvoice);

            }
            if (CurrenPurchase.payAmount > 0.00m)
            {
                CurrenCashBook = CurrenCashBook.CreateNewCashBook(branch, loggedUser.LoggedUser, CurrenPurchase);
                CurrenCashBook.branchAccount = SelectedBac;
                _ = await FinancialRestService.CreateCashBookAsync(mapper, CurrenCashBook);
            }
            if (CurrenPurchase.chqAmount > 0.00m)
            {
                CurrenChqueBook = CurrenChqueBook.CreateNewChequeBook(branch, loggedUser.LoggedUser, CurrenPurchase);
                CurrenChqueBook.branchAccount = SelectedBac;
                _ = await FinancialRestService.CreateChequeBookAsync(mapper, CurrenChqueBook);
            }
            SelectedStore = null;
            SelectedUom = null;
            SelectedItem = null;
            SelectedItmCt = null;
            CurrentPurchaseItem = null;
            CurrenPurchase = null;
            SelectedSupCt = null;
            SelectedSupplier = null;
            CreditInvoice = null;
            CurrenCashBook = null;
            CurrenChqueBook = null;
            PurchaseItems.Clear();
            Initialize();
        }

        private void SelectionChanged()
        {
            if (CurrentPurchaseItem != null)
            {
                SelectedItmCt = CurrentPurchaseItem.item.ToItemCriteria();
                SelectedItem = CurrentPurchaseItem.item;
                aggregator.GetEvent<UIElementFocusEvent>().Publish("itm");
            }
        }

        private async void SavePurchaseItem()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            CurrentPurchaseItem.recordState = ConstValues.RCS_FINE;
            CurrentPurchaseItem.purchaseType = GetPurchaseType();
            decimal rq = (CurrentPurchaseItem.reorderLevel * SelectedUom.baseRatioToPurchase * SelectedUom.purchaseQuantifyValue);
            PurchaseItem pi = PurchaseItems.Where(i => i.item.id == SelectedItem.id).FirstOrDefault();
            if (pi != null)
            {
                SelectedStore.reorderLevel += rq;
                CurrentPurchaseItem.reorderLevel = rq;
                CurrentPurchaseItem = CurrentPurchaseItem.Merge(pi);
                _ = await PurchaseRestService.UpdatePurchaseItemAsync(mapper, CurrentPurchaseItem);
                _ = await ItemRestService.UpdateStoreInforAsync(mapper, SelectedStore);
                PurchaseItem np = CurrentPurchaseItem.CloneObject();
                PurchaseItems.Remove(pi);
                PurchaseItems.Add(np);
                SelectedStore = null;
                SelectedUom = null;
                SelectedItem = null;
                SelectedItmCt = null;
                CurrentPurchaseItem = null;
            }
            else
            {
                CurrentPurchaseItem.reorderLevel = rq;
                SelectedStore.reorderLevel = rq;
                CurrentPurchaseItem = await PurchaseRestService.CreatePurchaseItemAsync(mapper, CurrentPurchaseItem);
                if (CurrentPurchaseItem.id > 0)
                {
                    _ = await ItemRestService.UpdateStoreInforAsync(mapper, SelectedStore);
                    PurchaseItems.Add(CurrentPurchaseItem.CloneObject());
                    SelectedStore = null;
                    SelectedUom = null;
                    SelectedItem = null;
                    SelectedItmCt = null;
                    CurrentPurchaseItem = null;
                }
                else
                {
                    _ = ShowMessageDialg("Saving Purchase Item", "Can't Savae Purchase Item, found Some problems.", MsgDialogType.error);
                }
            }
            CurrenPurchase.CalaculatePurchase(PurchaseItems);
            _ = await PurchaseRestService.UpdatePurchaseAsync(mapper, CurrenPurchase);
        }

        private async void CalculatePurchaseItem()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            SelectedStore = await ItemRestService.GetStoreInforByItemIdAndBranchAsync(mapper, SelectedItem.id, branch.id);
            SelectedUom = SelectedItem.unitOfMeasurement;
            decimal pq = (CurrentPurchaseItem.quantity * SelectedUom.baseRatioToPurchase * SelectedUom.purchaseQuantifyValue);
            decimal fq = (CurrentPurchaseItem.freeQuantity * SelectedUom.baseRatioToPurchase * SelectedUom.purchaseQuantifyValue);
            decimal pc = (CurrentPurchaseItem.cost / (SelectedUom.baseRatioToPurchase * SelectedUom.purchaseQuantifyValue));
            CurrentPurchaseItem.cost = pc;
            CurrentPurchaseItem.quantity = pq;
            CurrentPurchaseItem.freeQuantity = fq;
            CurrentPurchaseItem.realQuantity = pq + fq;
            CurrentPurchaseItem.availableQuantity = pq + fq;
            CurrentPurchaseItem.amount = (CurrentPurchaseItem.realQuantity * CurrentPurchaseItem.cost);
            SelectedStore.cost = pc;
            SelectedStore.retailPrice = CurrentPurchaseItem.retailPrice;
            SelectedStore.wholesalePrice = CurrentPurchaseItem.wholesalePrice;
            SelectedStore.availableQuantity += pq + fq;
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
            if (CurrentPurchaseItem == null)
            {
                CurrentPurchaseItem = CurrentPurchaseItem.CreateNew(CurrenPurchase, SelectedItem);
            }
            else
            {
                CurrentPurchaseItem.item = SelectedItem;
            }
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
