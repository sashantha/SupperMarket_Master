using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingcode.Base.Api;
using Wingcode.Base.ViewModels;
using Wingcode.Suppliers.Views;

namespace Wingcode.Suppliers
{
    internal class SupplierMenuRegistry : IMenuRegistry
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
                    Index = 2,
                    Name = "Supplier Register",
                    AttachedControl = typeof(SupplierRegister)
                }
            };
            Dictionary<string, List<SubMenuItemViewModel>> dic = new Dictionary<string, List<SubMenuItemViewModel>>
            {
                { "Registers", subs }
            };
            return dic;
        }
    }
}
