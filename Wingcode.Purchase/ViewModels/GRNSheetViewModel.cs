using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wingcode.Purchase.ViewModels
{
    public class GRNSheetViewModel : BindableBase
    {

        private DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand
        {
            get { return _searchCommand; }
            set { SetProperty(ref _searchCommand, value); }
        }

        public GRNSheetViewModel()
        {
            SearchCommand = new DelegateCommand(SearchItem);
        }

        private void SearchItem()
        {
            
        }
    }
}
