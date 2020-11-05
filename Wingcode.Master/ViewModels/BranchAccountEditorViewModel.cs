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
using Wingcode.Data.Rest.Model;
using Wingcode.Data.Rest.Service;
using Wingcode.Master.Event;
using Wingcode.Master.Extension;

namespace Wingcode.Master.ViewModels
{
    public class BranchAccountEditorViewModel : BaseViewModel
    {
        #region Property

        private IDialogService dialogService;
        private IContainerExtension containerExtension;
        private IEventAggregator aggregator;
        private ILoggedUserProvider loggedUser;


        private BranchAccount _selectedBranchAcc;
        public BranchAccount SelectedBranchAcc
        {
            get { return _selectedBranchAcc; }
            set { SetProperty(ref _selectedBranchAcc, value); }
        }

        private bool _isNew;
        public bool IsNew
        {
            get { return _isNew; }
            set { SetProperty(ref _isNew, value); }
        }

        private Bank selectedBank;
        public Bank SelectedBank
        {
            get { return selectedBank; }
            set { SetProperty(ref selectedBank, value); }
        }

        private ObservableCollection<Bank> banks;
        public ObservableCollection<Bank> Banks
        {
            get { return banks; }
            set { SetProperty(ref banks, value); }
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

        public BranchAccountEditorViewModel(IContainerExtension extension, ILoggedUserProvider provider)
        {
            containerExtension = extension;
            loggedUser = provider;
            SaveCommand = new DelegateCommand(SaveBranch);
            NewCommand = new DelegateCommand(NewBranch);
            UpdateCommand = new DelegateCommand(UpdateBranch);
            dialogService = containerExtension.Resolve<IDialogService>();
            aggregator = containerExtension.Resolve<IEventAggregator>();
        }

        #endregion

        #region Functions
        internal async void Initialize()
        {
            LoadAllItem();
            await Task.Delay(500);
            if (IsNew)
                NewBranch();
            else
                aggregator.GetEvent<BankSelectionEvent>().Publish(SelectedBranchAcc.bank);
        }

        private async void LoadAllItem()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            Banks = await BankRestService.GetAllBankAsync(mapper);
        }
        private async void SaveBranch()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (selectedBank == null)
            {
                _ = ShowMessageDialg("New Branch Account Creation", "Can't Save Branch Account, Bank not selected", MsgDialogType.error);
            }
            if (SelectedBranchAcc.IsValidBranchAccount())
            {
                SelectedBranchAcc.bank = selectedBank;
                SelectedBranchAcc.branch = loggedUser.LoggedUser.branch;
                BranchAccount b = await BranchRestService.CreateBranchAccountAsync(mapper, SelectedBranchAcc);
                if (b.id > 0)
                {
                    _ = ShowMessageDialg("New Branch Account Creation", "Branch Account Created Successfully", MsgDialogType.infor);
                    RizeSyncEvent();
                    Initialize();
                }
                else
                {
                    _ = ShowMessageDialg("New Branch Account Creation", "Can't Save Branch Account", MsgDialogType.error);
                    return;
                }
            }
            else
            {
                _ = ShowMessageDialg("New Branch Account Creation", "Invalid Branch Account Details or Already Exist Branch Account", MsgDialogType.warrning);
                return;
            }
        }

        private async void UpdateBranch()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedBranchAcc != null)
            {
                if (SelectedBranchAcc.id > 0)
                {
                    BranchAccount i = await BranchRestService.UpdateBranchAccountAsync(mapper, SelectedBranchAcc);
                    if (i.id > 0)
                    {
                        _ = ShowMessageDialg("Branch Account Update", "Branch Account Updated Successfully", MsgDialogType.infor);
                        RizeSyncEvent();
                    }
                    else
                    {
                        _ = ShowMessageDialg("Branch Account Update", "Can't Save Branch Account", MsgDialogType.error);
                        return;
                    }
                }
                else
                {
                    _ = ShowMessageDialg("Branch Account Update", "Please Select Branch Account Before Update", MsgDialogType.warrning);
                    return;
                }
            }
            else
            {
                _ = ShowMessageDialg("Branch Account Update", "Please Select Branch Account Before Update", MsgDialogType.warrning);
                return;
            }
        }

        private async void NewBranch()
        {
            aggregator.GetEvent<UIElementFocusEvent>().Publish("");
            await Task.Delay(500);
            SelectedBranchAcc = SelectedBranchAcc.CreateNewBranchAccount();
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

        #endregion
    }
}
