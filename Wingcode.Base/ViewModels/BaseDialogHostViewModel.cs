using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wingcode.Base.DataModel;

namespace Wingcode.Base.ViewModels
{
    public abstract class BaseDialogHostViewModel : BaseViewModel
    {

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private Control _container;
        public Control Container
        {
            get { return _container; }
            set { SetProperty(ref _container, value);}
        }

        private DelegateCommand<string> _closeCommand;
        public DelegateCommand<string> CloseCommand
        {
            get { return _closeCommand; }
            set { SetProperty(ref _closeCommand, value); }
        }

    }
}
