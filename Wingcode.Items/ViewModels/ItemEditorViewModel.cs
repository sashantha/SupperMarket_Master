using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base.DataModel;
using Wingcode.Base.Event;
using Wingcode.Base.Extensions;
using Wingcode.Base.Input;
using Wingcode.Controls;
using Wingcode.Data.Rest.Model;
using Wingcode.Data.Rest.Service;
using Wingcode.Items.Event;
using Wingcode.Items.Extensions;

namespace Wingcode.Items.ViewModels
{
    public class ItemEditorViewModel : BaseViewModel
    {
        #region Property

        private IDialogService dialogService;
        private IContainerExtension containerExtension;
        private IEventAggregator aggregator;
        private ILoggedUserProvider loggedUser;


        public Item SelectedItem { get; set; }

        public bool IsNew { get; set; }

        public ItemGroup SelectedItemGroup { get; set; }

        public ItemSubGroup SelectedItemSubGroup { get; set; }

        public UnitOfMeasurement SelectedUom { get; set; }

        public StoreInfor ItemStore { get; set; }

        public ObservableCollection<Item> SourceItemGroups { get; set; }

        public ObservableCollection<ItemGroup> ItemGroups { get; set; }

        public ObservableCollection<ItemSubGroup> ItemSubGroups { get; set; }

        public ISuggestionProvider UomSugges { get; set; }
        #endregion

        #region Commands

        public DelegateCommand LanguageSwapCommand { get; set; }

        public DelegateCommand SaveCommand { get; set; }

        public DelegateCommand NewCommand { get; set; }

        public DelegateCommand UpdateCommand { get; set; }

        #endregion

        #region Constructor

        public ItemEditorViewModel(IContainerExtension extension, ILoggedUserProvider provider)
        {
            containerExtension = extension;
            loggedUser = provider;
            SaveCommand = new DelegateCommand(SaveCustomer);
            NewCommand = new DelegateCommand(NewCustomer);
            UpdateCommand = new DelegateCommand(UpdateCustomer);
            LanguageSwapCommand = new DelegateCommand(SwapLang);
            dialogService = containerExtension.Resolve<IDialogService>();
            aggregator = containerExtension.Resolve<IEventAggregator>();
        }

        #endregion

        #region Functions
        internal async void Initialize()
        {
            LoadAll();
            await Task.Delay(500);
            if (IsNew)
                NewCustomer();
            else
                FindStoreInfor();
        }

        private async void FindStoreInfor()
        {
            if (SelectedItem.id <= 0)
                return;
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            ItemStore = await ItemRestService.GetStoreInforByItemIdAndBranchAsync(mapper, SelectedItem.id, loggedUser.LoggedUser.branch.id);
            aggregator.GetEvent<ItemGroupSelectionEvent>().Publish(SelectedItem.itemGroup);
            aggregator.GetEvent<ItemSubGroupSelectionEvent>().Publish(SelectedItem.itemSubGroup);
            //aggregator.GetEvent<UnitOfMeasureSelectionEvent>().Publish(SelectedItem.unitOfMeasurement);
            SelectedUom = SelectedItem.unitOfMeasurement;
        }

        private async void LoadAll()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            ItemGroups = await ItemRestService.GetAllItemGroupAsync(mapper);
            ItemSubGroups = await ItemRestService.GetAllItemSubGroupAsync(mapper);
            ObservableCollection<UnitOfMeasurement>  ums = await ItemRestService.GetAllUnitOfMeasurementAsync(mapper);
            UomSugges = new UomSuggestionProvider() { uomSource = ums};
        }
              

        private async void SaveCustomer()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedItemGroup == null || SelectedItemSubGroup == null || SelectedUom == null) 
            {
                _ = ShowMessageDialg("New Item Creation", "Can't Save Item Select Group and Sub Group", MsgDialogType.error);
                return;
            }
            SelectedItem.barcode = SelectedItem.code;
            SelectedItem.itemGroup = SelectedItemGroup;
            SelectedItem.itemSubGroup = SelectedItemSubGroup;
            SelectedItem.unitOfMeasurement = SelectedUom;
            if (SelectedItem.IsValiedItem())
            {
                Item i = await ItemRestService.CreateItemAsync(mapper, SelectedItem);
                if (i.id > 0)
                {
                    ItemStore.branch = loggedUser.LoggedUser.branch;
                    ItemStore.item = i;
                    StoreInfor s = await ItemRestService.CreateStoreInforAsync(mapper, ItemStore);
                    if (s.id > 0)
                    {
                        _ = ShowMessageDialg("New Item Creation", "Item Created Successfully", MsgDialogType.infor);
                        RizeSyncEvent();
                        Initialize();
                    }
                    else
                    {
                        _ = ShowMessageDialg("New Item Creation", "Can't Save Item Store", MsgDialogType.error);
                        return;
                    }
                }
                else
                {
                    _ = ShowMessageDialg("New Item Creation", "Can't Save Item", MsgDialogType.error);
                    return;
                }
            }
            else
            {
                _ = ShowMessageDialg("New Item Creation", "Invalid Item Details or Already Exist Item", MsgDialogType.warrning);
                return;
            }
        }

        private async void UpdateCustomer()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedItem != null)
            {
                if (SelectedItem.id > 0)
                { 
                    if (SelectedItem.unitOfMeasurement.id != SelectedUom.id)
                        SelectedItem.unitOfMeasurement = SelectedUom;

                    Item i = await ItemRestService.UpdateItemAsync(mapper, SelectedItem);
                    if (i.id > 0)
                    {
                        StoreInfor s = await ItemRestService.UpdateStoreInforAsync(mapper, ItemStore);
                        if (s.id > 0)
                        {
                            _ = ShowMessageDialg("Item Update", "Item Updated Successfully", MsgDialogType.infor);
                            RizeSyncEvent();
                            Initialize();
                        }
                        else
                        {
                            _ = ShowMessageDialg("Item Update", "Can't Update Item Store", MsgDialogType.error);
                            return;
                        }
                    }
                    else
                    {
                        _ = ShowMessageDialg("Item Update", "Can't Save Item", MsgDialogType.error);
                        return;
                    }
                }
                else
                {
                    _ = ShowMessageDialg("Item Update", "Please Select Item Before Update", MsgDialogType.warrning);
                    return;
                }
            }
            else
            {
                _ = ShowMessageDialg("Item Update", "Please Select Item Before Update", MsgDialogType.warrning);
                return;
            }
        }

        private async void NewCustomer()
        {
            aggregator.GetEvent<UIElementFocusEvent>().Publish("");
            await Task.Delay(500);
            SelectedItem = SelectedItem.CreateNewItem();
            ItemStore = ItemStore.CreateNewStore();
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

        private void SwapLang()
        {
            InputLanguageController.SwapInputLanguage();
        }

        #endregion
    }
}
