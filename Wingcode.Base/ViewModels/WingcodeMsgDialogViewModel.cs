using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base.DataModel;
using Wingcode.Base.Dialog;

namespace Wingcode.Base.ViewModels
{
    public class WingcodeMsgDialogViewModel : BaseDialogViewModel, IDialogAware
    {

        public string Title { get; set; }

        public PackIconKind IconKind { get; set; }

        public bool IsNotIconVisible { get; set; }

        public string Message { get; set; }

        public bool OkEnable { get; set; }

        public bool OkCancelEnable { get; set; }

        public bool YesNoEnable { get; set; }

        public DelegateCommand<string> OkDialogCommand { get; set; }
        public DelegateCommand<string> YesDialogCommand { get; set; }
        public DelegateCommand<string> NoDialogCommand { get; set; }

        public event Action<IDialogResult> RequestClose;

        public WingcodeMsgDialogViewModel()
        {
            CloseCommand = new DelegateCommand<string>(CloseDialog);
            OkDialogCommand = new DelegateCommand<string>(CloseDialog);
            YesDialogCommand = new DelegateCommand<string>(CloseDialog);
            NoDialogCommand = new DelegateCommand<string>(CloseDialog);
        }

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            switch (parameter?.ToLower())
            {
                case "ok":
                    result = ButtonResult.OK;
                    break;
                case "yes":
                    result = ButtonResult.Yes;
                    break;
                case "no":
                    result = ButtonResult.No;
                    break;
                case "cancel":
                    result = ButtonResult.Cancel;
                    break;
            }
            RaiseRequestClose(new DialogResult(result));
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters == null)
                return;

            Title = parameters.GetValue<string>(DialogParameterKeys.Title);
            Message = parameters.GetValue<string>(DialogParameterKeys.Message);
            MsgDialogButtonType bType = parameters.GetValue<MsgDialogButtonType>(DialogParameterKeys.ButtonType);
            UpdateMsgBoxButtons(bType);
            MsgDialogType dialogType = parameters.GetValue<MsgDialogType>(DialogParameterKeys.DialogType);
            UpdateMsgDialogType(dialogType);
        }

        private void UpdateMsgDialogType(MsgDialogType dialogType)
        {
            switch (dialogType)
            {
                case MsgDialogType.none:
                    IsNotIconVisible = true;
                    IconKind = PackIconKind.Information;
                    break;
                case MsgDialogType.infor:
                    IsNotIconVisible = false;
                    IconKind = PackIconKind.Information;
                    break;
                case MsgDialogType.warrning:
                    IsNotIconVisible = false;
                    IconKind = PackIconKind.Alert;
                    break;
                case MsgDialogType.error:
                    IsNotIconVisible = false;
                    IconKind = PackIconKind.CloseCircle;
                    break;
                case MsgDialogType.Confirmation:
                    IsNotIconVisible = true;
                    IconKind = PackIconKind.Information;
                    Message += " ?";
                    break;
            }
        }

        private void UpdateMsgBoxButtons(MsgDialogButtonType bType)
        {
            switch (bType)
            {
                case MsgDialogButtonType.Ok:
                    OkEnable = true;
                    OkCancelEnable = false;
                    YesNoEnable = false;
                    break;
                case MsgDialogButtonType.OkCancel:
                    OkEnable = false;
                    OkCancelEnable = true;
                    YesNoEnable = false;
                    break;
                case MsgDialogButtonType.YesNo:
                    OkEnable = false;
                    OkCancelEnable = false;
                    YesNoEnable = true;
                    break;
            }
        }
    }
}
