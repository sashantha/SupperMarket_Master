using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base.DataModel;

namespace Wingcode.Base.ViewModels
{
    public class BaseDialogViewModel : BaseViewModel
    {

        public DelegateCommand<string> CloseCommand { get; set; }
    }
}
