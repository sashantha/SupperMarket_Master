using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System.Threading.Tasks;
using Wingcode.Base.DataModel;
using Wingcode.Base.Event;
using Wingcode.Base.Extensions;
using Wingcode.Data.Rest.Model;
using Wingcode.Data.Rest.Service;
using Wingcode.Master.Extension;

namespace Wingcode.Master.ViewModels
{
    public class BranchEditorViewModel : BaseViewModel
    {

        #region Property

        private IDialogService dialogService;
        private IContainerExtension containerExtension;
        private IEventAggregator aggregator;
        private ILoggedUserProvider loggedUser;


        public Branch SelectedBranch { get; set; }

        public bool IsNew { get; set; }

        #endregion

        #region Commands

        public DelegateCommand SaveCommand { get; set; }

        public DelegateCommand NewCommand { get; set; }

        public DelegateCommand UpdateCommand { get; set; }

        #endregion

        #region Constructor

        public BranchEditorViewModel(IContainerExtension extension, ILoggedUserProvider provider)
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
        internal void Initialize()
        {
            if (IsNew)
                NewBranch();
        }

        private async void SaveBranch()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            
            if (SelectedBranch.IsValidBranch())
            {
                Branch b = await BranchRestService.CreateBranchAsync(mapper, SelectedBranch);
                if (b.id > 0)
                {
                    b.code = $"BRN{b.id}";
                    b = await BranchRestService.UpdateBranchAsync(mapper, b);
                    if (b.id > 0)
                    {
                        _ = ShowMessageDialg("New Branch Creation", "Branch Created Successfully", MsgDialogType.infor);
                        RizeSyncEvent();
                        Initialize();
                    }
                    else
                    {
                        _ = ShowMessageDialg("New Branch Creation", "Can't Branch Code", MsgDialogType.error);
                        return;
                    }
                }
                else
                {
                    _ = ShowMessageDialg("New Branch Creation", "Can't Save Branch", MsgDialogType.error);
                    return;
                }
            }
            else
            {
                _ = ShowMessageDialg("New Branch Creation", "Invalid Branch Details or Already Exist Branch", MsgDialogType.warrning);
                return;
            }
        }

        private async void UpdateBranch()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedBranch != null)
            {
                if (SelectedBranch.id > 0)
                {
                    Branch i = await BranchRestService.UpdateBranchAsync(mapper, SelectedBranch);
                    if (i.id > 0)
                    {
                        _ = ShowMessageDialg("Branch Update", "Branch Updated Successfully", MsgDialogType.infor);
                        RizeSyncEvent();
                    }
                    else
                    {
                        _ = ShowMessageDialg("Branch Update", "Can't Save Branch", MsgDialogType.error);
                        return;
                    }
                }
                else
                {
                    _ = ShowMessageDialg("Branch Update", "Please Select Branch Before Update", MsgDialogType.warrning);
                    return;
                }
            }
            else
            {
                _ = ShowMessageDialg("Branch Update", "Please Select Branch Before Update", MsgDialogType.warrning);
                return;
            }
        }

        private async void NewBranch()
        {
            aggregator.GetEvent<UIElementFocusEvent>().Publish("");
            await Task.Delay(500);
            SelectedBranch = SelectedBranch.CreateNewBranch();
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
