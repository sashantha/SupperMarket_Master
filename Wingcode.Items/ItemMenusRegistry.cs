using System.Collections.Generic;
using Wingcode.Base.Api;
using Wingcode.Base.ViewModels;
using Wingcode.Items.Views;

namespace Wingcode.Items
{
    internal class ItemMenusRegistry : IMenuRegistry
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
                    Index = 3,
                    Name = "Item Register",
                    AttachedControl = typeof(ItemRegister)
                },
            };
            Dictionary<string, List<SubMenuItemViewModel>> dic = new Dictionary<string, List<SubMenuItemViewModel>>
            {
                { "Registers", subs }
            };
            return dic;
        }
    }
}
