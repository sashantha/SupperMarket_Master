using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using Wingcode.Base.DataModel;
using Wingcode.Base.Event;
using Wingcode.Base.Extensions;
using Wingcode.Data.Rest.Model;
using Wingcode.Data.Rest.Service;
using Wingcode.Master.Views;

namespace Wingcode.Master.ViewModels
{
    public class MasterRegisterViewModel : BaseViewModel
    {

        #region Property

        private IContainerExtension containerExtension;
        private ILoggedUserProvider loggedUser;
        private IDialogService dialogService;
        private IEventAggregator aggregator;

        private Branch selectedBranch;
        public Branch SelectedBranch
        {
            get { return selectedBranch; }
            set { SetProperty(ref selectedBranch, value); }
        }

        private ObservableCollection<Branch> branches;
        public ObservableCollection<Branch> Branches
        {
            get { return branches; }
            set { SetProperty(ref branches, value); }
        }

        private bool canAccessBranch;   

        public bool CanAccessBranch
        {
            get { return canAccessBranch; }
            set { canAccessBranch = value; }
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

        private BranchAccount selectedBranchAc;
        public BranchAccount SelectedBranchAc
        {
            get { return selectedBranchAc; }
            set { SetProperty(ref selectedBranchAc, value); }
        }

        private ObservableCollection<BranchAccount> brancheacs;
        public ObservableCollection<BranchAccount> BrancheAcs
        {
            get { return brancheacs; }
            set { SetProperty(ref brancheacs, value); }
        }

        private Unit selectedMeasure;
        public Unit SelectedMeasure
        {
            get { return selectedMeasure; }
            set { SetProperty(ref selectedMeasure, value); }
        }

        private ObservableCollection<Unit> measures;
        public ObservableCollection<Unit> Measures
        {
            get { return measures; }
            set { SetProperty(ref measures, value); }
        }

        
        #endregion

        #region Commands

        private DelegateCommand _branchEditorCommand;
        public DelegateCommand BranchEditorCommand
        {
            get { return _branchEditorCommand; }
            set { SetProperty(ref _branchEditorCommand, value); }
        }

        private DelegateCommand _branchAccEditorCommand;
        public DelegateCommand BranchAccEditorCommand
        {
            get { return _branchAccEditorCommand; }
            set { SetProperty(ref _branchAccEditorCommand, value); }
        }

        private DelegateCommand _bankEditorCommand;
        public DelegateCommand BankEditorCommand
        {
            get { return _bankEditorCommand; }
            set { SetProperty(ref _bankEditorCommand, value); }
        }

        private DelegateCommand measureEditorCommand;
        public DelegateCommand MeasureEditorCommand
        {
            get { return measureEditorCommand; }
            set { SetProperty(ref measureEditorCommand, value); }
        }
               
        #endregion

        #region Constructor

        public MasterRegisterViewModel(IContainerExtension container)
        {
            containerExtension = container;
            BranchEditorCommand = new DelegateCommand(ShowBranchEditor);
            BranchAccEditorCommand = new DelegateCommand(ShowBranchAccEditor);
            BankEditorCommand = new DelegateCommand(ShowBankEditor);
            MeasureEditorCommand = new DelegateCommand(ShowMeasureEditor);
            loggedUser = containerExtension.Resolve<ILoggedUserProvider>();
            dialogService = containerExtension.Resolve<IDialogService>();
            aggregator = containerExtension.Resolve<IEventAggregator>();
            CanAccessBranch = false;
        }
               

        #endregion

        #region Functions and Events

        internal void Initialize()
        {
            CanAccessBranch = loggedUser.LoggedUser.userRole.Equals("ADMIN");
            LoadAllItem();
        }

        private async void LoadAllItem()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            Branches = await BranchRestService.GetAllBranchAsync(mapper);
            Banks = await BankRestService.GetAllBankAsync(mapper);
            BrancheAcs = await BranchRestService.GetAllBranchAccountAsync(mapper, loggedUser.LoggedUser.branch.id);
            Measures = await ItemRestService.GetAllUnitAsync(mapper);
        }

        private async void ShowBranchEditor()
        {
            BranchEditorViewModel viewModel = new BranchEditorViewModel(containerExtension, loggedUser);
            if (SelectedBranch != null)
            {
                viewModel.IsNew = false;
                viewModel.SelectedBranch = SelectedBranch;
            }
            else
            {
                viewModel.IsNew = true;
            }
            viewModel.SyncEditro += ViewModel_SyncEditro;
            _ = await DialogHost.Show(new BranchEditor() 
            {
                DataContext = viewModel
            }, "MasterRoot", EditorClosing);
        }

        private void ViewModel_SyncEditro()
        {
            LoadAllItem();
            aggregator.GetEvent<DataGridSelectionClearEvent>().Publish();
            SelectedBranch = null;
        }

        private void EditorClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadAllItem();
            aggregator.GetEvent<DataGridSelectionClearEvent>().Publish();
            SelectedBranch = null;
        }

        private async void ShowBranchAccEditor()
        {
            BranchAccountEditorViewModel viewModel = new BranchAccountEditorViewModel(containerExtension, loggedUser);
            if (SelectedBranchAc != null)
            {
                viewModel.IsNew = false;
                viewModel.SelectedBranchAcc = SelectedBranchAc;
            }
            else
            {
                viewModel.IsNew = true;
            }
            viewModel.SyncEditro += ViewModel_SyncEditro;
            _ = await DialogHost.Show(new BranchAccountEditor()
            {
                DataContext = viewModel
            }, "MasterRoot", EditorClosing);
        }

        private async void ShowBankEditor()
        {
            BankEditorViewModel viewModel = new BankEditorViewModel(containerExtension, loggedUser);
            if (SelectedBank != null)
            {
                viewModel.IsNew = false;
                viewModel.SelectedBank = SelectedBank;
            }
            else
            {
                viewModel.IsNew = true;
            }
            viewModel.SyncEditro += ViewModel_SyncEditro;
            _ = await DialogHost.Show(new BankEditor()
            {
                DataContext = viewModel
            }, "MasterRoot", EditorClosing);
        }

        private async void ShowMeasureEditor()
        {
            MeasureEditorViewModel viewModel = new MeasureEditorViewModel(containerExtension, loggedUser);
            if (SelectedMeasure != null)
            {
                viewModel.IsNew = false;
                viewModel.SelectedMeasure = SelectedMeasure;
            }
            else
            {
                viewModel.IsNew = true;
            }
            viewModel.SyncEditro += ViewModel_SyncEditro;
            _ = await DialogHost.Show(new MeasureEditor()
            {
                DataContext = viewModel
            }, "MasterRoot", EditorClosing);
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
