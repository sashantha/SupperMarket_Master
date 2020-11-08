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

        public string Title { get; set; }

        public Control Container { get; set; }

        public DelegateCommand<string> CloseCommand { get; set; }

    }
}
