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
    public class ItemSubGroupEditorViewModel : BaseViewModel
    {

        #region Property

        private IDialogService dialogService;
        private IContainerExtension containerExtension;
        private IEventAggregator aggregator;
        private ILoggedUserProvider loggedUser;


        public ItemSubGroup SelectedItemGroup { get; set; }

        public ObservableCollection<ItemSubGroup> SourceItemGroups { get; set; }


        #endregion

        #region Commands

        public DelegateCommand SaveCommand { get; set; }

        public DelegateCommand NewCommand { get; set; }

        public DelegateCommand UpdateCommand { get; set; }

        #endregion

        #region Constructor

        public ItemSubGroupEditorViewModel(IContainerExtension extension, ILoggedUserProvider provider)
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
            LoadAllItemSubGroups();
            NewCustomer();
        }

        private async void LoadAllItemSubGroups()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            SourceItemGroups = await ItemRestService.GetAllItemSubGroupAsync(mapper);
        }

        private async void SaveCustomer()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (!string.IsNullOrEmpty(SelectedItemGroup.subGroupName))
            {
                ItemSubGroup g = await ItemRestService.CreateItemSubGroupAsync(mapper, SelectedItemGroup);
                if (g.id > 0)
                {
                    dialogService.ShowMsgDialog("New Item Group Creation", "Item Group Created Successfully", MsgDialogType.infor, (r) =>
                    {
                        LoadAllItemSubGroups();
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
                    ItemSubGroup g = await ItemRestService.UpdateItemSubGroupAsync(mapper, SelectedItemGroup);
                    if (g.id > 0)
                    {
                        dialogService.ShowMsgDialog("Item Group Update", "Item Group Update Successfully", MsgDialogType.infor, (r) =>
                        {
                            LoadAllItemSubGroups();
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
            SelectedItemGroup = new ItemSubGroup()
            {
                id = 0,
                subGroupName = string.Empty
            };
        }

        #endregion
    }
}
