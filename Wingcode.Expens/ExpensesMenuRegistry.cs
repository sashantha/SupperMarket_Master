using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base.Api;
using Wingcode.Base.ViewModels;
using Wingcode.Expens.Views;

namespace Wingcode.Expens
{
    internal class ExpensesMenuRegistry : IMenuRegistry
    {
        public List<MenuItemViewModel> GetMenuRegistry()
        {
            return null;
        }

        public Dictionary<string, List<SubMenuItemViewModel>> GetSubMenuRegistry()
        {
            List<SubMenuItemViewModel> subs = new List<SubMenuItemViewModel>
            {
                new SubMenuItemViewModel()
                {
                    Index = 0,
                    Name = "Expens",
                    AttachedControl = typeof(Expenses)
                }
            };
            Dictionary<string, List<SubMenuItemViewModel>> dic = new Dictionary<string, List<SubMenuItemViewModel>>
            {
                { "Expenses", subs }
            };
            return dic;
        }
    }
}
