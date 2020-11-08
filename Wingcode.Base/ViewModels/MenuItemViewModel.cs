using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Wingcode.Base.DataModel;

namespace Wingcode.Base.ViewModels
{
    public class MenuItemViewModel : BaseViewModel
    {
        public int Index { get; set; }

        public string Text { get; set; }

        public string Name { get; set; }

        public PackIconKind IconKind { get; set; }

        public Type  AttachedControl { get; set; }

        public List<SubMenuItemViewModel> Childs { get; set; }

        public bool HasChilds => Childs == null ? true : false;

        public bool ItemMenuVisibility => Childs == null ? false : true;

        public Color SelectionColor => new PaletteHelper().GetTheme().Selection;

        public bool IsExpanded { get; set; }

        public MenuItemViewModel()
        {
        }
    }
}
