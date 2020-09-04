using Prism.Commands;
using Prism.Common;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Wingcode.Base.DataModel;
using Wingcode.Base.Extensions;

namespace Wingcode.Item.ViewModels
{
    public class ItemRegisterViewModel : BaseViewModel
    {
        private IDialogService dialogService;
        private IContainerExtension containerExtension;

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private DelegateCommand _showCommand;
        public DelegateCommand ShowCommand
        {
            get { return _showCommand; }
            set { SetProperty(ref _showCommand, value); }
        }

        private DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand
        {
            get { return _searchCommand; }
            set { SetProperty(ref _searchCommand, value); }
        }

        public TestItem Item { get; set; } = new TestItem();

        public ItemRegisterViewModel(IContainerExtension container, IDialogService dialog)
        {
            dialogService = dialog;
            containerExtension = container;
            SearchText = string.Empty;
            ShowCommand = new DelegateCommand(ShowMessage);
            SearchCommand = new DelegateCommand(SearchItem);
        }

        private void SearchItem()
        {
            
        }

        private void ShowMessage()
        {
            dialogService.ShowMsgDialog("Item Control", "Item Control Working", MsgDialogButtonType.Ok, r => { });            
        }
    }
}
