using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wingcode.Supplier.ViewModels
{
    public class SupplierRegisterViewModel : BindableBase
    {

        private IDialogService dialogService;
        private IContainerExtension containerExtension;

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand
        {
            get { return _searchCommand; }
            set { SetProperty(ref _searchCommand, value); }
        }

        public SupplierRegisterViewModel(IContainerExtension container, IDialogService dialog)
        {
            containerExtension = container;
            dialogService = dialog;
            SearchCommand = new DelegateCommand(SearchItem);
        }

        private void SearchItem()
        {
            throw new NotImplementedException();
        }
    }
}
