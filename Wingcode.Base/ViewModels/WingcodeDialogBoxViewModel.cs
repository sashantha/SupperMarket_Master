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
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private BaseDialogHostViewModel _dialogHost;
        public BaseDialogHostViewModel DialogHost
        {
            get { return _dialogHost; }
            set { SetProperty(ref _dialogHost, value); }
        }

        private Control _container;
        public Control Container
        {
            get { return _container; }
            set { SetProperty(ref _container, value); }
        }

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
