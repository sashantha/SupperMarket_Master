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

        public delegate void ItemEditorSynchronizeHandler();

        public event ItemEditorSynchronizeHandler SyncEditro;

        private Item _selectedItem;
        public Item SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        private bool _isNew;
        public bool IsNew
        {
            get { return _isNew; }
            set { SetProperty(ref _isNew, value); }
        }

        private ItemGroup _selectedItemGroup;
        public ItemGroup SelectedItemGroup
        {
            get { return _selectedItemGroup; }
            set { SetProperty(ref _selectedItemGroup, value); }
        }

        private ItemSubGroup _selectedItemSubGroup;
        public ItemSubGroup SelectedItemSubGroup
        {
            get { return _selectedItemSubGroup; }
            set { SetProperty(ref _selectedItemSubGroup, value); }
        }

        private StoreInfor _itemStore;
        public StoreInfor ItemStore
        {
            get { return _itemStore; }
            set { SetProperty(ref _itemStore, value); }
        }

        private ObservableCollection<Item> _sourceItemGroups;
        public ObservableCollection<Item> SourceItemGroups
        {
            get { return _sourceItemGroups; }
            set { SetProperty(ref _sourceItemGroups, value); }
        }

        private ObservableCollection<ItemGroup> _itemGroups;
        public ObservableCollection<ItemGroup> ItemGroups
        {
            get { return _itemGroups; }
            set { SetProperty(ref _itemGroups, value); }
        }

        private ObservableCollection<ItemSubGroup> _itemSubGroups;
        public ObservableCollection<ItemSubGroup> ItemSubGroups
        {
            get { return _itemSubGroups; }
            set { SetProperty(ref _itemSubGroups, value); }
        }
        #endregion

        #region Commands

        private DelegateCommand _languageSwapCommand;
        public DelegateCommand LanguageSwapCommand
        {
            get { return _languageSwapCommand; }
            set { SetProperty(ref _languageSwapCommand, value); }
        }


        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand
        {
            get { return _saveCommand; }
            set { SetProperty(ref _saveCommand, value); }
        }

        private DelegateCommand _newCommand;
        public DelegateCommand NewCommand
        {
            get { return _newCommand; }
            set { SetProperty(ref _newCommand, value); }
        }

        private DelegateCommand _updateCommand;
        public DelegateCommand UpdateCommand
        {
            get { return _updateCommand; }
            set { SetProperty(ref _updateCommand, value); }
        }

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
        internal void Initialize()
        {
            LoadAllItemGroups();
            LoadAllItemSubGroups();
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
        }

        private async void LoadAllItemGroups()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            ItemGroups = await ItemRestService.GetAllItemGroupAsync(mapper);
        }

        private async void LoadAllItemSubGroups()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            ItemSubGroups = await ItemRestService.GetAllItemSubGroupAsync(mapper);
        }

        private async void SaveCustomer()
        {            
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedItemGroup == null || SelectedItemSubGroup == null) 
            {
                _ = ShowMessageDialg("New Item Creation", "Can't Save Item Select Group and Sub Group", MsgDialogType.error);
                return;
            }
            SelectedItem.barcode = SelectedItem.code;
            SelectedItem.itemGroup = SelectedItemGroup;
            SelectedItem.itemSubGroup = SelectedItemSubGroup;
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
                        SyncEditro?.Invoke();
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
                    Item i = await ItemRestService.UpdateItemAsync(mapper, SelectedItem);
                    if (i.id > 0)
                    {
                        StoreInfor s = await ItemRestService.UpdateStoreInforAsync(mapper, ItemStore);
                        if (s.id > 0)
                        {
                            _ = ShowMessageDialg("Item Update", "Item Updated Successfully", MsgDialogType.infor);
                            SyncEditro?.Invoke();
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
                if (r.Result == ButtonResult.OK)
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
