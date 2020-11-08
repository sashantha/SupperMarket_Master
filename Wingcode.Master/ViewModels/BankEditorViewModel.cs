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
    public class BankEditorViewModel : BaseViewModel
    {

        #region Property

        private IDialogService dialogService;
        private IContainerExtension containerExtension;
        private IEventAggregator aggregator;
        private ILoggedUserProvider loggedUser;


        public Bank SelectedBank { get; set; }

        public bool IsNew { get; set; }

        #endregion

        #region Commands

        public DelegateCommand SaveCommand { get; set; }

        public DelegateCommand NewCommand { get; set; }

        public DelegateCommand UpdateCommand { get; set; }

        #endregion

        #region Cunstructor

        public BankEditorViewModel(IContainerExtension extension, ILoggedUserProvider provider)
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

            if (SelectedBank.IsValidBank())
            {
                Bank b = await BankRestService.CreateBankAsync(mapper, SelectedBank);
                if (b.id > 0)
                {
                    _ = ShowMessageDialg("New Bank Creation", "Bank Created Successfully", MsgDialogType.infor);
                    RizeSyncEvent();
                    Initialize();
                }
                else
                {
                    _ = ShowMessageDialg("New Bank Creation", "Can't Save Branch", MsgDialogType.error);
                    return;
                }
            }
            else
            {
                _ = ShowMessageDialg("New Bank Creation", "Invalid Bank Details or Already Exist Branch", MsgDialogType.warrning);
                return;
            }
        }

        private async void UpdateBranch()
        {
            IRestDataMapper mapper = containerExtension.Resolve<IRestDataMapper>();
            if (SelectedBank != null)
            {
                if (SelectedBank.id > 0)
                {
                    Bank i = await BankRestService.UpdateBankAsync(mapper, SelectedBank);
                    if (i.id > 0)
                    {
                        _ = ShowMessageDialg("Bank Update", "Bank Updated Successfully", MsgDialogType.infor);
                        RizeSyncEvent();
                    }
                    else
                    {
                        _ = ShowMessageDialg("Bank Update", "Can't Save Bank", MsgDialogType.error);
                        return;
                    }
                }
                else
                {
                    _ = ShowMessageDialg("Bank Update", "Please Select Bank Before Update", MsgDialogType.warrning);
                    return;
                }
            }
            else
            {
                _ = ShowMessageDialg("Bank Update", "Please Select Bank Before Update", MsgDialogType.warrning);
                return;
            }
        }

        private async void NewBranch()
        {
            aggregator.GetEvent<UIElementFocusEvent>().Publish("");
            await Task.Delay(500);
            SelectedBank = SelectedBank.CreateNewBank();
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
