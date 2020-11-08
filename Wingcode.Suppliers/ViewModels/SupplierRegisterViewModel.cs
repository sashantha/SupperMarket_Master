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
using Wingcode.Base.DataModel;
using Wingcode.Base.Event;
using Wingcode.Base.Extensions;
using Wingcode.Data.Rest.Model;
using Wingcode.Data.Rest.Service;

namespace Wingcode.Suppliers.ViewModels
{
    public class SupplierRegisterViewModel : BaseViewModel
    {

        #region Private Property

        private IDialogService dialogService;
        private IContainerExtension containerExtension;
        private IEventAggregator aggregator;
        private ILoggedUserProvider loggedUser;

        #endregion

        #region Public Full Property

        public string SearchText { get; set; }

        public Supplier SelectedSupplier { get; set; }

        public ObservableCollection<Supplier> SourceSuppliers { get; set; }

        #endregion

        #region Commands

        public DelegateCommand SearchCommand { get; set; }

        public DelegateCommand SaveCommand { get; set; }

        public DelegateCommand NewCommand { get; set; }

        public DelegateCommand UpdateCommand { get; set; }

        public DelegateCommand DeleteCommand { get; set; }

        #endregion

        #region Constructor

        public SupplierRegisterViewModel(IContainerExtension container, IDialogService dialog, IEventAggregator eventAggregator)
        {
            containerExtension = container;
            dialogService = dialog;
            aggregator = eventAggregator;
            SearchCommand = new DelegateCommand(SearchSupplier);
            SaveCommand = new DelegateCommand(SaveSupplier);
            NewCommand = new DelegateCommand(NewSupplier);
            UpdateCommand = new DelegateCommand(UpdateSupplier);
            DeleteCommand = new DelegateCommand(DeleteSupplier);
            loggedUser = containerExtension.Resolve<ILoggedUserProvider>();
        }

        #endregion

        #region Functions

        internal void Initialize()
        {
            LoadSuppliersAsync();
            NewSupplier();
        }


        private async void LoadSuppliersAsync()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            SourceSuppliers = await SupplierRestService.GetSupplierByBranchIdAsync(mapper, loggedUser.LoggedUser.branch.id);
        }

        private void SearchSupplier()
        {

        }

        private async void SaveSupplier()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (!string.IsNullOrEmpty(SelectedSupplier.name) && SelectedSupplier.id == 0)
            {
                SelectedSupplier.branch = loggedUser.LoggedUser.branch;
                SupplierCreditAccount ac = await SupplierCreditAccountRestService
                    .CreateSupplierCreditAccountAsync(mapper, new SupplierCreditAccount() { id = 0, totalCredit = 0.00m });
                if (ac.id <= 0)
                {
                    dialogService.ShowMsgDialog("New Supplier Creation", "Can't Save Supplier", MsgDialogType.error, (r) =>
                    {
                        return;
                    });
                }
                SelectedSupplier.supplierCreditAccount = ac;
                Supplier c = await SupplierRestService.CreateSupplierAsync(mapper, SelectedSupplier);
                if (c.id > 0)
                {
                    c.code = $"S{c.id}";
                    c = await SupplierRestService.UpdateSupplierAsync(mapper, c);
                    if (c.id > 0 && !string.IsNullOrEmpty(c.code))
                    {
                        dialogService.ShowMsgDialog("New Supplier Creation", "Supplier Created Successfully", MsgDialogType.infor, (r) =>
                        {
                            LoadSuppliersAsync();
                            NewSupplier();
                        });
                    }
                    else
                    {
                        dialogService.ShowMsgDialog("New Supplier Creation", "Can't Update Supplier", MsgDialogType.error, (r) =>
                        {
                            return;
                        });
                    }
                }
                else
                {
                    dialogService.ShowMsgDialog("New Supplier Creation", "Can't Save Supplier", MsgDialogType.error, (r) =>
                    {
                        return;
                    });

                }
            }
            else
            {
                dialogService.ShowMsgDialog("New Supplier Creation", "Invalid Supplier Details or Already Exist Supplier", MsgDialogType.warrning, (r) =>
                {
                    return;
                });
            }
        }

        private async void UpdateSupplier()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedSupplier != null)
            {
                if (SelectedSupplier.id > 0)
                {
                    Supplier c = await SupplierRestService.UpdateSupplierAsync(mapper, SelectedSupplier);
                    if (c != null)
                    {
                        if (c.id > 0)
                        {
                            dialogService.ShowMsgDialog("Supplier Details Update", "Supplier Update Successfully", MsgDialogType.infor, (r) =>
                            {
                                LoadSuppliersAsync();
                                NewSupplier();
                            });
                        }
                        else
                        {
                            dialogService.ShowMsgDialog("Supplier Details Update", "Can't Update Supplier", MsgDialogType.error, (r) =>
                            {
                                return;
                            });
                        }
                    }
                    else
                    {
                        dialogService.ShowMsgDialog("Supplier Details Update", "Can't Update Supplier", MsgDialogType.error, (r) =>
                        {
                            return;
                        });
                    }
                }
                else
                {
                    dialogService.ShowMsgDialog("Supplier Details Update", "Please Select Supplier Before Update", MsgDialogType.warrning, (r) =>
                    {
                        return;
                    });
                }
            }
            else
            {
                dialogService.ShowMsgDialog("Supplier Details Update", "Please Select Supplier Before Update", MsgDialogType.warrning, (r) =>
                {
                    return;
                });
            }
        }

        private async void NewSupplier()
        {
            aggregator.GetEvent<DataGridSelectionClearEvent>().Publish();
            aggregator.GetEvent<UIElementFocusEvent>().Publish("");
            await Task.Delay(1000);
            SelectedSupplier = new Supplier()
            {
                id = 0,
                name = string.Empty
            };
        }

        private async void DeleteSupplier()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedSupplier != null)
            {
                if (SelectedSupplier.id > 0)
                {
                    int c = await SupplierRestService.DeleteSupplierAsync(mapper, SelectedSupplier.id);
                    if (c > 0)
                    {
                        dialogService.ShowMsgDialog("Supplier Details Delete", "Supplier Delete Successfully", MsgDialogType.infor, (r) =>
                        {
                            LoadSuppliersAsync();
                            NewSupplier();
                        });
                    }
                    else
                    {
                        dialogService.ShowMsgDialog("Supplier Details Delete", "Can't Delete Supplier", MsgDialogType.error, (r) =>
                        {
                            return;
                        });
                    }
                }
                else
                {
                    dialogService.ShowMsgDialog("Supplier Details Delete", "Please Select Supplier Before Delete", MsgDialogType.warrning, (r) =>
                    {
                        return;
                    });
                }
            }
            else
            {
                dialogService.ShowMsgDialog("Supplier Details Delete", "Please Select Supplier Before Delete", MsgDialogType.warrning, (r) =>
                {
                    return;
                });
            }
        }

        #endregion
    }
}
