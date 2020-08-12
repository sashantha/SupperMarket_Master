using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wingcode.Base.DataModel;

namespace Wingcode.Base.ViewModels
{
    public class SubMenuItemViewModel : BaseViewModel
    {
        public int Index { get; set; }

        public string Name { get; set; }

        public string ParentName { get; set; }

        public Type  AttachedControl { get; set; }

        public SubMenuItemViewModel()
        {
        }
    }
}
