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

namespace Wingcode.Customers.ViewModels
{
    public class CustomerRegisterViewModel : BaseViewModel
    {
        #region Private Property

        private IDialogService dialogService;
        private IContainerExtension containerExtension;
        private IEventAggregator aggregator;
        private ILoggedUserProvider loggedUser;

        #endregion

        #region Public Full Property

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private Customer selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set { SetProperty(ref selectedCustomer, value); }
        }

        private ObservableCollection<Customer> sourceCustomers;

        public ObservableCollection<Customer> SourceCustomers
        {
            get { return sourceCustomers; }
            set { SetProperty(ref sourceCustomers, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand
        {
            get { return _searchCommand; }
            set { SetProperty(ref _searchCommand, value); }
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

        private DelegateCommand _deleteCommand;
        public DelegateCommand DeleteCommand
        {
            get { return _deleteCommand; }
            set { SetProperty(ref _deleteCommand, value); }
        }

        #endregion

        #region Constructor

        public CustomerRegisterViewModel(IContainerExtension container, IDialogService dialog, IEventAggregator eventAggregator)
        {
            containerExtension = container;
            dialogService = dialog;
            aggregator = eventAggregator;
            SearchCommand = new DelegateCommand(SearchCustomer);
            SaveCommand = new DelegateCommand(SaveCustomer);
            NewCommand = new DelegateCommand(NewCustomer);
            UpdateCommand = new DelegateCommand(UpdateCustomer);
            DeleteCommand = new DelegateCommand(DeleteCustomer);
            loggedUser = containerExtension.Resolve<ILoggedUserProvider>();                        
        }

        #endregion

        #region Functions

        internal void Initialize()
        {
            LoadCustomersAsync();
            NewCustomer();
        }


        private async void LoadCustomersAsync() 
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            SourceCustomers = await CustomerRestService.GetCustomerByBranchIdAsync(mapper, loggedUser.LoggedUser.branch.id);
        }

        private void SearchCustomer()
        {

        }

        private async void SaveCustomer()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (!string.IsNullOrEmpty(SelectedCustomer.name) && SelectedCustomer.id == 0)
            {
                SelectedCustomer.branch = loggedUser.LoggedUser.branch;
                CustomerCreditAccount ac = await CustomerCreditAccountRestService
                    .CreateCustomerCreditAccountAsync(mapper, new CustomerCreditAccount() { id = 0, totalCredit = 0.00m});
                if (ac.id <= 0) 
                {
                    dialogService.ShowMsgDialog("New Customer Creation", "Can't Save Customer", MsgDialogType.error, (r) =>
                    {
                        return;
                    });
                }
                SelectedCustomer.customerCreditAccount = ac;
                Customer c = await CustomerRestService.CreateCustomerAsync(mapper, SelectedCustomer);
                if (c.id > 0)
                {
                    c.code = $"C{c.id}";
                    c = await CustomerRestService.UpdateCustomerAsync(mapper, c);
                    if (c.id > 0 && !string.IsNullOrEmpty(c.code))
                    {
                        dialogService.ShowMsgDialog("New Customer Creation", "Customer Created Successfully", MsgDialogType.infor, (r) =>
                        {
                            LoadCustomersAsync();
                            NewCustomer();
                        });
                    }
                    else
                    {
                        dialogService.ShowMsgDialog("New Customer Creation", "Can't Update Customer", MsgDialogType.error, (r) =>
                        {
                            return;
                        });
                    }
                }
                else
                {
                    dialogService.ShowMsgDialog("New Customer Creation", "Can't Save Customer", MsgDialogType.error, (r) =>
                    {
                        return;
                    });

                }
            }
            else 
            {
                dialogService.ShowMsgDialog("New Customer Creation", "Invalid Customer Details or Already Exist Customer", MsgDialogType.warrning, (r) =>
                {
                    return;
                });
            }
        }

        private async void UpdateCustomer()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedCustomer != null)
            {
                if (SelectedCustomer.id > 0)
                {
                    Customer c = await CustomerRestService.UpdateCustomerAsync(mapper, selectedCustomer);
                    if (c != null)
                    {
                        if (c.id > 0)
                        {
                            dialogService.ShowMsgDialog("Customer Details Update", "Customer Update Successfully", MsgDialogType.infor, (r) =>
                            {
                                LoadCustomersAsync();
                                NewCustomer();
                            });
                        }
                        else
                        {
                            dialogService.ShowMsgDialog("Customer Details Update", "Can't Update Customer", MsgDialogType.error, (r) =>
                            {
                                return;
                            });
                        }
                    }
                    else
                    {
                        dialogService.ShowMsgDialog("Customer Details Update", "Can't Update Customer", MsgDialogType.error, (r) =>
                        {
                            return;
                        });
                    }
                }
                else
                {
                    dialogService.ShowMsgDialog("Customer Details Update", "Please Select Customer Before Update", MsgDialogType.warrning, (r) =>
                    {
                        return;
                    });
                }
            }
            else
            {
                dialogService.ShowMsgDialog("Customer Details Update", "Please Select Customer Before Update", MsgDialogType.warrning, (r) =>
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
            SelectedCustomer = new Customer()
            {
                id = 0,
                name = string.Empty
            };            
        }

        private async void DeleteCustomer()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedCustomer != null)
            {
                if (SelectedCustomer.id > 0)
                {
                    int c = await CustomerRestService.DeleteCustomerAsync(mapper, selectedCustomer.id);
                    if (c > 0)
                    {
                        dialogService.ShowMsgDialog("Customer Details Delete", "Customer Delete Successfully", MsgDialogType.infor, (r) =>
                        {
                            LoadCustomersAsync();
                            NewCustomer();
                        });
                    }
                    else
                    {
                        dialogService.ShowMsgDialog("Customer Details Delete", "Can't Delete Customer", MsgDialogType.error, (r) =>
                        {
                            return;
                        });
                    }
                }
                else
                {
                    dialogService.ShowMsgDialog("Customer Details Delete", "Please Select Customer Before Delete", MsgDialogType.warrning, (r) =>
                    {
                        return;
                    });
                }
            }
            else
            {
                dialogService.ShowMsgDialog("Customer Details Delete", "Please Select Customer Before Delete", MsgDialogType.warrning, (r) =>
                {
                    return;
                });
            }
        }

        #endregion
    }
}
