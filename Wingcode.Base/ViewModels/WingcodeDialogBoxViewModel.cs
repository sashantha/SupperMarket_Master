using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wingcode.Base.Dialog;

namespace Wingcode.Base.ViewModels
{
    public class WingcodeDialogBoxViewModel : BaseDialogViewModel, IDialogAware
    {
        public string Title { get; set; }

        public BaseDialogHostViewModel DialogHost { get; set; }

        public Control Container { get; set; }

        public event Action<IDialogResult> RequestClose;

        public WingcodeDialogBoxViewModel()
        {
            CloseCommand = new DelegateCommand<string>(CloseDialog);
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
            DialogHost = parameters.GetValue<BaseDialogHostViewModel>(DialogParameterKeys.DialogHost);

            if (DialogHost == null)
                return;
            Title = DialogHost.Title;
            Container = DialogHost.Container;
            DialogHost.CloseCommand = new DelegateCommand<string>(CloseDialog);
        }
    }
}
