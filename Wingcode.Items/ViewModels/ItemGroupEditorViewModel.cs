﻿using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Wingcode.Base.DataModel;
using Wingcode.Base.Event;
using Wingcode.Base.Extensions;
using Wingcode.Data.Rest.Model;
using Wingcode.Data.Rest.Service;

namespace Wingcode.Items.ViewModels
{
    public class ItemGroupEditorViewModel : BaseViewModel
    {
        #region Property

        private IDialogService dialogService;
        private IContainerExtension containerExtension;
        private IEventAggregator aggregator;
        private ILoggedUserProvider loggedUser;


        private ItemGroup _selectedItemGroup;
        public ItemGroup SelectedItemGroup
        {
            get { return _selectedItemGroup; }
            set { SetProperty(ref _selectedItemGroup, value); }
        }

        private ObservableCollection<ItemGroup> _sourceItemGroups;
        public ObservableCollection<ItemGroup> SourceItemGroups
        {
            get { return _sourceItemGroups; }
            set { SetProperty(ref _sourceItemGroups, value); }
        }


        #endregion

        #region Commands

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

        public ItemGroupEditorViewModel(IContainerExtension extension, ILoggedUserProvider provider)
        {
            containerExtension = extension;
            loggedUser = provider;
            SaveCommand = new DelegateCommand(SaveCustomer);
            NewCommand = new DelegateCommand(NewCustomer);
            UpdateCommand = new DelegateCommand(UpdateCustomer);
            dialogService = containerExtension.Resolve<IDialogService>();
            aggregator = containerExtension.Resolve<IEventAggregator>();
        }

        #endregion

        #region Functions
        internal void Initialize()
        {
            LoadAllItemGroups();
            NewCustomer();
        }

        private async void LoadAllItemGroups() 
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            SourceItemGroups = await ItemRestService.GetAllItemGroupAsync(mapper);
        }

        private async void SaveCustomer()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (!string.IsNullOrEmpty(SelectedItemGroup.groupName))
            {
                ItemGroup g = await ItemRestService.CreateItemGroupAsync(mapper, SelectedItemGroup);
                if (g.id > 0)
                {
                    dialogService.ShowMsgDialog("New Item Group Creation", "Item Group Created Successfully", MsgDialogType.infor, (r) =>
                    {
                        LoadAllItemGroups();
                        NewCustomer();
                    });
                }
                else
                {
                    dialogService.ShowMsgDialog("New Item Group Creation", "Can't Save Item Group", MsgDialogType.error, (r) =>
                    {
                        return;
                    });

                }
            }
            else
            {
                dialogService.ShowMsgDialog("New Item Group Creation", "Invalid Item Group Details or Already Exist Customer", MsgDialogType.warrning, (r) =>
                {
                    return;
                });
            }
        }

        private async void UpdateCustomer()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedItemGroup != null)
            {
                if (SelectedItemGroup.id > 0)
                {
                    ItemGroup g = await ItemRestService.UpdateItemGroupAsync(mapper, SelectedItemGroup);
                    if (g.id > 0)
                    {
                        dialogService.ShowMsgDialog("Item Group Update", "Item Group Update Successfully", MsgDialogType.infor, (r) =>
                        {
                            LoadAllItemGroups();
                            NewCustomer();
                        });
                    }
                    else
                    {
                        dialogService.ShowMsgDialog("Item Group Update", "Can't Update Item Group", MsgDialogType.error, (r) =>
                        {
                            return;
                        });

                    }
                }
                else
                {
                    dialogService.ShowMsgDialog("Item Group Update", "Please Select Item Group Before Update", MsgDialogType.warrning, (r) =>
                    {
                        return;
                    });
                }
            }
            else
            {
                dialogService.ShowMsgDialog("Item Group Update", "Please Select Item Group Before Update", MsgDialogType.warrning, (r) =>
                {
                    return;
                });
            }
        }

        private async void NewCustomer()
        {
            aggregator.GetEvent<DataGridSelectionClearEvent>().Publish();
            aggregator.GetEvent<UIElementFocusEvent>().Publish("");
            await Task.Delay(1000);
            SelectedItemGroup = new ItemGroup()
            {
                id = 0,
                groupName = string.Empty
            };
        } 

        #endregion
    }
}
