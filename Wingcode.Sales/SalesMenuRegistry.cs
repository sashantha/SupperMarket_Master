using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base.Api;
using Wingcode.Base.ViewModels;
using Wingcode.Sales.Views;

namespace Wingcode.Sales
{
    internal class SalesMenuRegistry : IMenuRegistry
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
                    Name = "Make Sale",
                    AttachedControl = typeof(POSSheet)
                }
            };
            Dictionary<string, List<SubMenuItemViewModel>> dic = new Dictionary<string, List<SubMenuItemViewModel>>
            {
                { "Sales", subs }
            };
            return dic;
        }
    }
}
