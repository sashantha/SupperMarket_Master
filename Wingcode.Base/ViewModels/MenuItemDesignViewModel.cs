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
    public class MenuItemDesignViewModel : MenuItemViewModel
    {
        public static MenuItemDesignViewModel instance => new MenuItemDesignViewModel();

        public MenuItemDesignViewModel()
        {
            Name = "Home";
            Text = "Home";
            IconKind = PackIconKind.Home;
        }
    }
}
